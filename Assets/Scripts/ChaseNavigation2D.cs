using System.Collections.Generic;
using UnityEngine;

internal sealed class ChaseNavigation2D
{
    private const float RepathInterval = 0.35f;
    private const float WaypointTolerance = 0.1f;
    private const int MaxSearchCells = 2400;

    private static readonly Vector2Int[] Neighbors =
    {
        Vector2Int.right, Vector2Int.up, Vector2Int.left, Vector2Int.down,
        new Vector2Int(1, 1), new Vector2Int(-1, 1),
        new Vector2Int(-1, -1), new Vector2Int(1, -1)
    };

    private readonly ContactFilter2D obstacleFilter;
    private readonly Collider2D footprint;
    private readonly float radius;
    private readonly Vector2 size;
    private Vector2 shapeOffset;
    private readonly float cellSize;
    private readonly List<Collider2D> overlaps = new List<Collider2D>();
    private readonly List<RaycastHit2D> hits = new List<RaycastHit2D>();
    private readonly List<Vector2> waypoints = new List<Vector2>();
    private readonly Dictionary<Vector2Int, float> costs = new Dictionary<Vector2Int, float>();
    private readonly Dictionary<Vector2Int, Vector2Int> parents = new Dictionary<Vector2Int, Vector2Int>();
    private readonly HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
    private readonly List<Vector2Int> queuedCells = new List<Vector2Int>();
    private readonly SortedSet<(float Estimate, int Order)> frontier = new SortedSet<(float, int)>();
    private Vector2 lastDestination;
    private float nextSearchTime;

    public ChaseNavigation2D(Rigidbody2D body, LayerMask obstacleLayer, float cellSize)
    {
        int collisionMask = Physics2D.GetLayerCollisionMask(body.gameObject.layer);
        obstacleFilter = new ContactFilter2D { useTriggers = false };
        obstacleFilter.SetLayerMask(collisionMask | obstacleLayer.value);

        float largestSolid = 0f;
        float smallestTrigger = float.PositiveInfinity;
        foreach (Collider2D candidate in body.GetComponents<Collider2D>())
        {
            if (!candidate.enabled)
                continue;

            float area = candidate.bounds.size.sqrMagnitude;
            if (!candidate.isTrigger && area > largestSolid)
            {
                largestSolid = area;
                footprint = candidate;
            }
            else if (candidate.isTrigger && largestSolid == 0f && area < smallestTrigger)
            {
                smallestTrigger = area;
                footprint = candidate;
            }
        }

        // Shrink the cast slightly so resting against a wall does not trap the enemy.
        Vector2 dimensions = footprint != null ? (Vector2)footprint.bounds.size : Vector2.one * 0.5f;
        size = Vector2.Max(dimensions - Vector2.one * 0.05f, Vector2.one * 0.05f);
        radius = Mathf.Max(size.x, size.y) * 0.5f;
        this.cellSize = Mathf.Max(0.4f, cellSize);
    }

    public bool HasLineOfSight(Vector2 origin, Vector2 destination)
    {
        return IsClear(origin, destination, false);
    }

    public Vector2 GetDirection(Vector2 origin, Vector2 destination)
    {
        shapeOffset = footprint != null ? (Vector2)footprint.bounds.center - origin : Vector2.zero;
        if (CanApproach(origin, destination))
        {
            waypoints.Clear();
            nextSearchTime = 0f;
            return (destination - origin).normalized;
        }

        while (waypoints.Count > 0 &&
               (waypoints[waypoints.Count - 1] - origin).sqrMagnitude <= WaypointTolerance * WaypointTolerance)
        {
            waypoints.RemoveAt(waypoints.Count - 1);
        }

        bool hasWaypoint = waypoints.Count > 0 && IsClear(origin, waypoints[waypoints.Count - 1], true);
        bool targetMoved = (destination - lastDestination).sqrMagnitude >= cellSize * cellSize;
        if (!hasWaypoint)
        {
            Vector2 escape = GetEscapeDirection();
            if (escape != Vector2.zero)
            {
                waypoints.Clear();
                return escape;
            }
        }

        if (Time.fixedTime >= nextSearchTime && (!hasWaypoint || targetMoved))
        {
            FindPath(origin, destination);
            lastDestination = destination;
            nextSearchTime = Time.fixedTime + RepathInterval;
        }

        // Skip grid points when the collider fits along a straight shortcut.
        while (waypoints.Count > 1 && IsClear(origin, waypoints[waypoints.Count - 2], true))
        {
            waypoints.RemoveAt(waypoints.Count - 1);
        }

        if (waypoints.Count == 0 || !IsClear(origin, waypoints[waypoints.Count - 1], true))
            return Vector2.zero;

        return (waypoints[waypoints.Count - 1] - origin).normalized;
    }

    private bool CanApproach(Vector2 origin, Vector2 destination)
    {
        Vector2 offset = destination - origin;
        float distance = offset.magnitude;
        if (distance < 0.001f)
            return true;

        // A player next to a wall can be reached without fitting the whole enemy at the player's center.
        Vector2 approach = destination - offset * (Mathf.Min(distance, radius) / distance);
        return IsClear(origin, approach, true) && IsClear(approach, destination, false);
    }

    private bool IsClear(Vector2 from, Vector2 to, bool useFootprint)
    {
        Vector2 offset = to - from;
        float distance = offset.magnitude;
        if (distance < 0.001f)
            return true;

        Vector2 direction = offset / distance;
        if (!useFootprint)
            Physics2D.Raycast(from, direction, obstacleFilter, hits, distance);
        else if (footprint is CapsuleCollider2D capsule)
            Physics2D.CapsuleCast(from + shapeOffset, size, capsule.direction, 0f, direction, obstacleFilter, hits, distance);
        else
            Physics2D.CircleCast(from + shapeOffset, radius, direction, obstacleFilter, hits, distance);

        foreach (RaycastHit2D hit in hits)
        {
            // Houses, trees and props are solid level geometry. Actors and pickups move independently.
            if (IsStaticObstacle(hit.collider))
                return false;
        }
        return true;
    }

    private Vector2 GetEscapeDirection()
    {
        if (footprint == null)
            return Vector2.zero;

        // A cast that starts inside an obstacle cannot find a valid first grid edge.
        footprint.Overlap(obstacleFilter, overlaps);
        Vector2 correction = Vector2.zero;
        foreach (Collider2D obstacle in overlaps)
        {
            if (!IsStaticObstacle(obstacle))
                continue;

            ColliderDistance2D separation = footprint.Distance(obstacle);
            if (separation.isValid && separation.distance < -0.025f)
                correction += separation.normal * separation.distance;
        }

        return correction.normalized;
    }

    private static bool IsStaticObstacle(Collider2D collider)
    {
        Rigidbody2D attachedBody = collider.attachedRigidbody;
        return attachedBody == null || attachedBody.bodyType == RigidbodyType2D.Static;
    }

    private void FindPath(Vector2 origin, Vector2 destination)
    {
        waypoints.Clear();
        Vector2Int start = Vector2Int.zero;
        Vector2Int closest = start;
        float closestDistance = Vector2.Distance(origin, destination);
        costs.Clear();
        parents.Clear();
        visited.Clear();
        queuedCells.Clear();
        frontier.Clear();
        costs.Add(start, 0f);
        queuedCells.Add(start);
        frontier.Add((closestDistance, 0));

        while (frontier.Count > 0 && visited.Count < MaxSearchCells)
        {
            var entry = frontier.Min;
            frontier.Remove(entry);
            Vector2Int cell = queuedCells[entry.Order];
            if (!visited.Add(cell))
                continue;

            Vector2 position = origin + (Vector2)cell * cellSize;
            float remaining = Vector2.Distance(position, destination);
            if (remaining < closestDistance)
            {
                closest = cell;
                closestDistance = remaining;
            }
            if (CanApproach(position, destination))
            {
                closest = cell;
                break;
            }

            foreach (Vector2Int step in Neighbors)
            {
                Vector2Int next = cell + step;
                float cost = costs[cell] + ((Vector2)step).magnitude * cellSize;
                if (visited.Contains(next) || (costs.TryGetValue(next, out float previous) && cost >= previous))
                    continue;

                Vector2 nextPosition = origin + (Vector2)next * cellSize;
                if (!IsClear(position, nextPosition, true))
                    continue;

                costs[next] = cost;
                parents[next] = cell;
                frontier.Add((cost + Vector2.Distance(nextPosition, destination), queuedCells.Count));
                queuedCells.Add(next);
            }
        }

        // Store the path in reverse order so the next point can be removed from the end.
        for (Vector2Int cell = closest; cell != start; cell = parents[cell])
            waypoints.Add(origin + (Vector2)cell * cellSize);
    }
}

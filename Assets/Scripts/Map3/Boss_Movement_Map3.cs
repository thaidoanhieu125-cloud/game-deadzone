// Keeps the original script reference in Unity recovery scenes.
public class Boss_Movement_Map3 : EnemyMovement
{
    protected override void Awake()
    {
        isBoss = true;
        base.Awake();
    }
}

// Keeps the original script reference in Unity recovery scenes.
public class Enemy_Movement : EnemyMovement
{
    protected override void Awake()
    {
        growlRange = 5.5f;
        growlVolume = 0.2f;
        minGrowlDelay = 1f;
        maxGrowlDelay = 2.5f;
        base.Awake();
    }
}

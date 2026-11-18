using UnityEngine;

public class EnemyMovement : Stats
{
    public float speed = 2f;
    public float jumpForce = 3f;

    private void Start()
    {
        SetStat(ref speed, 0);
        SetStat(ref jumpForce, 0);
    }
}

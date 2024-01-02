using UnityEngine;

public class DistanceAttack : AttackBase
{
    [SerializeField] private float snowballSpeed = 1;
    [SerializeField] private float attackBetweenSeconds = 2;
    [SerializeField] private float maxDegrees = 30;
    [SerializeField] private Rigidbody2D snowball;


    protected override void Start()
    {
        Init(attackBetweenSeconds, maxDegrees);
        base.Start();
    }

    private void Update()
    {
        var ret = Return(out var a, out var b, out var isRightAngle, out var flip);

        if (!enemy.InRadius(a, b))
            enemy.state = EnemyState.Walk;

        if (ret)
            return;

        flipper.FlipX = flip;

        var sb = Instantiate(snowball);
        sb.position = transform.position;
        sb.velocity = (a - b).normalized * snowballSpeed;
        sb.GetComponent<SpriteRenderer>().flipX = flipper.FlipX;

        previousTime = Time.time;
    }
}
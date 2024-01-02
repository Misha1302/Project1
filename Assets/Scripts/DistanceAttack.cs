using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
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
        var r = Return(out var a, out var b);
     
        enemy.TryChangeState(true);
        
        if (r)
            return;

        var sb = Instantiate(snowball);
        sb.position = transform.position;
        sb.velocity = (a - b).normalized * snowballSpeed;
        sb.GetComponent<SpriteRenderer>().flipX = sp.flipX;

        previousTime = Time.time;
    }
}
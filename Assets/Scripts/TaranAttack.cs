using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Damage))]
public class TaranAttack : AttackBase
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float calldown = 6;
    [SerializeField] private float maxDegrees = 10;

    private bool _attack;
    private float _direction;

    protected override void Start()
    {
        Init(calldown, maxDegrees);
        base.Start();
    }

    private void Update()
    {
        if (_attack)
        {
            Attack();
            return;
        }

        if (Return())
            return;

        _attack = true;
        _direction = sp.flipX ? 1 : -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_attack)
            return;

        enemy.state = EnemyState.Wait;

        StartCoroutine(WalkAfterCalldown());

        previousTime = Time.time;
        _attack = false;
    }

    private IEnumerator WalkAfterCalldown()
    {
        yield return new WaitForSeconds(calldown);
        enemy.state = EnemyState.Walk;
    }

    private void Attack()
    {
        enemy.state = EnemyState.Attack;
        transform.Translate(Vector3.right * (_direction * speed * Time.deltaTime));
    }
}
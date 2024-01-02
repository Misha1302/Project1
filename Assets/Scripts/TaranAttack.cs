using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Damage))]
public class TaranAttack : AttackBase
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float cooldown = 6;
    [SerializeField] private float maxDegrees = 10;

    private bool _attacking;
    private bool _coroutineRun;
    private float _direction;

    protected override void Start()
    {
        Init(0, maxDegrees);
        base.Start();
    }

    private void Update()
    {
        if (enemy.state == EnemyState.Wait)
            _attacking = false;

        if (enemy.state != EnemyState.Attack)
            return;

        if (!_attacking)
        {
            var a = transform.position;
            var b = GameManager.Instance.PlayerController.transform.position;

            if (a.Degrees(b) > maxDegrees)
            {
                enemy.state = EnemyState.Walk;
                return;
            }

            flipper.FlipX = a.x < b.x;
            _direction = flipper.FlipX ? 1 : -1;
            _attacking = true;
        }

        Attack();
    }

    private void OnCollisionEnter2D(Collision2D other) => OnCollision(other);
    private void OnCollisionStay2D(Collision2D other) => OnCollision(other);

    private void OnCollision(Collision2D other)
    {
        if (other.transform.TryGetComponent<GroundTag>(out _))
            return;

        if (!_attacking)
            return;

        _attacking = false;

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        if (_coroutineRun)
            yield break;

        _coroutineRun = true;

        enemy.state = EnemyState.Wait;
        yield return new WaitForSeconds(cooldown);
        enemy.state = EnemyState.Walk;

        _attacking = _coroutineRun = false;
    }

    private void Attack()
    {
        transform.Translate(Vector3.right * (_direction * speed * Time.deltaTime), Space.World);
    }
}
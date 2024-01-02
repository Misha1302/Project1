using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AttackBase))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float distanceToAttack = 6;
    [SerializeField] private float multiplierOnAttack = 1.3f;

    [HideInInspector] public EnemyState state = EnemyState.Walk;
    [HideInInspector] public Rigidbody2D rb2D;

    private AttackBase _attack;
    private EnemyMovement _enemyMovement;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        GetComponent<AttackBase>().SetEnemy(this);
        GetComponent<EnemyMovement>().SetEnemy(this);
    }

    private void Update()
    {
        print(state);
    }

    public bool InRadius(Vector3 a, Vector3 b) =>
        Math.Abs(a.x - b.x) <= distanceToAttack * (state == EnemyState.Attack ? multiplierOnAttack : 1);

    public void Stun(float stunTime)
    {
        StartCoroutine(StunCoroutine(stunTime));
    }

    private IEnumerator StunCoroutine(float stunTime)
    {
        var f = TryGetComponent<Damage>(out var damage);
        if (f) damage.enabled = false;

        state = EnemyState.Wait;
        rb2D.velocity = rb2D.velocity.WithX(0);
        yield return new WaitForSeconds(stunTime);
        state = EnemyState.Walk;

        if (f) damage.enabled = true;
    }
}
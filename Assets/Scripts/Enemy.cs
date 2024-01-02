using System;
using UnityEngine;

[RequireComponent(typeof(AttackBase))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float distanceToAttack = 7;
    [SerializeField] private float multiplierOnAttack = 1.3f;

    [HideInInspector] public EnemyState state = EnemyState.Walk;

    private AttackBase _attack;
    private EnemyMovement _enemyMovement;

    private bool IsAttacking => state == EnemyState.Attack;

    private void Start()
    {
        _attack = GetComponent<AttackBase>();
        _enemyMovement = GetComponent<EnemyMovement>();

        _attack.SetEnemy(this);
        _enemyMovement.SetEnemy(this);
    }

    public void TryChangeState(bool c)
    {
        var a = transform.position;
        var b = GameManager.Instance.PlayerController.transform.position;

        state = a.Degrees(b) <= _attack.MaxDegrees && InRadius(a, b) && c ? EnemyState.Attack : EnemyState.Walk;
    }

    public bool InRadius(Vector3 a, Vector3 b) =>
        Math.Abs(a.x - b.x) <= distanceToAttack * (IsAttacking ? multiplierOnAttack : 1);
}
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class AttackBase : MonoBehaviour
{
    private double _calldown;

    protected Enemy enemy;
    protected float previousTime;
    protected SpriteRenderer sp;
    public double MaxDegrees { get; private set; }


    protected virtual void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        previousTime = Time.time;
    }

    protected void Init(double calldown, double maxDegrees)
    {
        _calldown = calldown;
        MaxDegrees = maxDegrees;
    }

    protected bool Return() => Return(out _, out _);

    protected bool Return(out Vector3 a, out Vector3 b)
    {
        a = b = Vector3.positiveInfinity;

        if (enemy.state != EnemyState.Attack)
            return true;

        a = GameManager.Instance.PlayerController.transform.position;
        b = transform.position;
        sp.flipX = b.x < a.x;

        if (Time.time < previousTime + _calldown)
            return true;

        if (a.Degrees(b) > MaxDegrees)
        {
            enemy.state = EnemyState.Walk;
            return true;
        }

        return false;
    }

    public void SetEnemy(Enemy e)
    {
        enemy = e;
    }
}
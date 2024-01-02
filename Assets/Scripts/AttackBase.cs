using UnityEngine;

[RequireComponent(typeof(ObjectFlipper))]
public abstract class AttackBase : MonoBehaviour
{
    private double _cooldown;

    protected Enemy enemy;
    protected ObjectFlipper flipper;
    protected float previousTime;
    private double _maxDegrees;


    protected virtual void Start()
    {
        flipper = GetComponent<ObjectFlipper>();
        previousTime = Time.time;
    }

    protected void Init(double cooldown, double maxDegrees)
    {
        _cooldown = cooldown;
        _maxDegrees = maxDegrees;
    }

    protected bool Return(out Vector3 a, out Vector3 b, out bool isRightAngle, out bool flip)
    {
        a = GameManager.Instance.PlayerController.transform.position;
        b = transform.position;
        isRightAngle = a.Degrees(b) <= _maxDegrees;
        flip = b.x < a.x;

        if (enemy.state != EnemyState.Attack)
            return true;

        if (Time.time < previousTime + _cooldown)
            return true;

        if (!isRightAngle)
            return true;

        return false;
    }

    public void SetEnemy(Enemy e)
    {
        enemy = e;
    }
}
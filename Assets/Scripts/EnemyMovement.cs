using System;
using UnityEngine;

[RequireComponent(typeof(ObjectFlipper))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private float movementSpeed = 1;

    private Transform _destination;
    private Enemy _enemy;
    private ObjectFlipper _objectFlipper;
    
    private void Start()
    {
        _objectFlipper = GetComponent<ObjectFlipper>();
        _destination = point2;
    }

    private void Update()
    {
        // ReSharper disable Unity.InefficientPropertyAccess
        if (_enemy.state != EnemyState.Walk)
            return;

        var a = transform.position;
        var b = GameManager.Instance.PlayerController.transform.position;
        var right = a.x < _destination.position.x;

        if (_enemy.InRadius(a, b) && (right ? a.x < b.x : b.x < a.x))
        {
            _enemy.state = EnemyState.Attack;
            return;
        }

        _objectFlipper.FlipX = right;

        var vel = _enemy.rb2D.velocity;
        vel.x = Math.Sign(_destination.position.x - a.x) * movementSpeed;
        _enemy.rb2D.velocity = vel;

        ChoosePoint();
    }

    private void ChoosePoint()
    {
        if (transform.position.x < point1.position.x || Math.Abs(transform.position.x - point1.position.x) < 0.1f)
            _destination = point2;
        else if (transform.position.x > point2.position.x || Math.Abs(transform.position.x - point2.position.x) < 0.1f)
            _destination = point1;
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemy = enemy;
    }
}
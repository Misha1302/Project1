using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private float movementSpeed = 1;

    private Transform _destination;
    private Enemy _enemy;

    private SpriteRenderer _sp;

    private void Start()
    {
        _destination = point2;
        _sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // ReSharper disable Unity.InefficientPropertyAccess
        if (_enemy.state != EnemyState.Walk)
            return;


        var right = _destination.position.x > transform.position.x;
        _sp.flipX = right;


        var a = transform.position;
        var b = GameManager.Instance.PlayerController.transform.position;

        transform.position =
            Vector3.MoveTowards(a, _destination.position, movementSpeed * Time.deltaTime);

        _enemy.TryChangeState(right ? a.x < b.x : a.x > b.x);

        ChoosePoint();
    }

    private void ChoosePoint()
    {
        if (!transform.position.Eq(_destination.position))
            return;

        _destination = _destination == point1 ? point2 : point1;
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemy = enemy;
    }
}
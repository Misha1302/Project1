using UnityEngine;
using static Direction;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(ObjectFlipper))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private GroundChecker groundChecker;
    private ObjectFlipper _flipper;

    private Rigidbody2D _rb2D;

    private void Start()
    {
        _flipper = GetComponent<ObjectFlipper>();
        _rb2D = GetComponent<Rigidbody2D>();
        GameManager.Instance.InputController.onMove.AddListener(Move);
    }

    private void Move(Direction dir)
    {
        var vel = _rb2D.velocity;


        // if no left and right or have left and right
        if ((!dir.Has(Left) && !dir.Has(Right)) || (dir.Has(Left) && dir.Has(Right)))
        {
            vel.x = 0;
        }
        else // if left or right
        {
            if (dir.Has(Left)) // if left
            {
                vel.x = -speed;
                _flipper.FlipX = true;
            }
            else if (dir.Has(Right)) // if right
            {
                vel.x = speed;
                _flipper.FlipX = false;
            }
        }

        // if up and isGrounded
        if ((dir.Has(Up) && groundChecker.IsGrounded) || groundChecker.NeedToJump)
            vel.y = jumpForce;

        _rb2D.velocity = vel;
    }
}
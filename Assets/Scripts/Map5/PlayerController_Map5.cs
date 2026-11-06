using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_Map5 : MonoBehaviour
{
    public Transform body;
    public Animator anima;
    public InputAction MoveAction;
    public Rigidbody2D rigidbody2d;
    public float speed = 5;

    void Start()
    {
        // Kích hoạt Input để đảm bảo nhân vật có thể di chuyển
        if (MoveAction != null)
        {
            MoveAction.Enable();
        }

        rigidbody2d = GetComponent<Rigidbody2D>();

        if (body != null)
        {
            anima = body.GetComponent<Animator>();
        }
    }

    void OnDisable()
    {
        // Luôn disable input khi object bị tắt để tránh lỗi
        if (MoveAction != null)
        {
            MoveAction.Disable();
        }
    }

    void Update()
    {
        if (MoveAction == null) return;

        Vector2 move = MoveAction.ReadValue<Vector2>();

        rigidbody2d.linearVelocity = move * speed;

        if (anima != null)
        {
            anima.SetBool("isMoving", move != Vector2.zero);

            float moveX = 0;
            float moveY = 0;

            if (Mathf.Abs(move.y) > 0.1f)
            {
                moveY = move.y > 0 ? 1 : -1;
            }
            else if (Mathf.Abs(move.x) > 0.1f)
            {
                moveX = move.x > 0 ? 1 : -1;
            }

            anima.SetFloat("moveX", moveX);
            anima.SetFloat("moveY", moveY);
        }
    }
}
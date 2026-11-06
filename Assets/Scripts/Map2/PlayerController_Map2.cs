using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_Map2 : MonoBehaviour
{
    public Transform body;
    public Animator anima;
    public InputAction MoveAction;
    public Rigidbody2D rigidbody2d;
    public float speed = 5;

    void Awake()
    {
        MoveAction.Enable(); // Kích hoạt nút bấm ngay lập tức khi đối tượng được sinh ra
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
        anima = body.GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();

        rigidbody2d.linearVelocity = move * speed;

        anima.SetBool("isMoving", move != Vector2.zero);

        float moveX = 0;
        float moveY = 0;

        // 👉 ƯU TIÊN UP / DOWN TRƯỚC
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
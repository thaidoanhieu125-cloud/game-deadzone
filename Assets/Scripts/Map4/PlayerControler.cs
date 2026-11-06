using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public Transform body;
    public Animator anima;
    public InputAction MoveAction;
    public Rigidbody2D rigidbody2d;
    public AudioSource audioSourceFootstep;
    public AudioClip runClip;
    public float speed = 5;

    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        anima = body.GetComponent<Animator>();
        audioSourceFootstep.clip = runClip;
    }

    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();
        if (move != Vector2.zero)
        {
            if (!audioSourceFootstep.isPlaying)
            {
                audioSourceFootstep.Play();
            }
        }
        else
        {
            if (audioSourceFootstep.isPlaying)
            {
                audioSourceFootstep.Stop();
            }
        }
        rigidbody2d.linearVelocity = move * speed;

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
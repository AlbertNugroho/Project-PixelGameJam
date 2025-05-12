using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public InputActionReference move;
    public Animator playeranim;
    public float speed = 5f;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public SpriteRenderer sp;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move.action.Enable();
    }

    private void OnDisable()
    {
        move.action.Disable();
    }

    void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>();

        HandleAnimationAndFlip(moveDirection);
    }

    private void FixedUpdate()
    {
        if (!GetComponent<Dash>().isDashing)
        {
            rb.linearVelocity = moveDirection.normalized * speed;
        }
    }

    private void HandleAnimationAndFlip(Vector2 dir)
    {
        playeranim.SetBool("walking", dir != Vector2.zero);

        if (dir.x < 0)
            sp.flipX = true;
        else if (dir.x > 0)
            sp.flipX = false;
    }
}

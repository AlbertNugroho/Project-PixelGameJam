using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Dash : MonoBehaviour
{
    public InputActionReference move;
    public InputActionReference dash;

    public float dashSpeed = 10f;
    public float dashTime = 0.15f;
    public float dashCooldown = 0.25f;

    private float dashTimer;
    private float dashCooldownTimer;
    public bool isDashing;
    private Vector2 dashDirection;

    private Rigidbody2D rb;
    public Animator playeranim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dash.action.performed += OnDash;
    }

    private void OnEnable()
    {
        move.action.Enable();
        dash.action.Enable();
    }

    private void OnDisable()
    {
        move.action.Disable();
        dash.action.Disable();
    }

    private void Update()
    {
        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                StopDash();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
        }
    }

    private void OnDash(InputAction.CallbackContext ctx)
    {
        if (dashCooldownTimer > 0f || isDashing)
            return;

        Vector2 input = move.action.ReadValue<Vector2>();

        if (input == Vector2.zero)
            return;

        dashDirection = input.normalized;
        playeranim.SetTrigger("Dash");
        isDashing = true;
        dashTimer = dashTime;
        dashCooldownTimer = dashCooldown;
    }

    private void StopDash()
    {
        isDashing = false;
        rb.linearVelocity = Vector2.zero;
    }
}

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
    }

    private void Update()
    {
        if(dash.action.WasPerformedThisFrame())
        {
            OnDash();
        }
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

    private void OnDash()
    {
        if (dashCooldownTimer > 0f || isDashing)
            return;

        Vector2 input = move.action.ReadValue<Vector2>();

        if (input == Vector2.zero)
            return;

        dashDirection = input.normalized;
        if (playeranim != null)
        {
            AudioManager.instance.PlayClip(AudioManager.instance.dashfx);
            playeranim.SetTrigger("Dash");
        }
        else
        {
            Debug.LogWarning("Animator is missing or destroyed.");
        }
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

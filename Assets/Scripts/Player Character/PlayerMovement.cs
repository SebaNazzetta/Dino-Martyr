using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;

    public float acceleration = 1f;
    [Range(0, 1f)] public float groundDecay = 0.6f;
    public float maxXSpeed = 3f;
    public float jumpSpeed = 6f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;
    public float airControlFactor = 0.8f;  // 🔹 Reduce el control en el aire para evitar saltos inconsistentes

    private bool grounded;
    private float xInput;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    public Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        CheckInput();
        HandleJump();
        ApplyBetterJumpPhysics();
    }

    private void FixedUpdate()
    {
        HandleXMovement();
        CheckGround();
        ApplyFriction();
    }

    void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    void HandleXMovement()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            float increment = xInput * acceleration;

            // 🔹 Diferente control en el aire para que no afecte el salto
            float controlFactor = grounded ? 1f : airControlFactor;

            float newSpeed = Mathf.Clamp(body.velocity.x + (increment * controlFactor), -maxXSpeed, maxXSpeed);
            body.velocity = new Vector2(newSpeed, body.velocity.y);

            FaceInput();

            if (grounded)
            {
                anim.SetBool("walking", true);
            }
        }
    }

    void FaceInput()
    {
        float direction = Mathf.Sign(xInput);
        if (direction != 0)
        {
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    void HandleJump()
    {
        if (grounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            // 🔹 SOLUCIÓN: Restablecemos completamente la velocidad antes de saltar
            body.velocity = new Vector2(body.velocity.x * 0.5f, 0);
            body.velocity += Vector2.up * jumpSpeed;

            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
        }
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void ApplyFriction()
    {
        if (grounded && xInput == 0)
        {
            body.velocity *= groundDecay;
            anim.SetBool("walking", false);
        }
    }

    void ApplyBetterJumpPhysics()
    {
        if (body.velocity.y < 0)
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (body.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}

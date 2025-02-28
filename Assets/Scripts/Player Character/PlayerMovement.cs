using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;
    private BoxCollider2D _collision;
    private PlayerCollision _playerCollision;
    private Animator _anim;

    public float acceleration = 1f;
    [Range(0, 1f)] public float groundDecay = 0.6f;
    public float maxXSpeed = 3f;
    public float jumpSpeed = 6f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;
    public float airControlFactor = 0.8f;

    public bool inWater = false; // Variable para detectar si está en el agua

    [HideInInspector] public bool grounded;
    private float xInput;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _collision = GetComponent<BoxCollider2D>();
        _playerCollision = GetComponent<PlayerCollision>();
    }

    void Update()
    {
        CheckInput();
        HandleJump();
        ApplyBetterJumpPhysics();
    }

    private void FixedUpdate()
    {
        grounded = _playerCollision.IsGrounded();
        HandleXMovement();
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
            float speedModifier = inWater ? 0.5f : 1f; // Reducimos la velocidad en agua
            float increment = xInput * acceleration * speedModifier;

            float controlFactor = grounded ? 1f : airControlFactor;
            float newSpeed = Mathf.Clamp(_body.velocity.x + (increment * controlFactor), -maxXSpeed * speedModifier, maxXSpeed * speedModifier);
            _body.velocity = new Vector2(newSpeed, _body.velocity.y);

            FaceInput();

            if (grounded)
            {
                _anim.SetBool("walking", true);
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
            _body.velocity = new Vector2(_body.velocity.x * 0.5f, 0);
            _body.velocity += Vector2.up * jumpSpeed;

            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
        }
    }

    void ApplyFriction()
    {
        if (grounded && xInput == 0)
        {
            _body.velocity *= groundDecay;
            _anim.SetBool("walking", false);
        }
    }

    void ApplyBetterJumpPhysics()
    {
        if (_body.velocity.y < 0)
        {
            _body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_body.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Die()
    {
        _collision.enabled = false;
        _anim.SetTrigger("die");
        _body.bodyType = RigidbodyType2D.Static;
        this.enabled = false;
    }
}

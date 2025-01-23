using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;

    public float acceleration;
    [Range(0, 1f)]
    public  float  groundDecay;
    public float maxXSpeed;

    public float jumpSpeed;

    public bool grounded;
    float xInput;

    public Animator anim;

    void Awake() 
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        CheckInput();
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleXMovement();
        CheckGround();
        ApplyFriction();
    }
    
    void CheckInput()
    {
        xInput = Input.GetAxis("Horizontal");
    }

    void HandleXMovement()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            //increment velocity by our acceleration, then clamp within max
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -maxXSpeed, maxXSpeed);
            body.velocity = new Vector2(newSpeed, body.velocity.y);

            FaceInput();

            if(grounded)
            {
                anim.SetBool("walking", true);
            }

        }
    }

    void FaceInput()
    {
        float direction = Mathf.Sign(xInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, 0); // Reset y velocity to 0
            body.velocity += new Vector2(0, jumpSpeed); // Then apply jump speed
        }
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void ApplyFriction()
    {
        if(grounded && xInput == 0)
        {
            body.velocity *= groundDecay;
            anim.SetBool("walking", false);
        }
    }

}

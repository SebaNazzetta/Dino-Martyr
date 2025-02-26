using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Rigidbody2D _body;
    private bool canDoubleJump;

    public float doubleJumpSpeed = 5f;

    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_playerMovement == null || _body == null)
            return;

        if (_playerMovement.grounded)
        {
            canDoubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && canDoubleJump)
        {
            _body.velocity = new Vector2(_body.velocity.x, doubleJumpSpeed);
            canDoubleJump = false;
        }
    }
}

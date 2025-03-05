using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Vector2 _groundBoxSize;
    [SerializeField] private Vector2 _groundBoxOffset;
    [SerializeField] private Vector2 _headBoxSize;
    [SerializeField] private Vector2 _headBoxOffset;
    [SerializeField] private LayerMask _groundMask;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    public bool IsGrounded()
    {
        bool isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x,
            gameObject.transform.position.y - _groundBoxOffset.y), _groundBoxSize, 0f, _groundMask);

        return isGrounded;
    }

    public bool isGrabed()
    {
        // Calcula la posición del OverlapBox
        Vector2 grabBoxPosition = new Vector2(
            gameObject.transform.position.x + (_headBoxOffset.x * transform.localScale.x),
            gameObject.transform.position.y - _headBoxOffset.y
        );

        // Depura la posición y el tamaño del OverlapBox
        Debug.Log("Grab Box Position: " + grabBoxPosition);
        Debug.Log("Grab Box Size: " + _headBoxSize);

        // Verifica si hay una colisión
        bool isGrabed = Physics2D.OverlapBox(grabBoxPosition, _headBoxSize, 0f, _groundMask);

        // Depura el resultado de la colisión
        Debug.Log("Is Grab Detected: " + isGrabed);

        return isGrabed;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // Draw a box for ground
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x
         + (_groundBoxOffset.x * transform.localScale.x),
            gameObject.transform.position.y - _groundBoxOffset.y), _groundBoxSize);

        // Draw a box for head
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x
        + (_headBoxOffset.x * transform.localScale.x),
            gameObject.transform.position.y - _headBoxOffset.y), _headBoxSize);
    }
#endif
}
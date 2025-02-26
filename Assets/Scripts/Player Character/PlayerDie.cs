using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private GameObject _skull;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _playerMovement.Die();
            FindObjectOfType<DeadTransition>().StartShrink();
        }
    }
    
    public void Die()
    {
        var skull = Instantiate(_skull, this.transform.localPosition, Quaternion.identity);
        skull.transform.localScale = new Vector2(this.transform.localScale.x, 1f);
        Destroy(gameObject);
    }



}

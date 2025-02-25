using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private GameObject _skull;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Die();
        }
    }
    
    private void Die()
    {
        _skull.SetActive(true);
        _skull.transform.parent = null;
        this.gameObject.SetActive(false);
    }
}

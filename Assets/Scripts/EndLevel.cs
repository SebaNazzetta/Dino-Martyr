using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private GameObject _winPlayerPosition;

    private void Awake()
    {
        _winPlayerPosition.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = _winPlayerPosition.transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            MonoBehaviour[] scripts = collision.gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }

            GetComponent<Animator>().SetTrigger("win");
            collision.gameObject.GetComponentInChildren<Animator>().SetTrigger("win");
            Debug.Log("Level Complete");
            // Add code to show ui to continue next level
        }
    }
}

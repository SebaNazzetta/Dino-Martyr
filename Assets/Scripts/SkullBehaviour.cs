using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if(collision.gameObject.layer == 3) // 3 is the layer of the LAVA
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.841495f, 0.3373953f);
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0.01691365f, 0.1169016f);
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 0.5f, gameObject.transform.localPosition.z);

        }

        if(collision.gameObject.layer == 8) // 8 is the layer of the SPIKES
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

        }*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) // 8 is the layer of the SPIKES
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

}

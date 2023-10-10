using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2D;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().isBig == true)
            {
                Debug.Log("checkShrink");
                collision.gameObject.GetComponent<Player>().isBig = false;
                collision.gameObject.GetComponent<Player>().Shrink();
            }
            else
            {
                GameManager.instance.live--;
            }
        }
        if(collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Coin")
        {
            Destroy(gameObject);
        }


    }
}

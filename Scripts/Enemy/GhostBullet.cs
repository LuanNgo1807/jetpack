using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        Vector3 direction = player.transform.position - transform.position;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        DestroyObject(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
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
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Ground")){
            Destroy(gameObject);
        }
    }
}

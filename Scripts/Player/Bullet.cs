using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player player;
    public Rigidbody2D rb;
    public Vector2 velocity;
    public bool isFaceRight;

    public float distance;

    public float forceX;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        isFaceRight = player.faceRight;
        //velocity = rb.velocity;
        if (isFaceRight)
        {
            rb.AddForce(new Vector2(forceX, 0), ForceMode2D.Impulse);
            velocity = new Vector2(20, -15);
        }
        else
        {
            rb.AddForce(new Vector2(-forceX, 0), ForceMode2D.Impulse);
            velocity = new Vector2(-20, -15);
        }
        Invoke("Explore", 2);
    }


    private void Update()
    {
        if (rb.velocity.y < velocity.y)
        {
            rb.velocity = velocity;
        }

/*        RaycastHit2D rayHit1 = Physics2D.Raycast(transform.position, Vector2.right, distance);
        Debug.DrawRay(transform.position, Vector2.right * distance);
        if (rayHit1.collider != null && rayHit1.collider.tag == "Ground");
        {
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isFaceRight)
        {
            rb.velocity = new Vector2(velocity.x, -velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(velocity.x, -velocity.y);
        }
        if(collision.gameObject.tag == "Trap")
        {
            gameObject.SetActive(false);
        }
        if(collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ExploreCollider")
        {
            gameObject.SetActive(false);
        }
    }

    public void Explore()
    {
        gameObject.SetActive(false);
    }
}

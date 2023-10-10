using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float force;
    public LayerMask layerMask;
    public float speed;
    private bool fall = false;



    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {


        
    }
    public void Move()
    {
        rb.velocity = Vector2.left * speed;
    }


    public void CheckFlip()
    {
        RaycastHit2D rayHit1 = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, layerMask);
        RaycastHit2D rayHit2 = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, layerMask);


        if (rayHit1.collider != null && rayHit1.collider.tag == "Ground")
        {
            speed = 4;
        }
        if (rayHit2.collider != null && rayHit2.collider.tag == "Ground")
        {
            speed = -4;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" && collision.contacts[0].normal.y > 0) || (collision.gameObject.tag == "Box" && collision.contacts[0].normal.y > 0))
        {
            fall = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().isBig == false)
            {
                collision.gameObject.GetComponent<Player>().Grow();
            }
            collision.gameObject.GetComponent<Animator>().SetBool("hit", true);

            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (fall)
        {
            CheckFlip();
            Move();
        }
    }

}

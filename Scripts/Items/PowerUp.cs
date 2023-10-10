using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Rigidbody2D rb;
    public float force;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
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

}

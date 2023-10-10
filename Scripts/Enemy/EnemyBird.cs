using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBird : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    public float speed = 3;
    public Animator animator;
    public bool dead = false;
    public bool faceRight = false;
    public Player player;
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        currentPoint = pointA.transform;
    }

    private void Update()
    {
        
        if (!dead)
        {
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (currentPoint == pointB.transform && Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {

                currentPoint = pointA.transform;
                Vector2 localScale = gameObject.transform.localScale;
                localScale.x *= -1;
                gameObject.transform.localScale = localScale;
            }

            if (currentPoint == pointA.transform && Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                currentPoint = pointB.transform;
                Vector2 localScale = gameObject.transform.localScale;
                localScale.x *= -1;
                gameObject.transform.localScale = localScale;
            }
        }

    }

   
    
    public void DestroyDelay()
    {
        Invoke("DestroyEnemy", 0.6f);
    }
    public void DelaySetTrigger()
    {
        player.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().isBig == true)
            {
                Debug.Log("checkShrink");
                collision.gameObject.GetComponent<Player>().isBig = false;
                collision.gameObject.GetComponent<Player>().Shrink();
                collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                Invoke("DelaySetTrigger", 0.5f);
            }
            else
            {
                GameManager.instance.live--;
            }

        }
        if (collision.gameObject.tag == "Bullet")
        {
            animator.SetTrigger("Dead");
            circleCollider.isTrigger = true;
            dead = true;
            Invoke("DestroyEnemy", 1f);
        }
    }



    public void FlipFace()
    {
        faceRight = !faceRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }


}

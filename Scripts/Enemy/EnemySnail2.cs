using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySnail2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    public LayerMask layerMask;
    public float speed = 3;
    public Animator animator;
    public bool dead = false;
    public bool faceRight = false;
    public Player player;
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    public bool fall = false;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        currentPoint = pointA.transform;
    }

    private void Update()
    {

        if (!dead && !fall)
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

        if (fall)
        {
            CheckFlip();
            Move();
        }
    }

    public void CheckFlip()
    {
        RaycastHit2D rayHit1 = Physics2D.Raycast(boxCollider.bounds.center, Vector2.right, boxCollider.bounds.extents.x + 2f, layerMask);
        RaycastHit2D rayHit2 = Physics2D.Raycast(boxCollider.bounds.center, Vector2.left, boxCollider.bounds.extents.x + 2f, layerMask);


        if (rayHit1.collider != null && rayHit1.collider.tag == "Ground")
        {
            speed *= -1;
            FlipFace();
        }
        if (rayHit2.collider != null && rayHit2.collider.tag == "Ground")
        {
            speed *= -1;
            FlipFace();
        }
    }
    public void DestroyDelay()
    {
        Invoke("DestroyEnemy", 0.6f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !(collision.contacts[0].normal.y < 0))
        {
            GameManager.instance.live--;
        }
        if (collision.gameObject.tag == "Ground" && collision.contacts[0].normal.y > 0)
        {
            fall = true;
        }

        if (collision.gameObject.tag == "Player" && collision.contacts[0].normal.y < 0)
        {
            animator.SetTrigger("Dead");
            player.Bounce();
            boxCollider.isTrigger = true;
            dead = true;
            Invoke("DestroyEnemy", 0.6f);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            animator.SetTrigger("Dead");
            boxCollider.isTrigger = true;
            dead = true;
            Invoke("DestroyEnemy", 1f);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            speed *= -1;
            FlipFace();
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

    public void Move()
    {
        rb.velocity = Vector2.left * speed;
    }

}

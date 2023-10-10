using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
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
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        currentPoint = pointA.transform;
        rb.AddForce(Vector2.up * 50);
    }
    public float distanceRay;
    private void Update()
    {


        /*        Physics2D.Raycast(transform.position, Vector2.up, distanceRay);

                if (!dead && !fall)
                {
                    if (currentPoint == pointB.transform)
                    {
                        rb.velocity = new Vector2(3, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-3, rb.velocity.y);
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
                }*/
        Move();

    }

    public void CheckFlip()
    {
        RaycastHit2D rayHit1 = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, layerMask);
        RaycastHit2D rayHit2 = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, layerMask);


        if (rayHit1.collider != null && rayHit1.collider.tag == "Ground")
        {
            speed = 3;
            FlipFace();
        }
        if (rayHit2.collider != null && rayHit2.collider.tag == "Ground")
        {
            speed = -3;
            FlipFace();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ReverseSpeedEnemy"))
        {
            speed *= -1;
        }
    }
    public void DestroyDelay()
    {
        Invoke("DestroyEnemy", 0.6f);
    }
    public void DelaySetTrigger()
    {

        //player.gameObject.tag = "Player";
        player.gameObject.GetComponent<Animator>().SetBool("Hit", false);
        //rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !(collision.contacts[0].normal.y < 0))
        {
            GameManager.instance.live = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player" && !(collision.contacts[0].normal.y < 0))
        {

            if (collision.gameObject.GetComponent<Player>().isBig == true)
            {
                collision.gameObject.GetComponent<Player>().isBig = false;
                collision.gameObject.GetComponent<Player>().Shrink();
                //collision.gameObject.tag = "Ground";
                circleCollider.isTrigger = true;
                if (rb.velocity.y >= 0)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                }
                collision.gameObject.GetComponent<Animator>().SetBool("Hit", true);
                Invoke("DelaySetTrigger", 1f);
            }
            else
            {
                GameManager.instance.live--;
            }

        }
        if (collision.gameObject.tag == "Ground" && collision.contacts[0].normal.y > 0)
        {
            fall = true;
            //rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (collision.gameObject.tag == "Player" && collision.contacts[0].normal.y < 0)
        {
            circleCollider.isTrigger = true;
            animator.SetTrigger("Dead");
            player.Bounce();
            dead = true;
            //rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //b.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

            Invoke("DestroyEnemy", 0.25f);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            gameObject.tag = "Untagged";
            circleCollider.isTrigger = true;
            animator.SetTrigger("Dead");
            dead = true;
            Invoke("DestroyEnemy", 0.5f);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            speed *= -1;
            FlipFace();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (fall == true)
            {
                circleCollider.isTrigger = false;
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }
            else
            {
                Invoke("SetIsTrigger", 0.7f);
            }
        }

    }
    public void SetIsTrigger()
    {
        circleCollider.isTrigger = false;
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
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
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

}

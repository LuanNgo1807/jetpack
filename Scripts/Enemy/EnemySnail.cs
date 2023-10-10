using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySnail : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    private BoxCollider2D boxCollider;
    public LayerMask layerMask;
    public float speed = 5;
    public Animator animator;
    public bool dead = false;
    public bool faceRight = true;
    public bool inShell = false;
    public Player player;
    public bool fall = false;
    public bool idleShell = false;
    public bool shellMove = false;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        currentPoint = pointB.transform;
    }

    private void Update()
    {

        if (!dead && !fall)
        {
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(-speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(speed, 0);
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

    public void Move()
    {
        rb.velocity = Vector2.left * speed;
    }
    public void CheckFlip()
    {
        RaycastHit2D rayHit1 = Physics2D.Raycast(boxCollider.bounds.center, Vector2.right, boxCollider.bounds.extents.x + 0.5f, layerMask);
        RaycastHit2D rayHit2 = Physics2D.Raycast(boxCollider.bounds.center, Vector2.left, boxCollider.bounds.extents.x + 0.5f, layerMask);

        if (rayHit1.collider != null && (rayHit1.collider.tag == "Ground"))
        {
            if (inShell)
            {
                speed = 15;
            }
            else
            {
                speed = 3;

            }

            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            gameObject.transform.localScale = localScale;
        }
        if (rayHit2.collider != null && (rayHit2.collider.tag == "Ground"))
        {
            if (inShell)
            {
                speed = -15;
            }
            else
            {
                speed = -3;

            }
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            gameObject.transform.localScale = localScale;
        }
    }

    public void DelaySetTrigger()
    {
        player.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

        player.gameObject.GetComponent<Animator>().SetBool("Hit", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !(collision.contacts[0].normal.y < 0) && idleShell == false)
        {
            if (collision.gameObject.GetComponent<Player>().isBig == true)
            {
                collision.gameObject.GetComponent<Player>().isBig = false;
                collision.gameObject.GetComponent<Player>().Shrink();
                //collision.gameObject.tag = "Ground";
                boxCollider.isTrigger = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                collision.gameObject.GetComponent<Animator>().SetBool("Hit", true);
                Invoke("DelaySetTrigger", 1f);
            }
            else
            {
                GameManager.instance.live = 0;
            }
        }
/*        if (collision.gameObject.tag == "Player" && !(collision.contacts[0].normal.y < 0) && shellMove)
        {
            if (collision.gameObject.GetComponent<Player>().isBig == true)
            {
                collision.gameObject.GetComponent<Player>().isBig = false;
                collision.gameObject.GetComponent<Player>().Shrink();
                //collision.gameObject.tag = "Ground";
                boxCollider.isTrigger = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                collision.gameObject.GetComponent<Animator>().SetBool("Hit", true);
                Invoke("DelaySetTrigger", 0.5f);
            }
            else
            {
                GameManager.instance.live--;
            }
        }*/

        if (collision.gameObject.tag == "Ground")
        {
            fall = true;
            if (transform.localScale.x < 0)
            {
                if (inShell)
                {
                    speed = 15;
                }
                else
                {
                    speed = 3;

                }
            }
            else
            {
                if (inShell)
                {
                    speed = -15;
                }
                else
                {
                    speed = -3;

                }
            }
        }


        if (collision.gameObject.tag == "Player")
        {
            if ((!inShell) && collision.contacts[0].normal.y < 0)
            {
                IdleShell();
                
            }
            else if ((inShell && speed == 0))
            {
                player.Bounce();
                MoveShell();
                Invoke("MoveNormal", 3f);
            }
            else if ((inShell && speed == 15) && collision.contacts[0].normal.y < 0)
            {
                player.Bounce();
                MoveNormal();
            }

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        Invoke("SetIsTrigger", 1);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && shellMove == true && !(collision.contacts[0].normal.y < 0) && player.OnGround())
        {
            GameManager.instance.live = 0;
        }
    }

    public void SetIsTrigger()
    {
        boxCollider.isTrigger = false;
    }

    public void IdleShell()
    {
        idleShell = true;
        shellMove = false;
        animator.SetBool("InShell", true);
        player.Bounce();
        speed = 0;
        inShell = true;
        Invoke("MoveNormal", 5);
    }
    public void DelayMoveShell()
    {
        shellMove = true;
    }
    public void MoveShell()
    {
        idleShell = false;
        Invoke("DelayMoveShell", 0.5f);
        gameObject.tag = "Bullet";
        if (player.faceRight)
        {
            speed = -15;

        }
        else
        {
            speed = 15;

        }
    }
    public void MoveNormal()
    {
        idleShell = false;
        shellMove = false;
        gameObject.tag = "Enemy";
        animator.SetBool("InShell", false);
        if (transform.localScale.x < 0)
        {
            speed = 3;
        }
        else
        {
            speed = -3;
        }
        
        inShell = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    public LayerMask layerMask;
    public Animator animator;
    public bool dead = false;
    public GameObject bulletPlant;
    public Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("Attack", 3, 3);
    }

    private void Update()
    {
        if (dead == false)
        {
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
                boxCollider.isTrigger = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                collision.gameObject.GetComponent<Animator>().SetBool("Hit", true);
                Invoke("DelaySetTrigger", 0.3f);
            }
            else
            {
                GameManager.instance.live--;
            }
        }
        if (collision.gameObject.tag == "Player" && collision.contacts[0].normal.y < 0)
        {
            animator.SetBool("Dead",true);
            player.Bounce();
            boxCollider.isTrigger = true;
            dead = true;
            Invoke("DestroyEnemy", 1f);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            animator.SetTrigger("Dead");
            boxCollider.isTrigger = true;
            dead = true;
            Invoke("DestroyEnemy", 1f);
        }
    }


    public void Attack()
    {
        if(dead == false)
        {
            animator.SetTrigger("Attack");
            Invoke("SpawnBullet", 0.1f);
        }
    }


    public void SpawnBullet()
    {
        Instantiate(bulletPlant,transform.position + new Vector3(0,0.1f,0), Quaternion.identity);
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}

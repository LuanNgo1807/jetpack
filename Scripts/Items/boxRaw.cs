using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class boxRaw : MonoBehaviour
{
    public Player player;
    public Animator animator;
    public BoxCollider2D boxCollider;
    public LayerMask layerMask;
    public GameObject[] boxPrefabs;
    private bool enemyAbove;
    public int live = 1;
    public bool isAnimationPlaying = false;

    void Start()
    {
        enemyAbove = false;
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.contacts[0].normal.y > 0 /*&& SetAniBox()*/)
        {
            player.CheckBoxcast();
            //DestroyBox();
            Vector3 posYPlayer = player.transform.position + new Vector3(0, 0.5f, 0);
            Vector3 posBoxY = transform.position - new Vector3(0, 0.4f, 0);
            float posRequire = Vector3.Distance(posBoxY, posYPlayer);
            if (posRequire < 1.35f)
            {
                //Debug.Log(posRequire);

            }

        }

    }

    public void DestroyBox()
    {
        player.CheckBoxcast();
        if (player.gameObject.GetComponent<Player>().isBig == true)
        {
            Debug.DrawRay(boxCollider.bounds.center, Vector2.down * 10, Color.red);
            animator.SetBool("boxRaw", true);
            Invoke("SetAnimation", 0.1f);
            live = 0;
            if (live == 0)
            {
                player.isJumping2 = false;
                Destroy(gameObject);
                Instantiate(boxPrefabs[0], transform.position, Quaternion.identity);
                Instantiate(boxPrefabs[1], transform.position, Quaternion.identity);
                Instantiate(boxPrefabs[2], transform.position, Quaternion.identity);
                Instantiate(boxPrefabs[3], transform.position, Quaternion.identity);
            }
        }
        else
        {
            Debug.DrawRay(boxCollider.bounds.center, Vector2.down * 10, Color.red);
            animator.SetBool("boxRaw", true);
            Invoke("SetAnimation", 0.1f);
            if (live == 0)
            {

                Destroy(gameObject);
                Instantiate(boxPrefabs[0], transform.position, Quaternion.identity);
                Instantiate(boxPrefabs[1], transform.position, Quaternion.identity);
                Instantiate(boxPrefabs[2], transform.position, Quaternion.identity);
                Instantiate(boxPrefabs[3], transform.position, Quaternion.identity);

            }
            live--;
        }
    }
    public float value;
    public void DestroyEnemy()
    {

    }
    private void OnCollisionStay(Collision collision)
    {
        enemyAbove = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        enemyAbove = false;
    }

    public bool SetAniBoxEnemy()
    {
        RaycastHit2D rayHit3 = Physics2D.Raycast(boxCollider.bounds.center, Vector2.up, boxCollider.bounds.extents.y + 0.5f, layerMask);
        if (rayHit3.collider != null && rayHit3.collider.tag == "Enemy")
        {
            return true;
        }
        else
        {

            return false;
        }

    }
    public bool SetAniBox()
    {
        RaycastHit2D rayHit3 = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + 0.5f, layerMask);
        if (rayHit3.collider != null)
        {
            return true;
        }
        else
        {

            return false;
        }

    }
    public void SetAnimation()
    {
        animator.SetBool("boxRaw", false);
    }
}

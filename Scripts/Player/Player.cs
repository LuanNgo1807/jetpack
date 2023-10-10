using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int moveSpeed = 10;
    public Rigidbody2D rb;
    public bool faceRight = true;
    public bool moveLeft = false;
    public bool moveRight = false;
    public bool jump = false;
    public int value = 0;
    public bool isBig = false;
    public GameObject translateScene;
    public Button TransLateCamera;
    public float distanceX;
    public bool underWater = false;
    
    //public int forceValueMax = 15;
    //public int forceValueMin;

    //public Transform groundCheck;
    public LayerMask layerMask;
    public LayerMask layerMaskEnemy;
    public LayerMask layerMaskEnemy2;
    //public float maxDistance;
    //public Vector2 boxSize;
    public BoxCollider2D boxCollider;
    public Button buttonJump;
    public float time;
    public GameObject bulletPrefebs;
    public Vector2 directionBullet;
    public float valueChange;
    public Animator animator;
    public Animator bigAnimator;
    public int bulletAmount;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public SpriteRenderer spriteSmall;
    public GameObject spriteBig;


    public float jumpForce;
    public float maxJumpingTime = 1f;
    public float jumpingTimer = 0f;
    public float defaultGravity;
    public bool isJumping;
    public float heigh = 10f;



    //Jump demo

    public BoxCollider2D boxCollider2;
    public Rigidbody2D rb2;
    public LayerMask layerMask2;

    public float jumpForce2;
    public float jumpTime2;
    public float jumpCounter2;
    public bool isJumping2;


/*    public GameObject A;
    public GameObject B;
    public bool isDestroy;*/


    void Start()
    {
        //isDestroy = true;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        directionBullet = Vector2.right;
        bulletAmount = 3;
        defaultGravity = rb.gravityScale;
        isBig = false;
    }

    public void CheckBoxcast()
    {
        if(OnGround() == false)
        {
            RaycastHit2D rayHit1 = Physics2D.Raycast(boxCollider.bounds.center + new Vector3(valueChange, 0, 0), Vector2.up, boxCollider.bounds.extents.y + heigh, layerMaskEnemy2);
            RaycastHit2D rayHit2 = Physics2D.Raycast(boxCollider.bounds.center + new Vector3(-valueChange, 0, 0), Vector2.up, boxCollider.bounds.extents.y + heigh, layerMaskEnemy2);
            Debug.DrawRay(boxCollider.bounds.center + new Vector3(valueChange, 0, 0), Vector2.up * 10, Color.green);
            Debug.DrawRay(boxCollider.bounds.center + new Vector3(-valueChange, 0, 0), Vector2.up * 10, Color.green);
            


            if (rayHit1.collider != null && rayHit1.collider.tag == "Enemy" && OnGround() == false)
            {
                GameObject hitObject = rayHit1.collider.gameObject;
                hitObject.gameObject.tag = "Box";
                hitObject.GetComponent<CircleCollider2D>().isTrigger = true;

                hitObject.GetComponent<Enemy>().dead = true;

                hitObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100, ForceMode2D.Impulse);
                //hitObject.GetComponent<Rigidbody2D>().gravityScale = 10;
                hitObject.GetComponent<Animator>().SetTrigger("Dead");
                hitObject.GetComponent<Enemy>().DestroyDelay();
                hitObject.GetComponent<Enemy>().DestroyDelay();
            }

            if (rayHit2.collider != null && rayHit2.collider.tag == "Enemy" && OnGround() == false)
            {
                Debug.Log("check box cast");
                Debug.DrawRay(boxCollider.bounds.center + new Vector3(valueChange, 0, 0), Vector2.up * 10, Color.green);
                Debug.DrawRay(boxCollider.bounds.center + new Vector3(-valueChange, 0, 0), Vector2.up * 10, Color.green);
                GameObject hitObject2 = rayHit2.collider.gameObject;
                hitObject2.gameObject.tag = "Box";
                hitObject2.GetComponent<Enemy>().dead = true;
                hitObject2.GetComponent<CircleCollider2D>().isTrigger = true;

                hitObject2.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100, ForceMode2D.Impulse);
                //hitObject2.GetComponent<Rigidbody2D>().gravityScale = 10;
                hitObject2.GetComponent<Animator>().SetTrigger("Dead");
                hitObject2.GetComponent<Enemy>().DestroyDelay();
            }
        }


        

    }
    public void DebugAB()
    {
        //Debug.Log(A.GetComponent<GetBoxPos>().distance);
    }
    void Update()
    {
        //CheckBoxcast();

        Attack();
        DistanceLimit();
        SetJumpAnimation();
        SetDeadAnimation();
        if (isJumping)
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = defaultGravity;
                if (OnGround())
                {
                    isJumping = false;
                    jumpingTimer = 0;
                }
            }
            else if (rb.velocity.y > 0)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    jumpingTimer += Time.deltaTime;
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (jumpingTimer < maxJumpingTime)
                    {
                        rb.gravityScale = defaultGravity * 2f;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpHolder();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Grow();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Shrink();
        }
    }

    private void FixedUpdate()
    {
        MobileControl();
        MovePlayer();
    }


    public void TransLateCam()
    {
        translateScene.gameObject.SetActive(false);
    }
    public void Grow()
    {
        isBig = true;
        spriteSmall.enabled = false;
        spriteBig.SetActive(true);
    }
    public void Shrink()
    {
        isBig = false;
        spriteSmall.enabled = true;
        spriteBig.SetActive(false);
    }

    public void SetDeadAnimation()
    {
        if (GameManager.instance.live == 0)
        {
            animator.SetBool("Dead", true);
            Invoke("DestroyPlayer", 0.32f);
        }
    }

    public void ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if(Time.frameCount % 4 == 0)
            {
                

            }
        }
    }
    private void JumpTimer()
    {
        if (isJumping)
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = defaultGravity;
                if (OnGround())
                {
                    isJumping = false;
                    jumpingTimer = 0;
                }
            }
            else if (rb.velocity.y > 0)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    jumpingTimer += Time.deltaTime;
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (jumpingTimer < maxJumpingTime)
                    {
                        rb.gravityScale = defaultGravity * 2f;
                    }
                }
            }
        }
    }
    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
    public void SetJumpAnimation()
    {
        if (rb.velocity.y != 0 && !OnGround())
        {
            if(isBig)
            {
                bigAnimator.SetBool("bigJump", true);
            }
            else
            {
                animator.SetBool("Jump", true);
            }
        }
        if (rb.velocity.y == 0 || OnGround())
        {
            if (isBig)
            {
                bigAnimator.SetBool("bigJump", false);
            }
            else
            {
                animator.SetBool("Jump", false);
            }
        }
    }

    public void SetMoveLeft()
    {
        moveLeft = true;
    }
    public void SetMoveRight()
    {
        moveRight = true;
    }
    public void MobileControl()
    {
        if (moveLeft || Input.GetKey(KeyCode.LeftArrow))
        {
            moveRight = false;
            value = -1;
            if (isBig)
            {
                bigAnimator.SetBool("bigRun", true);
            }
            else
            {
                animator.SetBool("Run", true);
            }
        }
        else if (moveRight || Input.GetKey(KeyCode.RightArrow))
        {
            moveLeft = false;
            value = 1;
            if (isBig)
            {
                bigAnimator.SetBool("bigRun", true);
            }
            else
            {
                animator.SetBool("Run", true);
            }
        }
        else
        {
            value = 0;
            if (isBig)
            {
                bigAnimator.SetBool("bigRun", false);
            }
            else
            {
                animator.SetBool("Run", false);
            }
            moveLeft = false;
            moveRight = false;
        }
    }
    public void MovePlayer()
    {
        rb.velocity = new Vector2(value * moveSpeed, rb.velocity.y);

        if (value > 0 && faceRight == false)
        {
            FlipFace();
        }
        else if (value < 0 && faceRight == true)
        {
            FlipFace();
        }
    }



    public void JumpHolder()
    {

        if (OnGround() )
        {
            isJumping = true;
            Vector2 direction = new Vector2(0, jumpForce);
            rb.AddForce(direction, ForceMode2D.Impulse);
        }
        

    }
    public void FlipFace()
    {
        faceRight = !faceRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
    }


    public bool OnGround()
    {
        float heigh = 0.5f;
        RaycastHit2D rayHit1 = Physics2D.Raycast(boxCollider.bounds.center + new Vector3(valueChange, 0, 0), Vector2.down, boxCollider.bounds.extents.y + heigh, layerMask);
        RaycastHit2D rayHit2 = Physics2D.Raycast(boxCollider.bounds.center + new Vector3(-valueChange, 0, 0), Vector2.down, boxCollider.bounds.extents.y + heigh, layerMask);

        //RaycastHit2D rayHit2 = Physics2D.Raycast(boxCollider.bounds.center + new Vector3(0,0,0.5f), Vector2.down, boxCollider.bounds.extents.y + heigh, layerMask);
        //Debug.DrawRay(boxCollider.bounds.center + new Vector3(valueChange, 0, 0), Vector2.down * 10, Color.red);
        //Debug.DrawRay(boxCollider.bounds.center, Vector2.up * 10, Color.red);

        if (rayHit1.collider != null || rayHit2.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Bounce()
    {
        rb.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (GameManager.instance.amountApple > 0)
            {
                SpawnBullet();
                GameManager.instance.amountApple--;
                GameManager.instance.UpdateCountApple();
            }

            if (GameManager.instance.amountApple <= 0)
            {
                if (GameManager.instance.coins >= 20)
                {
                    SpawnBullet();
                    GameManager.instance.coins -= 20;
                    GameManager.instance.UpdateTextCoin();
                    GameManager.instance.UpdateCountApple();
                }
            }
        }
    }

    public void AttackMobie()
    {

        if (GameManager.instance.amountApple > 0)
        {
            SpawnBullet();
            GameManager.instance.amountApple--;
            GameManager.instance.UpdateCountApple();
        }

        if (GameManager.instance.amountApple <= 0)
        {
            if (GameManager.instance.coins >= 20)
            {
                SpawnBullet();
                GameManager.instance.coins -= 20;
                GameManager.instance.UpdateTextCoin();
                GameManager.instance.UpdateCountApple();
            }
        }
    }
    public void SpawnBullet()
    {
        if (faceRight)
        {
            GameObject bullet = ObjectPooling.instance.GetPollingObject();
            if(bullet != null)
            {
                bullet.transform.position = transform.position + new Vector3(1.2f, 0, 0);
                bullet.SetActive(true);

            }
            //Instantiate(bulletPrefebs, transform.position + new Vector3(1.2f, 0, 0), Quaternion.identity);
        }
        else
        {
            GameObject bullet = ObjectPooling.instance.GetPollingObject();
            if (bullet != null)
            {
                bullet.transform.position = transform.position + new Vector3(-1.2f, 0, 0);
                bullet.SetActive(true);

            }
            //Instantiate(bulletPrefebs, transform.position + new Vector3(-1.2f, 0, 0), Quaternion.identity);
        }
    }
    public void SpawnBulletFlip()
    {
    }

    public void DistanceLimit()
    {
        if (transform.position.x < -15)
        {
            transform.position = new Vector3(-15, transform.position.y, transform.position.z);
        }
        if (transform.position.x > distanceX)
        {
            transform.position = new Vector3(distanceX, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Box"))
        {
            isJumping2 = false;
        }

        if (collision.gameObject.CompareTag("MileStone"))
        {
            GameManager.instance.score += 50;
            GameManager.instance.UpdateTextScore();
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            collision.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("LoadLevel", 1);
        }
        if (collision.gameObject.CompareTag("TransLate"))
        {
            TransLateCamera.gameObject.SetActive(true);
        }
        if(collision.gameObject.CompareTag("DeadFall"))
        {
            GameManager.instance.live = 0;
        }
/*        if(collision.gameObject.CompareTag("Enemy"))
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
        }*/

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TransLate"))
        {
            TransLateCamera.gameObject.SetActive(false);
        }
    }

    public void JumpMobie()
    {
        rb.AddForce(Vector2.up * 25,ForceMode2D.Impulse);
    }
    public void LoadLevel()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(y + 1);
        GameManager.instance.level++;
    }

    List<GameObject> boxes = new List<GameObject>();
    GameObject a;
    GameObject b;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Box" && collision.contacts[0].normal.y < 0) {
            //rb.AddForce(Vector2.down * 1000, ForceMode2D.Impulse);
            isJumping2 = false;
        }
        if (collision.gameObject.tag == "Platform") {
            //rb.AddForce(Vector2.down * 1000, ForceMode2D.Impulse);
            transform.SetParent(collision.gameObject.transform);
            moveSpeed = 15;
        }

       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            transform.SetParent(null);
            moveSpeed = 10;
        }
    }
}


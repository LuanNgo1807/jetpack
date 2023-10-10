using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System;

[Serializable]
public class DemoJumping : MonoBehaviour
{

    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;
    public float valueChange;
    public LayerMask layerMask;

    public float jumpForce;
    public float jumpTime;
    public float jumpCounter;
    public bool isJumping;

    [Space(50)]
    [Header("Speed of move")]
    [Range(0f, 20f)]


    public float moveSpeed;
    [Tooltip("Test hover")]
    public string s;
    private void Awake()
    {
    }

    private void Start()
    {
        jumpCounter = jumpTime;

    }
    private void Update()
    {
        Jump();
        Move();

        if(Input.GetKeyDown(KeyCode.S))
        {
            Save();
            Debug.Log("Saved");
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }
    public void Save()
    {

    }

    public void Load()
    {

    }



    public void Move()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        if (OnGround() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpCounter = jumpTime;

        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
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
}



using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class touchJumping : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject playerGameobject;
    public Player player;
    public bool holder;

    private void Start()
    {
        playerGameobject = GameObject.Find("Player");
        player = playerGameobject.GetComponent<Player>();
        player.jumpCounter2 = player.jumpTime2;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        holder = true;

        if (player.OnGround())
        {
            playerGameobject.GetComponent<Rigidbody2D>().velocity = Vector2.up * player.jumpForce2;
            player.isJumping2 = true;
            player.jumpCounter2 = player.jumpTime2;
        }
    }


    private void Update()
    {
        if (holder && player.isJumping2)
        {
            if (player.jumpCounter2 > 0)
            {
                playerGameobject.GetComponent<Rigidbody2D>().velocity = Vector2.up * player.jumpForce2;
                player.jumpCounter2 -= Time.deltaTime;
                player.isJumping2 = true;
            }
            else
            {
                player.isJumping2 = false;
            }

        }

        if (holder)
        {
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        holder = false;
        player.isJumping2 = false;
    }











}



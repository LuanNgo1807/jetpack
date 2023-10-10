using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchMobieJump : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public Player player;
    public float count = 0;
    public bool startCount = false;
    public AudioSource audioSource;
    public void OnPointerDown(PointerEventData eventData)
    {
        startCount = true;
        if (player.underWater == false && player.OnGround())
        {
            player.JumpHolder();
            audioSource.volume = 0.3f;
            audioSource.Play();
        }

    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        
    }

    private void Update()
    {
        if (startCount && player.underWater == true)
        {
            Vector2 direction = new Vector2(0, player.jumpForce);
            player.rb.AddForce(direction, ForceMode2D.Force);
            
        }

        if (player.underWater == false)
        {
            if (player.isJumping)
            {
                if (player.rb.velocity.y < 0)
                {
                    player.rb.gravityScale = player.defaultGravity;
                    if (player.OnGround())
                    {
                        player.isJumping = false;
                        player.jumpingTimer = 0;
                    }
                }
                else if (player.rb.velocity.y > 0)
                {
                    if (startCount)
                    {
                        player.jumpingTimer += Time.deltaTime;
                    }
                }
            }
        }

        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (player.underWater == false)
        {
            if (player.jumpingTimer < player.maxJumpingTime)
            {
                player.rb.gravityScale = player.defaultGravity * 2f;
            }
            startCount = false;
            player.jumpingTimer = 0;
        }
        if (player.underWater)
        {
            startCount = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Transform pLatform;
    public Transform start;
    public Transform end;
    public float speed = 1.5f;

    int direction = 1;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = currentMovementTarget();
        pLatform.position = Vector2.Lerp(pLatform.position, target, speed * Time.deltaTime);
        float distance = (target - (Vector2)pLatform.position).magnitude;
        if (distance <= 0.4f)
        {
            direction *= -1;
        }
    }

    Vector2 currentMovementTarget()
    {
        if(direction == 1)
        {
            return start.position;  
        }
        else
        {
            return end.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && gameObject.tag == "Trap")
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
        }
    }
}

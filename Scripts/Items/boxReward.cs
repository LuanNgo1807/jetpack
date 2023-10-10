using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxReward : MonoBehaviour
{
    public Animator animator;
    public List<GameObject> items;
    public bool isPowerUp = false;
    public bool amount;
    public int index;
    public Sprite spriteEmpty;
    public Player player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.contacts[0].normal.y > 0)
        {
            player.CheckBoxcast();
            
            animator.SetTrigger("boxBounceUP");
            if (!isPowerUp && amount == false)
            {
                items[index].SetActive(true);
                isPowerUp = true;
                GetComponent<SpriteRenderer>().sprite = spriteEmpty; 
            }
            if(amount == true)
            {
                items[index].SetActive(true);
            }
        }
    }

}

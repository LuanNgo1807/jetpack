using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxRewardSpawn : MonoBehaviour
{
    public Animator animator;
    public List<GameObject> items;
    public bool isPowerUp = false;
    public int amount;
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
            //player.CheckBoxcast();
            //DestroyBoxReward();
        }
    }

    public void DestroyBoxReward()
    {
        if (amount < items.Count)
        {
            player.CheckBoxcast();
            animator.SetTrigger("boxBounceUP");
            
            items[amount].SetActive(true);
            amount++;
            if (amount == items.Count)
            {
                GetComponent<SpriteRenderer>().sprite = spriteEmpty;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = spriteEmpty;
        }
    }
}

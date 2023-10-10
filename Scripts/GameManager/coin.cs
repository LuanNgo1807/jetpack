using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.coins++;
            GameManager.instance.UpdateTextCoin();
            Debug.Log(GameManager.instance.coins);
            Destroy(gameObject);
        }
    }

    public void PushCoin()
    {

    }
}

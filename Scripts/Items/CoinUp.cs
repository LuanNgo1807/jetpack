using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUp : MonoBehaviour
{
    public Rigidbody2D rb;
    public float force;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        GameManager.instance.coins++;
        GameManager.instance.UpdateTextCoin();
        Invoke("OnDestroyCoinUp",0.5f);
    }

    public void OnDestroyCoinUp()
    {
        Destroy(gameObject);
    }

}

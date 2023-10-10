using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakBox : MonoBehaviour
{
    public Rigidbody2D rb;
    public float x;
    public float y;
    public float force;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(x,y) * force, ForceMode2D.Impulse);
        Invoke("DestroyBreakBox",1);
    }

    public void DestroyBreakBox()
    {
        Destroy(gameObject);
    }
}

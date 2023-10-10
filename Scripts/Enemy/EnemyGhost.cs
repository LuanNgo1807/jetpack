using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private Player player;
    public float distance;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        InvokeRepeating("Shoot", 1, 2.5f);
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

    }

    public void Shoot()
    {
        if(distance < 15)
        {
            Instantiate(bullet,transform.position, Quaternion.identity);
        }
    }
}

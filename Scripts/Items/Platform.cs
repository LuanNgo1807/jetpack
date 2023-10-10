using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    public float speed;
    public bool upDown;

    void Start()
    {
        currentPoint = pointA.transform;
    }

    void Update()
    {
        if(upDown)
        {
            if (currentPoint == pointB.transform)
            {
                transform.Translate(Vector2.up * speed);
            }
            else
            {
                transform.Translate(Vector2.up * -speed);
            }

            if (currentPoint == pointB.transform && Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {

                currentPoint = pointA.transform;
            }

            if (currentPoint == pointA.transform && Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                currentPoint = pointB.transform;
            }
        }
        else
        {
            if (currentPoint == pointB.transform)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.right * -speed * Time.deltaTime);
            }

            if (currentPoint == pointB.transform && Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {

                currentPoint = pointA.transform;
            }

            if (currentPoint == pointA.transform && Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                currentPoint = pointB.transform;
            }
        }
    }


}

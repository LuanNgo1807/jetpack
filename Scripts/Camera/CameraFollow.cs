using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {

        float x = Mathf.Clamp(target.position.x, xMin, xMax);
        float y = Mathf.Clamp(target.position.y, yMin, yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }
    public void TranslateCam()
    {

        yMin = -36;
        yMax = -36;
    }
    
}

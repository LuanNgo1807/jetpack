using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;
    private List<GameObject> poolingObjects = new List<GameObject>();
    public int amount = 10;
    public GameObject bulletPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            poolingObjects.Add(obj);
        }

    }

    public GameObject GetPollingObject()
    {
        for (int i = 0; i < amount; i++)
        {
            if (!poolingObjects[i].activeInHierarchy)
            {
                return poolingObjects[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

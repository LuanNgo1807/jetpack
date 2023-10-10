using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GetBoxPos : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    //public float distance;
    public Player player;
    //public GameObject c;
    private GameObject firstCollidedObject;
    private GameObject secondCollidedObject;
    private bool hasCollidedWithTwoObjects = false;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        //distance = 10;
    }

    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {

            GameObject otherObject = collision.gameObject;

            if (!hasCollidedWithTwoObjects)
            {
                int counter = 0;
                if (firstCollidedObject == null)
                {
                    counter++;
                    firstCollidedObject = otherObject;
                    //Debug.Log("collision one");
                    //Destroy(firstCollidedObject);
                }
                else if (secondCollidedObject == null && otherObject != firstCollidedObject)
                {
                    secondCollidedObject = otherObject;
                    hasCollidedWithTwoObjects = true;
                    counter++;
                    //Debug.Log("collision two");
                    //Destroy(secondCollidedObject);
                }

                if (hasCollidedWithTwoObjects && (Vector3.Distance(firstCollidedObject.transform.position, player.transform.position) < Vector3.Distance(secondCollidedObject.transform.position, player.transform.position)))
                {
                    GameObject tg = firstCollidedObject;
                    firstCollidedObject = secondCollidedObject;
                    secondCollidedObject = tg;
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!hasCollidedWithTwoObjects)
        {
            if (firstCollidedObject != null)
            {
                if (firstCollidedObject.GetComponent<boxRaw>() != null)
                {
                    firstCollidedObject.GetComponent<boxRaw>().DestroyBox();
                }
                if (firstCollidedObject.GetComponent<boxRewardSpawn>() != null)
                {
                    firstCollidedObject.GetComponent<boxRewardSpawn>().DestroyBoxReward();
                }
            }
        }
        else
        {
            if (secondCollidedObject.GetComponent<boxRaw>() != null)
            {
                secondCollidedObject.GetComponent<boxRaw>().DestroyBox();
            }
            else if (secondCollidedObject.GetComponent<boxRewardSpawn>() != null)
            {
                secondCollidedObject.GetComponent<boxRewardSpawn>().DestroyBoxReward();
            }
            else if (firstCollidedObject.GetComponent<boxRaw>() != null)
            {
                firstCollidedObject.GetComponent<boxRaw>().DestroyBox();
            }
            else if (firstCollidedObject.GetComponent<boxRewardSpawn>() != null)
            {
                firstCollidedObject.GetComponent<boxRewardSpawn>().DestroyBoxReward();
            }

        }
        hasCollidedWithTwoObjects = false;
        firstCollidedObject = null;
        secondCollidedObject = null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public GameObject[] availableRooms;
    public List<GameObject> currentRooms;
    private float screenWidthPoints;

    public GameObject[] availableObjects;
    public List<GameObject> objects;

    public float objectsMinDistance = 5.0f;
    public float objectsMaxDistance = 10.0f;

    public float objectsMinY = -1.4f;
    public float objectsMaxY = 1.4f;

    public float objectsMinRotation = -45.0f;
    public float objectsMaxRotation = 45.0f;

    // Start is called before the first frame update
    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthPoints = height * Camera.main.aspect;
        StartCoroutine(GeneratorCheck());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AddRoom(float farthestRoomEndX)
    {
        int randomRoomIndex = Random.Range(0, availableRooms.Length);
        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);
        float roomWidth = room.transform.Find("floor").localScale.x;
        float roomCenter = farthestRoomEndX + roomWidth * 0.5f;
        room.transform.position = new Vector3(roomCenter, 0, 0);
        currentRooms.Add(room);
    }
    private void GenerateRoomIfRequired()
    {
        List<GameObject> roomsToRemove = new List<GameObject>();
        bool addRooms = true;
        float playerX = transform.position.x;
        float removeRoomX = playerX - screenWidthPoints;
        float addRoomX = playerX + screenWidthPoints;
        float farthestRoomEndX = 0;
        foreach(var room in currentRooms)
        {
            float roomWidth = room.transform.Find("floor").localScale.x;
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            float roomEndX = roomStartX + roomWidth;
            if(roomStartX > addRoomX)
            {
                addRooms = false;
            }
            if(roomEndX < removeRoomX)
            {
                roomsToRemove.Add(room);
            }
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }
        foreach(var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }
        if (addRooms)
        {
            AddRoom(farthestRoomEndX);
        }
    }
    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateRoomIfRequired();
            GenerateObjectsIfRequired();

            yield return new WaitForSeconds(0.25f);
        }
    }
    void AddObject(float lastObjectX)
    {
        //1
        int randomIndex = Random.Range(0, availableObjects.Length);
        //2
        GameObject obj = (GameObject)Instantiate(availableObjects[randomIndex]);
        //3
        float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY = Random.Range(objectsMinY, objectsMaxY);
        obj.transform.position = new Vector3(objectPositionX, randomY, 0);
        //4
        float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
        obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
        //5
        objects.Add(obj);
    }
    void GenerateObjectsIfRequired()
    {
        //1
        float playerX = transform.position.x;
        float removeObjectsX = playerX - screenWidthPoints;
        float addObjectX = playerX + screenWidthPoints;
        float farthestObjectX = 0;
        //2
        List<GameObject> objectsToRemove = new List<GameObject>();
        foreach (var obj in objects)
        {
            //3
            float objX = obj.transform.position.x;
            //4
            farthestObjectX = Mathf.Max(farthestObjectX, objX);
            //5
            if (objX < removeObjectsX)
            {
                objectsToRemove.Add(obj);
            }
        }
        //6
        foreach (var obj in objectsToRemove)
        {
            objects.Remove(obj);
            Destroy(obj);
        }
        //7
        if (farthestObjectX < addObjectX)
        {
            AddObject(farthestObjectX);
        }
    }

}

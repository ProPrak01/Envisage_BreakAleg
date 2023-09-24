using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class dooropen : NetworkBehaviour
{
    
    public List<bool> checkall = new List<bool>();
   // public bool[] checkall = new bool[20];
    public bool i ;
    bool isMoving = false;
    Vector3 initialPosition;
    Vector3 targetPosition;

    public float moveSpeed = 1.0f; // Adjust the speed as needed

    void Start()
    {
        i = false;
        
        for (int j = 0; j < 11; j++)
        {
            checkall.Insert(j,false);
        }
        
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * 4.0f; // Move 2 units up
    }

    void Update()
    {
        
        //i = FindObjectOfType<dooropen>().i;
        for(int j = 0; j <= 10; j++)
        {
            if (checkall[j] == false)
            {
                i = false;
                break;
            }
            else
            {
                i = true;
            }
        }



        if (i && !isMoving)
        {
            StartCoroutine(MoveDoorSmoothly());
            Camera.main.GetComponent<shakycamera>().StartShake();
        }
    }

    IEnumerator MoveDoorSmoothly()
    {
        isMoving = true;
        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        while (Time.time - startTime < 3.0f / moveSpeed)
        {
            float journeyLength = Vector3.Distance(startPosition, targetPosition);
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

}

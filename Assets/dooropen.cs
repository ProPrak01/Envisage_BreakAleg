using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooropen : MonoBehaviour
{
    public bool[] checkall;
    public bool i ;
    bool isMoving = false;
    Vector3 initialPosition;
    Vector3 targetPosition;

    public float moveSpeed = 1.0f; // Adjust the speed as needed

    void Start()
    {
        i = false;
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * 2.0f; // Move 2 units up
    }

    void Update()
    {

        //i = FindObjectOfType<dooropen>().i;
        for(int j = 1; j <= 16; j++)
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
        }
    }

    IEnumerator MoveDoorSmoothly()
    {
        isMoving = true;
        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        while (Time.time - startTime < 1.0f / moveSpeed)
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

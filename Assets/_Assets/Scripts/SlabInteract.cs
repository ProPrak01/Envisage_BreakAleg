using UnityEngine;
using System.Collections;
public class SlabInteract : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject.
    public string player2Tag = "Player2"; // Tag of the player GameObject.

    public float interactionDistance = 2.0f; // Distance to trigger interaction with the block.
    public Material glowMaterial; // Material to apply when the player is near the block.
   // public Material glowMaterial3; // Material to apply when the player is near the block.

    public float rotationSpeed = 45.0f; // Rotation speed in degrees per second.
    public string animationParameter = "IsActivated"; // Name of the animation parameter.
    public int card_id;

    public int value_id;
    private bool isPlayerNear = false; // Flag to track player proximity.
    private Renderer slabRenderer; // Reference to the slab's renderer.
    private Material originalMaterial; // Store the original material.
    public bool isRotating = false; // Flag to track if the block is currently rotating.
    private Quaternion startRotation; // Starting rotation of the block.
    private Quaternion targetRotation; // Target rotation (180 degrees).
    private Quaternion originalRotation; // Original rotation of the block.
    public GameObject mastermindlvl2;
    public int i, j;
    public float p1p2 = 0;
    public KeyCode attachKey; // The key to press to attach/detach the object.


    void Start()
    {
        i = card_id / 10;
        j = card_id % 10;
        slabRenderer = GetComponent<Renderer>();
        originalMaterial = slabRenderer.material;
        originalRotation = transform.rotation; // Store the original rotation.
        startRotation = originalRotation;
        targetRotation = Quaternion.Euler(0, 0, 180) * startRotation; // Rotate 180 degrees around the Y-axis.
    }

    void Update()
    {
        GameObject player = null;
        if (p1p2 == 1)
        {

            player = GameObject.FindGameObjectWithTag(playerTag);
            attachKey = KeyCode.E;
        }
        else if (p1p2 == 2)
        {

            player = GameObject.FindGameObjectWithTag(player2Tag);
            attachKey = KeyCode.O;
        }
        if (isPlayerNear)
        {
            slabRenderer.material = glowMaterial;

            if (Input.GetKeyDown(attachKey) && !isRotating)
            {
                Debug.Log("Hello world");
             //   slabRenderer.material = glowMaterial3;

                // Start the rotation when the player presses "E".
                StartCoroutine(RotateBlock(targetRotation));
                mastermindlvl2.GetComponent<mastermindlvl2>().DoorOpenArray[i,j] = true;

            }
        }
        else
        {
            slabRenderer.material = originalMaterial;
            /**
            // Reverse the rotation when the player gets off the block.
            if (isRotating)
            {
                StopCoroutine("RotateBlock"); // Stop the ongoing rotation.
                StartCoroutine(RotateBlock(originalRotation));
            }
            **/
            mastermindlvl2.GetComponent<mastermindlvl2>().DoorOpenArray[i,j] = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag(playerTag))
        {
            isPlayerNear = true;
            p1p2 = 1;
        }
        else if (other.CompareTag(player2Tag))
        {
            isPlayerNear = true;
            p1p2 = 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
      
        // Check if the colliding object has the "Player" tag.
        if (other.CompareTag(playerTag))
        {

            isPlayerNear = false;
            p1p2 = 0;
            StartCoroutine(RotateBlock(originalRotation));

        }
        else if (other.CompareTag(player2Tag))
        {
            isPlayerNear = false;
            p1p2 = 0;
            StartCoroutine(RotateBlock(originalRotation));

        }
    }

    private IEnumerator RotateBlock(Quaternion target)
    {
        isRotating = true;

        float journeyLength = Quaternion.Angle(transform.rotation, target);
        float journeyTime = journeyLength / rotationSpeed;

        float startTime = Time.time;
        float endTime = startTime + journeyTime;

        while (Time.time < endTime)
        {
            float fractionOfJourney = (Time.time - startTime) / journeyTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, target, fractionOfJourney);
            yield return null;
        }

        // Ensure the block reaches the target rotation exactly.
        transform.rotation = target;

        isRotating = false;
    }
}

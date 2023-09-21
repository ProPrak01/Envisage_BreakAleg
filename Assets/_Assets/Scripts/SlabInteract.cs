using UnityEngine;
using System.Collections;
public class SlabInteract : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject.
    public float interactionDistance = 2.0f; // Distance to trigger interaction with the block.
    public Material glowMaterial; // Material to apply when the player is near the block.
    public float rotationSpeed = 45.0f; // Rotation speed in degrees per second.
    public string animationParameter = "IsActivated"; // Name of the animation parameter.

    private bool isPlayerNear = false; // Flag to track player proximity.
    private Renderer slabRenderer; // Reference to the slab's renderer.
    private Material originalMaterial; // Store the original material.
    private bool isRotating = false; // Flag to track if the block is currently rotating.
    private Quaternion startRotation; // Starting rotation of the block.
    private Quaternion targetRotation; // Target rotation (180 degrees).
    private Quaternion originalRotation; // Original rotation of the block.

    void Start()
    {
        slabRenderer = GetComponent<Renderer>();
        originalMaterial = slabRenderer.material;
        originalRotation = transform.rotation; // Store the original rotation.
        startRotation = originalRotation;
        targetRotation = Quaternion.Euler(0, 0, 180) * startRotation; // Rotate 180 degrees around the Y-axis.
    }

    void Update()
    {
        if (isPlayerNear)
        {
            slabRenderer.material = glowMaterial;

            if (Input.GetKeyDown(KeyCode.E) && !isRotating)
            {
                // Start the rotation when the player presses "E".
                StartCoroutine(RotateBlock(targetRotation));
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNear = false;
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

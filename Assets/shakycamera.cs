using UnityEngine;

public class shakycamera : MonoBehaviour
{
    public float shakeDuration = 0.5f;     // The duration of the camera shake.
    public float shakeMagnitude = 0.1f;    // The magnitude of the shake.

    private Vector3 originalPosition;       // The original position of the camera.
    private float shakeTimer = 0f;         // Timer to control the duration of the shake.

    void Start()
    {
        originalPosition = transform.localPosition; // Store the original position of the camera.

    }

    void Update()
    {
        // Check if the shake timer is active.
        if (shakeTimer > 0)
        {
            // Move the camera's position randomly within a small range.
            transform.localPosition = originalPosition + UnityEngine.Random.insideUnitSphere * shakeMagnitude;

            // Decrease the timer.
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reset the camera's position to its original position.
            shakeTimer = 0f;
            transform.localPosition = originalPosition;
        }
    }

    // Call this method to start the camera shake.
    public void StartShake()
    {
        shakeTimer = shakeDuration;
    }
}

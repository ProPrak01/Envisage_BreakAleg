using UnityEngine;

public class handheldcamera : MonoBehaviour
{
    public float maxShakeIntensity = 0.5f; // Maximum intensity of the camera shake.
    public float shakeSpeed = 1.0f; // Speed at which the camera shake decreases.

    private Vector3 originalPosition; // The original position of the camera.

    public float currentShakeIntensity = 0f; // Current intensity of the camera shake.

    void Start()
    {
        // Store the original position of the camera.
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Update the current shake intensity over time.
        currentShakeIntensity = Mathf.Lerp(currentShakeIntensity, 0f, shakeSpeed * Time.deltaTime);

        // Apply a random offset based on the current shake intensity.
        Vector3 randomOffset = Random.insideUnitSphere * currentShakeIntensity;

        // Apply the offset to the camera's position.
        transform.localPosition = originalPosition + randomOffset;
    }

    // Call this method to start the camera shake.
    public void StartShake(float intensity)
    {
        // Set the current shake intensity to the specified value.
        currentShakeIntensity = intensity;
    }
}

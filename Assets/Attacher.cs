using UnityEngine;

public class Attacher : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject.
    public GameObject objectPrefab; // The prefab to instantiate and make a child of the player.
    public KeyCode attachKey = KeyCode.E; // The key to press to attach the object.

    private GameObject instantiatedObject; // Reference to the instantiated object.

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "playerTag".
        if (other.CompareTag(playerTag))
        {
            // Store a reference to the instantiated object.
            instantiatedObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);

            // Make the instantiatedObject a child of the player's transform.
            instantiatedObject.transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the colliding object has the "playerTag".
        if (other.CompareTag(playerTag))
        {
            // Detach the object if it was attached.
            DetachObject();
        }
    }

    private void Update()
    {
        // Check if the attachKey is pressed and the instantiatedObject is not null.
        if (Input.GetKeyDown(attachKey) && instantiatedObject != null)
        {
            // Detach the object when the attach key is pressed.
            DetachObject();
        }
    }

    private void DetachObject()
    {
        // Remove the instantiatedObject from the player's children
    }
}
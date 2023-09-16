using UnityEngine;

public class Attacher : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject.
    public GameObject objectPrefab; // The prefab to instantiate and make a child of the player.
    public float interactionDistance = 2.0f; // Distance to trigger interaction with the block.
    public KeyCode attachKey = KeyCode.E; // The key to press to attach/detach the object.
    public Material glowMaterial; // Material to apply when the player is near the block.
    public Material originalMaterial;
    private GameObject instantiatedObject; // Reference to the instantiated object.
    private bool isPlayerNear = false; // Flag to track player proximity.
    private Renderer blockRenderer; // Reference to the block's renderer.

    private void Start()
    {
        blockRenderer = GetComponentInChildren<Renderer>();
        originalMaterial = blockRenderer.material;
    }

    private void Update()
    {
        // Check if the player is near the block.
        if (isPlayerNear)
        {
            // Apply the glow material when the player is near.
            blockRenderer.material = glowMaterial;

            // Check if the player presses the attach key and no object is attached to the player.
            if (Input.GetKeyDown(attachKey) /**&& instantiatedObject == null**/)
            {
                // Find the player with the "Player" tag.
                GameObject player = GameObject.FindGameObjectWithTag(playerTag);

                // Check if a player object is found.
                if (player != null)
                {
                    // Calculate the distance between the player and the block.
                    float distance = Vector3.Distance(player.transform.position, transform.position);

                    // Check if the player is within the interaction distance.
                    if (distance <= interactionDistance)
                    {
                        // Instantiate the objectPrefab and make it a child of the player.
                        instantiatedObject = Instantiate(objectPrefab, player.transform);

                        instantiatedObject.transform.localPosition = new Vector3(0f, 3.2f, 0f); // Example position.
                        instantiatedObject.transform.localScale = new Vector3(1f, 1f, 1f); // Example position.

                    }
                }
            }
        }
        else
        {
            // Reset the material when the player is not near.
            blockRenderer.material = originalMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag.
        if (other.CompareTag(playerTag))
        {
            // Check if the player already has an attached object.
            if (other.transform.childCount == 2)
            {
                isPlayerNear = false; // Prevent attachment when an object is already attached.
            }
            else
            {
                isPlayerNear = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the colliding object has the "Player" tag.
        if (other.CompareTag(playerTag))
        {
            isPlayerNear = false;
        }
    }
    
}

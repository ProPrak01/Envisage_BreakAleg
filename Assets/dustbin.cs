using UnityEngine;

public class dustbin : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject.
    public string player2Tag = "Player2"; // Tag of the player GameObject.

    public float interactionDistance = 2.0f; // Distance to trigger interaction with the block.
    public Material glowMaterial; // Material to apply when the player is near the block.
    public Material originalMaterial;

    private bool isPlayerNear = false; // Flag to track player proximity.
    private Renderer blockRenderer; // Reference to the block's renderer.

    public KeyCode attachKey = KeyCode.E; // The key to press to attach/detach the object.
    public float p1p2 = 0;


    private void Start()
    {
        blockRenderer = GetComponent<Renderer>();
        originalMaterial = blockRenderer.material;

    }

    private void Update()
    {
        // Check if the player is near the block.

        if (isPlayerNear)
        {
            // Apply the glow material when the player is near.
            blockRenderer.material = glowMaterial;
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
            // Check if the player presses the attach key.
            if (Input.GetKeyDown(attachKey)  /**&& Time.time - lastTransferTime >= transferCooldown**/)
            {
                // Find the player with the "Player" tag.

                // Check if a player object is found.
                if (player != null )
                {
                    // Calculate the distance between the player and the block.
                    float distance = Vector3.Distance(player.transform.position, transform.position);

                    // Check if the player is within the interaction distance.
                    if (distance <= interactionDistance)
                    {
                        // Transfer the child object from the player to the block.
                        TransferChildObject(player, transform);
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
        }
        else if (other.CompareTag(player2Tag))
        {
            isPlayerNear = false;
            p1p2 = 0;
        }
    }

    private void TransferChildObject(GameObject fromObject, Transform toObject)
    {
        // Check if the player already has a child object.
        if (fromObject.transform.childCount == 2)
        {
            // Get the child object.
            Transform child = fromObject.transform.GetChild(1);

            // Make it a child of the block.
            child.SetParent(toObject);
            child.gameObject.SetActive(false);

            

        }
    }
}

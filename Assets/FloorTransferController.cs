using UnityEngine;

public class TransferBlockController : MonoBehaviour
{
    public int idofemptyblock;
    public int numberneeded;
    public string playerTag = "Player"; // Tag of the player GameObject.
    public float interactionDistance = 2.0f; // Distance to trigger interaction with the block.
    public Material glowMaterial; // Material to apply when the player is near the block.
    public Material originalMaterial;

    private bool isPlayerNear = false; // Flag to track player proximity.
    private Renderer blockRenderer; // Reference to the block's renderer.
    

    public float transferCooldown = 1.0f; // Cooldown time between transfers.
    private float lastTransferTime = 0f; // Time of the last transfer.

   // public GameObject wantedplate;
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
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);

            // Check if the player presses the attach key.
            if (Input.GetKeyDown(KeyCode.E) && player.transform.childCount == 2 /**&& Time.time - lastTransferTime >= transferCooldown**/)
            {
                // Find the player with the "Player" tag.

                // Check if a player object is found.
                if (player != null && transform.childCount == 0)
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

            else if(Input.GetKeyDown(KeyCode.E) && player.transform.childCount == 1)
            {
                temp();
            }
        }
        else
        {
            // Reset the material when the player is not near.
            blockRenderer.material = originalMaterial;
        }
    }
    void temp()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (transform.childCount > 0 && player.transform.childCount < 2)
        {
            // Detach the child object from the block.
            Transform child = transform.GetChild(0);

            // Make it a child of the player again.
            child.SetParent(player.transform);

            // Reset the position relative to the player.
            child.localPosition = new Vector3(0f, 3.2f, 0f);

            // Scale the child object by a factor of 1 (normal size).
            child.localScale = Vector3.one;

            // Rotate the child object back to its original orientation.
            child.localRotation = Quaternion.identity;
            lastTransferTime = Time.time;
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag.
        if (other.CompareTag(playerTag))
        {
            isPlayerNear = true;
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

    private void TransferChildObject(GameObject fromObject, Transform toObject)
    {
        // Check if the player already has a child object.
        if (fromObject.transform.childCount == 2)
        {
            // Get the child object.
            Transform child = fromObject.transform.GetChild(1);

            // Make it a child of the block.
            child.SetParent(toObject);

            // Reset the position relative to the block.
            child.localPosition = Vector3.zero;
            child.localScale = new Vector3(1f, 1f, 1f);

            // Rotate the child object by 45 degrees on the Y-axis.
            child.localRotation = Quaternion.Euler(0f, 180f, 0f);
            
            if(child.tag == numberneeded.ToString())
            {
                FindObjectOfType<dooropen>().checkall[idofemptyblock]= true;
            }
            else
            {
                FindObjectOfType<dooropen>().checkall[idofemptyblock] = false;

            }

        }
    }
}

using UnityEngine;

public class SlabFlip : MonoBehaviour
{
    public int idofemptyblock;
    // public int numberneeded;
    public string playerTag = "Player"; // Tag of the player GameObject.
    public string player2Tag = "Player2"; // Tag of the player GameObject.
    public GameObject flipObject;
    private GameObject flipObject1;

    public float interactionDistance = 2.0f; // Distance to trigger interaction with the block.
    public Material glowMaterial; // Material to apply when the player is near the block.
    public Material glowMaterial2; // Material to apply when the player is near the block.

    public Material originalMaterial;

    private bool isPlayerNear = false; // Flag to track player proximity.
    private Renderer blockRenderer; // Reference to the block's renderer.

    public KeyCode attachKey = KeyCode.E; // The key to press to attach/detach the object.

    public float p1p2 = 0;
    public float transferCooldown = 1.0f; // Cooldown time between transfers.
    private float lastTransferTime = 0f; // Time of the last transfer.
    private Quaternion targetRotation;
    private Quaternion initialRotation;

    // public GameObject wantedplate;
    private void Start()
    {
        blockRenderer = GetComponent<Renderer>();
        originalMaterial = blockRenderer.material;
        targetRotation = Quaternion.Euler(0f, 0f, 90f);
        initialRotation = Quaternion.Euler(0f, 0f, 0f);

    }

    private void Update()
    {
        // Check if the player is near the block.

        if (isPlayerNear)
        {
            // Apply the glow material when the player is near.
          //  blockRenderer.material = glowMaterial;
            GameObject player = null;
            if (p1p2 == 1)
            {
                blockRenderer.material = glowMaterial;

                player = GameObject.FindGameObjectWithTag(playerTag);
                attachKey = KeyCode.E;
            }
            else if (p1p2 == 2)
            {
                blockRenderer.material = glowMaterial2;

                player = GameObject.FindGameObjectWithTag(player2Tag);
                attachKey = KeyCode.O;
            }
            // Check if the player presses the attach key.
            if (Input.GetKeyDown(attachKey) && player.transform.childCount == 1 /**&& Time.time - lastTransferTime >= transferCooldown**/)
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
                        //TransferChildObject(player, transform);
                        Flip();
                    }
                }

                

            }
            /**
            else if(Input.GetKeyDown(attachKey) && player.transform.childCount == 1)
            {
                //temp();
            }
            **/
        }
        else
        {
            // Reset the material when the player is not near.
            blockRenderer.material = originalMaterial;
        }
    }
    void temp()
    {
        GameObject player= null;
        if (p1p2 == 1)
        {
             player = GameObject.FindGameObjectWithTag(playerTag);

        }
        else if (p1p2 == 2)
        {
            player = GameObject.FindGameObjectWithTag(player2Tag);

        }

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

    /*private void TransferChildObject(GameObject fromObject, Transform toObject)
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
    }*/

    public void Flip()
    {
        flipObject1 = Instantiate(flipObject, transform.position, initialRotation);
        transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, 10*Time.deltaTime);
    }
}
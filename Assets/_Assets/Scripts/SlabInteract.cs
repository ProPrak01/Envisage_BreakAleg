using UnityEngine;

public class SlabInteract : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject.
    public float interactionDistance = 2.0f; // Distance to trigger interaction with the block.
    public Material glowMaterial; // Material to apply when the player is near the block.
    public Animator animator; // Reference to the Animator component.
    public string animationParameter = "IsActivated"; // Name of the animation parameter.

    private bool isPlayerNear = false; // Flag to track player proximity.
    private Renderer slabRenderer; // Reference to the slab's renderer.
    private Material originalMaterial; // Store the original material.

    void Start()
    {
        slabRenderer = GetComponent<Renderer>();
        originalMaterial = slabRenderer.material; 
        animator = GetComponent<Animator>();// Store the original material.
    }

    void Update()
    {
        if (isPlayerNear)
        {
            slabRenderer.material = glowMaterial;

            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetBool(animationParameter, true);
            }

        }
        else
        {
            slabRenderer.material = originalMaterial;
            animator.SetBool(animationParameter, false);

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
        //   animator.SetBool(animationParameter, false);

        }
    }
}

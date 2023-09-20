using UnityEngine;

public class SlabInteraction : MonoBehaviour
{
    public bool interactionCompleted = false;
    public KeyCode interactKey = KeyCode.E;
    public Animator animator; // Reference to the animator component
    public Material glowMaterial; // Material to apply when the player is above the slab
    public Material originalMaterial; // Original material of the slab

    private bool isPlayerAboveSlab = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerAboveSlab = true;
            SetSlabMaterial(glowMaterial); // Apply glow material when player is above the slab
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerAboveSlab = false;
            SetSlabMaterial(originalMaterial); // Reset to original material when player leaves the slab
        }
    }

    private void Update()
    {
        if (isPlayerAboveSlab && Input.GetKeyDown(interactKey))
        {
            interactionCompleted = true;
            animator.SetTrigger("TriggerAnimation"); // Trigger the animation
        }
    }

    private void SetSlabMaterial(Material material)
    {
        Renderer slabRenderer = GetComponent<Renderer>();
        slabRenderer.material = material;
    }
}

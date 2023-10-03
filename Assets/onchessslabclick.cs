using UnityEngine;

public class onchessslabclick : MonoBehaviour
{
    private Vector3 targetPosition = Vector3.zero; // Set an initial value (e.g., Vector3.zero) or the player's starting position
    private Transform playerTransform; // Reference to the player's transform
    private float moveSpeed = 5.0f; // Adjust this speed as needed

    private bool isMoving = false;

    Animator animator;
    private bool isWalking;

    private void Start()
    {
        // Find the player's transform by tag (make sure the player has a unique tag)
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = playerTransform.gameObject.GetComponentInChildren<Animator>();
        isMoving = false; // Ensure that isMoving is initially set to false
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Floor"))
                {
                    targetPosition = hit.point;
                    isMoving = true;
                }
            }
        }

        if (isMoving)
        {
            isWalking = true;

            // Move the player towards the target position using lerp
            playerTransform.position = Vector3.Lerp(playerTransform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Rotate the player to face the target position
            Quaternion lookRotation = Quaternion.LookRotation(targetPosition - playerTransform.position);
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, lookRotation, moveSpeed * Time.deltaTime);

            // Check if the player is close enough to the target to stop moving
            if (Vector3.Distance(playerTransform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
        else if(isMoving == false)
        {
            isWalking = false;
           // animator.SetBool("IsWalking", isWalking);


        }
        animator.SetBool("IsWalking", isWalking);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    Animator animator;
    public InputAction playerControls;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    /*private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }*/
    private bool isWalking;

    private void Update()
    {
        
        Vector2 inputVector = new Vector2(0, 0);

        if(Input.GetKey(KeyCode.UpArrow))
        {
            inputVector.y = +1;
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            inputVector.y = -1;
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            inputVector.x = +1;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            inputVector.x = -1;
        }

        
        inputVector = inputVector.normalized;
        
       Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        
        // Vector3 moveDir = playerControls.ReadValue<Vector3>();

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);

        animator.SetBool("IsWalking1", isWalking);
    }
}

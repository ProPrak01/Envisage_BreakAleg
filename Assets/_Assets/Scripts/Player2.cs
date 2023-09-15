using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private bool isWalking;
    
    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if(Input.GetKey(KeyCode.I))
        {
            inputVector.y = +1;
        }

        if(Input.GetKey(KeyCode.K))
        {
            inputVector.y = -1;
        }

        if(Input.GetKey(KeyCode.L))
        {
            inputVector.x = +1;
        }

        if(Input.GetKey(KeyCode.J))
        {
            inputVector.x = -1;
        }


        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir*moveSpeed*Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed*Time.deltaTime);
    
        animator.SetBool("IsWalking1", isWalking);
    }
}
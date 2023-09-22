using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
//hello
using UnityEngine.InputSystem;
//using Photon.Pun;


public class Player : NetworkBehaviour
{
  //  PhotonView view;
    [SerializeField] private float moveSpeed = 10f;
    Animator animator;
    private NetworkVariable<int> netvariable_temp = new NetworkVariable<int>(2,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    public InputAction playerControls;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
      //  view = GetComponent<PhotonView>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    private bool isWalking;
    
    private void Update()
    {
        if (!IsOwner) return;
            Vector3 moveDir = playerControls.ReadValue<Vector3>();

            transform.position += moveDir * moveSpeed * Time.deltaTime;

            isWalking = moveDir != Vector3.zero;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);

            animator.SetBool("IsWalking", isWalking);
        
        /**
        Vector2 inputVector = new Vector2(0, 0);

        if(Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }

        if(Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }

        if(Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        if(Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }

        
        inputVector = inputVector.normalized;
        **/
        //  Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        
    }
}

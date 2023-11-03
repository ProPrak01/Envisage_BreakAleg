using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;

public class Player : NetworkBehaviour
{
  //  private ParticleSystem trail;
    [SerializeField] private float moveSpeed = 10f;
    Animator animator;
    public PlayerInputAction playerControls;
    private InputAction move;
    public GameObject particle;

    [SerializeField] PlayerVisual playerVisual;


    private void Awake()
    {
        
        playerControls = new PlayerInputAction();

    }
    void Start()
    {
        
      particle = GameObject.FindGameObjectWithTag("particles");

        // trail = GetComponentInChildren<ParticleSystem>();
        //  trail.Stop();
        //  input = new Joy();
       // animator = GetComponentInChildren<Animator>();
        //  view = GetComponent<PhotonView>();


        PlayerData playerData = GameNetworkManager.Instance.GetPlayerDataFromClientId(OwnerClientId);
        playerVisual.SetPlayerColor(GameNetworkManager.Instance.GetPlayerColor(playerData.colorId));
    }
   
    /*private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        //  input.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
       // input.Disable();
    }*/
    private bool isWalking;
    
    private void Update()
    {
        animator = GetComponentInChildren<Animator>();

        //particle.transform.position = transform.position;

        //     if (!IsOwner) return;
        //Vector2 moveDir = playerControls.ReadValue<Vector2>();
        //Vector2 moveDir = move.ReadValue<Vector2>();


        Vector2 inputVector = new Vector2(0, 0);
        /**
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
        **/
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        SendInputToServerRpc(inputVector);
        inputVector = inputVector.normalized;
        
        //Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
      //  Vector3 movedir2 = new Vector3(inputVector.x, 0, inputVector.y);
       // transform.position += movedir2 * moveSpeed * Time.deltaTime;

      //  isWalking = movedir2 != Vector3.zero;

     
       // transform.forward = Vector3.Slerp(transform.forward, movedir2, rotateSpeed * Time.deltaTime);


        animator.SetBool("IsWalking", isWalking);
    }
    [ServerRpc(RequireOwnership = false)]
    private void SendInputToServerRpc(Vector2 input)
    {
        // Apply the input on the server and synchronize it with clients
        MovePlayerClientRpc(input);
    }

    [ClientRpc]
    private void MovePlayerClientRpc(Vector2 input)
    {
        // Exclude the local player (IsOwner) from movement
        if (!IsOwner)
        {
            // Apply the movement on all clients
            Vector3 moveDir = new Vector3(input.x, 0f, input.y);
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
          //  float rotateSpeed = 10f;
            // Optionally, update the player's rotation based on the movement direction
            if (moveDir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveDir);
            }
        }
    }
}

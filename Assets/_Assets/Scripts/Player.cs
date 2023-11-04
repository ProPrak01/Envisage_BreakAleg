using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections.Generic;



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

    private ulong clientId_test;
    private ulong clientId_test1;
    private ulong passClientId_test;

    private void Awake()
    {

        playerControls = new PlayerInputAction();

    }
    void Start()
    {

        particle = GameObject.FindGameObjectWithTag("particles");
        PlayerData playerdata = GameNetworkManager.Instance.GetPlayerDataFromPlayerIndex(0);
        PlayerData playerdata1 = GameNetworkManager.Instance.GetPlayerDataFromPlayerIndex(1);

        clientId_test = playerdata.clientId;
        clientId_test1 = playerdata1.clientId;
        // trail = GetComponentInChildren<ParticleSystem>();
        //  trail.Stop();
        //  input = new Joy();
        // animator = GetComponentInChildren<Animator>();
        //  view = GetComponent<PhotonView>();
        if (OwnerClientId == clientId_test)
        {
            passClientId_test = clientId_test;
        }
        else if (OwnerClientId == clientId_test1)
        {
            passClientId_test = clientId_test1;

        }


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





        if (!IsOwner)
        {
            return;
        }
        animator = GetComponentInChildren<Animator>();

        //particle.transform.position = transform.position;

        //     if (!IsOwner) return;
        //Vector2 moveDir = playerControls.ReadValue<Vector2>();
        //Vector2 moveDir = move.ReadValue<Vector2>();


        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }


        //inputVector.x = Input.GetAxis("Horizontal");
        //inputVector.y = Input.GetAxis("Vertical");
        SendInputToServerRpc(inputVector, passClientId_test);
        inputVector = inputVector.normalized;

        //Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        //  Vector3 movedir2 = new Vector3(inputVector.x, 0, inputVector.y);
        // transform.position += movedir2 * moveSpeed * Time.deltaTime;

        //  isWalking = movedir2 != Vector3.zero;


        // transform.forward = Vector3.Slerp(transform.forward, movedir2, rotateSpeed * Time.deltaTime);


        animator.SetBool("IsWalking", isWalking);
    }
    [ServerRpc(RequireOwnership = false)]
    private void SendInputToServerRpc(Vector2 input, ulong passedId)
    {
        // Apply the input on the server and synchronize it with clients
        MovePlayerClientRpc(input, passedId);

    }
    /**
    Vector3 moveDir = new Vector3(input.x * 1000, 0f, input.y * 1000);
    //  transform.position += moveDir * moveSpeed * Time.deltaTime;
    transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDir, moveSpeed * Time.deltaTime);

    float rotateSpeed = 10f;
    transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    //  float rotateSpeed = 10f;
    // Optionally, update the player's rotation based on the movement direction
    if (moveDir != Vector3.zero)
    {
        transform.rotation = Quaternion.LookRotation(moveDir);
    }
    **/


    [ClientRpc]
    private void MovePlayerClientRpc(Vector2 input, ulong passedId)
    {
        Debug.Log("ownerClientId: " + OwnerClientId + " passedId: " + passedId);
        // Exclude the local player (IsOwner) from movement
        if (OwnerClientId == passedId)
        {

            // Apply the movement on all clients
            Vector3 moveDir = new Vector3(input.x, 0f, input.y);
            //  transform.position += moveDir * moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDir, moveSpeed * Time.deltaTime);

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
//hello
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;
//using Photon.Pun;

public class Player : NetworkBehaviour
{
  //  private ParticleSystem trail;
    //  PhotonView view;
    [SerializeField] private float moveSpeed = 10f;
    Animator animator;
    //private Joy input=null;
 //   private NetworkVariable<int> netvariable_temp = new NetworkVariable<int>(2,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    public PlayerInputAction playerControls;
    private InputAction move;
    public GameObject particle;
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
        animator = GetComponentInChildren<Animator>();
      //  view = GetComponent<PhotonView>();
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
        //particle.transform.position = transform.position;

   //     if (!IsOwner) return;
        //Vector2 moveDir = playerControls.ReadValue<Vector2>();
        //Vector2 moveDir = move.ReadValue<Vector2>();

        
        Vector2 inputVector = new Vector2(0, 0);

        if(Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }

        if(Input.GetKey(KeyCode.S))
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
        
        //Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        Vector3 movedir2 = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += movedir2 * moveSpeed * Time.deltaTime;

        isWalking = movedir2 != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movedir2, rotateSpeed * Time.deltaTime);


        animator.SetBool("IsWalking", isWalking);
    }
}

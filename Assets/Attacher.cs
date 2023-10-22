//using Photon.Pun;
using UnityEngine;
using Unity.Netcode;
//using static UnityEditor;
using UnityEngine.UIElements;

public class Attacher : NetworkBehaviour
{
    //public GameObject playerprefab;
    // public string number;
    //PhotonView view;
    public string playerTag = "Player"; // Tag of the player GameObject.
    public string player2Tag = "Player2"; // Tag of the player GameObject.
    public float p1p2;
    //public GameObject objectPrefab; // The prefab to instantiate and make a child of the player.
    public float interactionDistance = 2.0f; // Distance to trigger interaction with the block.
    public KeyCode attachKey = KeyCode.E; // The key to press to attach/detach the object.
    public Material glowMaterial; // Material to apply when the player is near the block.
    public Material originalMaterial;
    private GameObject instantiatedObject; // Reference to the instantiated object.
    public GameObject objectPrefab; // Reference to the instantiated object.
    public bool buttonpressed = false;
    private bool isPlayerNear = false; // Flag to track player proximity.
    private Renderer blockRenderer; // Reference to the block's renderer.

    private void Start()
    {
        blockRenderer = GetComponentInChildren<Renderer>();
        originalMaterial = blockRenderer.material;
    }

    private void Update()
    {
        // Check if the player is near the block.
        if (isPlayerNear)
        {
            if (!IsOwner) return;
            // Apply the glow material when the player is near.
            blockRenderer.material = glowMaterial;
            GameObject player = null;
            if (p1p2 == 1)
            {
                player = GameObject.FindGameObjectWithTag(playerTag);
                attachKey = KeyCode.E;
                // attachKey = KeyCode.O;    // shuffledcontrolled

            }
            else if (p1p2 == 2)
            {
                player = GameObject.FindGameObjectWithTag(player2Tag);
                attachKey = KeyCode.O;
                // attachKey = KeyCode.E;    // shuffledcontrolled


            }
            // Check if the player presses the attach key and no object is attached to the player.
            if (Input.GetKeyDown(attachKey) || (buttonpressed == true)/**&& instantiatedObject == null**/)
            {

                // Find the player with the "Player" tag.

                // Check if a player object is found.
                if (player != null)
                {
                    // Calculate the distance between the player and the block.
                    float distance = Vector3.Distance(player.transform.position, transform.position);

                    // Check if the player is within the interaction distance.
                    if (distance <= interactionDistance)
                    {   

                        //  PhotonNetwork.Instantiate(playerprefab.name, player.transform.position, Quaternion.identity);


                        // Instantiate the objectPrefab and make it a child of the player.
                        //instantiatedObject = PhotonNetwork.Instantiate(number, player.transform.position, Quaternion.identity);
                        /**  instantiatedObject = Instantiate(objectPrefab, player.transform);
                          instantiatedObject.GetComponent<NetworkObject>().Spawn(true);
                          //  instantiatedObject.transform.parent = player.transform;
                          instantiatedObject.transform.localPosition = new Vector3(0f, 3.2f, 0f); // Example position.
                          instantiatedObject.transform.localScale = new Vector3(1f, 1f, 1f); // Example position.
                        **/
                        InstatiateServerRpc();
                    }
                }
                buttonpressed = false;
            }
            /**
            if (buttonpressed == true)
                if (player != null)
                {
                    // Calculate the distance between the player and the block.
                    float distance = Vector3.Distance(player.transform.position, transform.position);

                    // Check if the player is within the interaction distance.
                    if (distance <= interactionDistance)
                    {
                        //  PhotonNetwork.Instantiate(playerprefab.name, player.transform.position, Quaternion.identity);


                        // Instantiate the objectPrefab and make it a child of the player.
                        //instantiatedObject = PhotonNetwork.Instantiate(number, player.transform.position, Quaternion.identity);
                        /**  instantiatedObject = Instantiate(objectPrefab, player.transform);
                          instantiatedObject.GetComponent<NetworkObject>().Spawn(true);
                          //  instantiatedObject.transform.parent = player.transform;
                          instantiatedObject.transform.localPosition = new Vector3(0f, 3.2f, 0f); // Example position.
                          instantiatedObject.transform.localScale = new Vector3(1f, 1f, 1f); // Example position.
                        
                        InstatiateServerRpc();
                        buttonpressed = false;

                    }
                }**/
        }
        else
        {
            // Reset the material when the player is not near.
            blockRenderer.material = originalMaterial;
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag.
        if (other.CompareTag(playerTag) || other.CompareTag(player2Tag))
        {

            if (other.CompareTag(playerTag))
            {
                p1p2 = 1;
            }
            else if (other.CompareTag(player2Tag))
            {
                p1p2 = 2;
            }
            // Check if the player already has an attached object.
            if (other.transform.childCount == 2)
            {
                isPlayerNear = false;
                p1p2 = 0;
                // Prevent attachment when an object is already attached.
            }
            else
            {
                isPlayerNear = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the colliding object has the "Player" tag.
        if (other.CompareTag(playerTag) || other.CompareTag(player2Tag))
        {
            isPlayerNear = false;
            p1p2 = 0;

        }
    }
    [ServerRpc]
    private void InstatiateServerRpc()
    {
        GameObject player = null;
        if (p1p2 == 1)
        {
            player = GameObject.FindGameObjectWithTag(playerTag);
            if (Input.GetKeyDown(KeyCode.E) || (buttonpressed == true)/**&& instantiatedObject == null**/)
                attachKey = KeyCode.E;
            // attachKey = KeyCode.O;    // shuffledcontrolled

        }
        else if (p1p2 == 2)
        {
            player = GameObject.FindGameObjectWithTag(player2Tag);
            attachKey = KeyCode.O;
            // attachKey = KeyCode.E;    // shuffledcontrolled


        }
        //  PhotonNetwork.Instantiate(playerprefab.name, player.transform.position, Quaternion.identity);


        // Instantiate the objectPrefab and make it a child of the player.
        //instantiatedObject = PhotonNetwork.Instantiate(number, player.transform.position, Quaternion.identity);
        
        instantiatedObject = Instantiate(objectPrefab, player.transform);
        instantiatedObject.GetComponent<NetworkObject>().Spawn(true);
         instantiatedObject.transform.parent = player.transform;
        instantiatedObject.transform.localPosition = new Vector3(0f, 3.2f, 0f); // Example position.
        instantiatedObject.transform.localScale = new Vector3(1f, 1f, 1f); // Example position.
        

    }
    public void onbuttonclickfxn( )
    {
        GameObject player = null;
        player = GameObject.FindGameObjectWithTag("Player");

        /*  if (p1p2 == 1)
          {
              player = GameObject.FindGameObjectWithTag(playerTag);
              attachKey = KeyCode.E;
              // attachKey = KeyCode.O;    // shuffledcontrolled

          }
          else if (p1p2 == 2)
          {
              player = GameObject.FindGameObjectWithTag(player2Tag);
              attachKey = KeyCode.O;
              // attachKey = KeyCode.E;    // shuffledcontrolled


          }*/
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance)
        {
           

            buttonpressed = true;
        }
    }
}
using System;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay.Models;
using UnityEngine;

public class player_to_floor : NetworkBehaviour
{
    private CustomData customData;

    [Serializable]
    public class CustomData
    {
        public int intValue;

    }
    public GameObject attachertoplayerscript;

    public string PlayerTag = "Player";


    public GameObject numberPrefab;
    public GameObject numberPrefab2;
    public GameObject numberPrefab3;
    public GameObject numberPrefab4;

  //  public GameObject choosenPrefab;

    public KeyCode attachKey = KeyCode.E;
    public Material glowM;
    public Material normalM;
    private bool isAttached = false;
    private NetworkObject attachedObject;


    private void Start()
    {
        // Initialize customData in the Start method or wherever appropriate
        customData = new CustomData
        {
            intValue = 0,

        };


    }
    private void Update()
    {
        /**
        if (!IsOwner)
            return;
        **/
        if (Input.GetKeyDown(attachKey) && !isAttached && transform.childCount < 1)
        {
            TryAttach();
            RemoveChildObjectsOnServerRpc(NetworkManager.LocalClientId);
        }

    }

    private void TryAttach()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f); // Adjust the radius as needed
        foreach (Collider collider in colliders)
        {
            Debug.Log("tryattach_floor");

            if (collider.CompareTag(PlayerTag) && (collider.gameObject.transform.childCount > 1) )
            {
                Debug.Log("tryattach_floor_if");

                // normalM = collider.gameObject.GetComponentInChildren<Renderer>().material;
                // collider.gameObject.GetComponentInChildren<Renderer>().material = glowM;

                //  NetworkObject networkObjectToAttach = collider.gameObject.GetComponent<NetworkObject>();
                Transform firstChild = collider.transform.GetChild(1); // Get the first child
                if (firstChild.CompareTag("1"))
                 {
                    Debug.Log("b1");

                    customData.intValue = 1;
                }
                 else if (firstChild.CompareTag("2"))
                 {
                    Debug.Log("b2");

                    customData.intValue = 2;
                }
                 else if (firstChild.CompareTag("3"))
                 {
                    Debug.Log("b3");

                    customData.intValue = 3;
                }
                 else if (firstChild.CompareTag("4"))
                 {
                    Debug.Log("b4");

                    customData.intValue = 4;
                }

                string jsonData = JsonUtility.ToJson(customData);
                SpawnNumberOnServerRpc(jsonData);
                customData.intValue = 0;

                break;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnNumberOnServerRpc(string jsonData)
    {
        Debug.Log("server_running_floor");
        CustomData receivedData = JsonUtility.FromJson<CustomData>(jsonData);


        if (!isAttached)
        {
            Debug.Log("bro");
            Vector3 displacement = new Vector3(0f, 0f, 0f);
            GameObject number = null;
            if (receivedData.intValue == 1)
            {
                number = Instantiate(numberPrefab, transform.position + displacement, Quaternion.identity);

            }
            else if (receivedData.intValue == 2)
            {
                number = Instantiate(numberPrefab2, transform.position + displacement, Quaternion.identity);

            }
            else if (receivedData.intValue == 3)
            {
                number = Instantiate(numberPrefab3, transform.position + displacement, Quaternion.identity);

            }
            else if (receivedData.intValue == 4)
            {
                number = Instantiate(numberPrefab4, transform.position + displacement, Quaternion.identity);

            }
            NetworkObject numberNetworkObject = number.GetComponent<NetworkObject>();

            if (numberNetworkObject != null)
            {

                numberNetworkObject.Spawn();
                AttachToNumber(numberNetworkObject.NetworkObjectId);
                PerformAttachmentClientRpc();
            }
        }
    }

    private void AttachToNumber(ulong networkObjectId)
    {
        NetworkObject numberNetworkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId];

        if (numberNetworkObject != null)
        {
            attachedObject = numberNetworkObject;
            isAttached = true;
            numberNetworkObject.transform.SetParent(transform);
            numberNetworkObject.transform.localScale = new Vector3(1f, 1f, 1f);
            numberNetworkObject.transform.rotation =  Quaternion.Euler(0, 0, 0);
           // attachertoplayerscript.GetComponent<attacher_to_player>().isAttached = false;
        }

    }

    [ClientRpc]
    private void PerformAttachmentClientRpc()
    {
        isAttached = true;
    }

    [ServerRpc(RequireOwnership = false)]
    public void RemoveChildObjectsOnServerRpc(ulong clientOwnerId)
    {
        if (IsServer)
        {
            NetworkObject playerNetworkObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(clientOwnerId);

            if (playerNetworkObject != null)
            {
                Transform playerTransform = playerNetworkObject.transform;

                // Remove all child objects of the player object
                foreach (Transform child in playerTransform)
                {
                    NetworkObject childNetworkObject = child.GetComponent<NetworkObject>();
                    if (childNetworkObject != null)
                    {
                        childNetworkObject.Despawn();
                    }
                }

            }
        }
    }


}

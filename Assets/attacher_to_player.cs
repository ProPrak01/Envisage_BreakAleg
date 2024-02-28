using System;
using Unity.Netcode;
using UnityEngine;

public class attacher_to_player : NetworkBehaviour
{
    private CustomData customData;

    [Serializable]
    public class CustomData
    {
        public int intValue;
        
    }




    public string blockTag1 = "Attacher1";
    public string blockTag2 = "Attacher2";

    public string blockTag3 = "Attacher3";

    public string blockTag4 = "Attacher4";

    public GameObject numberPrefab;
    public GameObject numberPrefab2;
    public GameObject numberPrefab3;
    public GameObject numberPrefab4;
    public bool check_touch = false;
   // public GameObject choosenPrefab;
    public KeyCode attachKey = KeyCode.E;
    public Material glowM;
    public Material normalM;
  //  public bool isAttached = false;
    private NetworkObject attachedObject;

   // private NetworkVariable<int> chooseprefab_no = new NetworkVariable<int>(0);



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


       



        if (!IsOwner)
            return;

        if ((Input.GetKeyDown(attachKey) )&& transform.childCount <2)
        {
            TryAttach();
            check_touch = false;
        }
       
    }

    private void TryAttach()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f); // Adjust the radius as needed
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(blockTag1)|| collider.CompareTag(blockTag2)|| collider.CompareTag(blockTag3)|| collider.CompareTag(blockTag4))
            {
               
                normalM =  collider.gameObject.GetComponentInChildren<Renderer>().material;
              //  collider.gameObject.GetComponentInChildren<Renderer>().material = glowM;

              //  NetworkObject networkObjectToAttach = collider.gameObject.GetComponent<NetworkObject>();
                if (collider.CompareTag("Attacher1"))
                {
                    customData.intValue = 1;
                    Debug.Log("a1");
                }
                else if (collider.CompareTag("Attacher2"))
                {
                    customData.intValue = 2;
                    Debug.Log("a2");

                }
                else if (collider.CompareTag("Attacher3"))
                {
                    customData.intValue = 3;
                    Debug.Log("a3");

                }
                else if (collider.CompareTag("Attacher4"))
                {
                    customData.intValue = 4;
                    Debug.Log("a4");

                }
                //  NetworkObject choosenprefab_net = choosenPrefab.gameObject.GetComponent<NetworkObject>();
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
        //NetworkObject choosenprefab_net_server = NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId];
       // GameObject chosenPrefab = choosenprefab_net_server.gameObject; // You now have a reference to the GameObject
        Debug.Log("rpc running");
        CustomData receivedData = JsonUtility.FromJson<CustomData>(jsonData);



            Debug.Log("bro");
            Vector3 displacement = new Vector3(0f, 3f, 0f);
            GameObject number = null;
             if (receivedData.intValue == 1)
            {
                number = Instantiate(numberPrefab, transform.position + displacement , Quaternion.identity);

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

    private void AttachToNumber(ulong networkObjectId)
    {
        NetworkObject numberNetworkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId];

        if (numberNetworkObject != null)
        {
            attachedObject = numberNetworkObject;
           // isAttached = true;
            numberNetworkObject.transform.SetParent(transform);
            numberNetworkObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    [ClientRpc]
    private void PerformAttachmentClientRpc()
    {
       // isAttached = true;
    }

    
}

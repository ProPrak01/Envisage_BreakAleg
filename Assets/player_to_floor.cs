using Unity.Netcode;
using UnityEngine;

public class player_to_floor : NetworkBehaviour
{

    public string blockTag1 = "Attacher1";
    public string blockTag2 = "Attacher2";

    public string blockTag3 = "Attacher3";

    public string blockTag4 = "Attacher4";

    public GameObject numberPrefab;
    public GameObject numberPrefab2;
    public GameObject numberPrefab3;
    public GameObject numberPrefab4;

    public GameObject choosenPrefab;
    public KeyCode attachKey = KeyCode.E;
    public Material glowM;
    public Material normalM;
    private bool isAttached = false;
    private NetworkObject attachedObject;



    private void Update()
    {
        if (!IsOwner)
            return;

        if (Input.GetKeyDown(attachKey) && !isAttached)
        {
            TryAttach();
        }

    }

    private void TryAttach()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f); // Adjust the radius as needed
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(blockTag1) || collider.CompareTag(blockTag2) || collider.CompareTag(blockTag3) || collider.CompareTag(blockTag4))
            {

                normalM = collider.gameObject.GetComponentInChildren<Renderer>().material;
                collider.gameObject.GetComponentInChildren<Renderer>().material = glowM;

                NetworkObject networkObjectToAttach = collider.gameObject.GetComponent<NetworkObject>();
                if (collider.CompareTag("Attacher1"))
                {
                    choosenPrefab = numberPrefab;
                }
                else if (collider.CompareTag("Attacher2"))
                {
                    choosenPrefab = numberPrefab2;
                }
                else if (collider.CompareTag("Attacher3"))
                {
                    choosenPrefab = numberPrefab3;
                }
                else if (collider.CompareTag("Attacher4"))
                {
                    choosenPrefab = numberPrefab4;
                }
                SpawnNumberOnServerRpc();
                break;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnNumberOnServerRpc()
    {



        if (!isAttached)
        {
            Debug.Log("bro");
            Vector3 displacement = new Vector3(0f, 3f, 0f);
            GameObject number = Instantiate(choosenPrefab, transform.position + displacement, Quaternion.identity);
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
        }
    }

    [ClientRpc]
    private void PerformAttachmentClientRpc()
    {
        isAttached = true;
    }
}

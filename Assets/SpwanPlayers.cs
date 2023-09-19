using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpwanPlayers : MonoBehaviour
{
    public GameObject playerprefab;
    // Start is called before the first frame update
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0,Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerprefab.name,randomPosition,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

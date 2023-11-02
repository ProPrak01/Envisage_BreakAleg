using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class characterSelectReady : NetworkBehaviour
{

    public static characterSelectReady Instance { get; private set; }

    public event EventHandler OnReadyChanged;
    private Dictionary<ulong, bool> playerReadyDic;

    private void Awake()
    {
        Instance = this;
        playerReadyDic = new Dictionary<ulong, bool>();
    }

    public void setPlayerReady()
    {
        SetPlayerReadyServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        SetPlayerReadyClientRpc(serverRpcParams.Receive.SenderClientId);
        playerReadyDic[serverRpcParams.Receive.SenderClientId] = true;
        bool allClientsReady = true;
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!playerReadyDic.ContainsKey(clientId) || !playerReadyDic[clientId])
            {
                // This player is NOT ready
                allClientsReady = false;
                break;
            }
        }
        if (allClientsReady)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("level1", loadSceneMode: LoadSceneMode.Single);
        }
    }
    [ClientRpc]
    private void SetPlayerReadyClientRpc(ulong clientId)
    {
        playerReadyDic[clientId] = true;

        OnReadyChanged?.Invoke(this, EventArgs.Empty);
    }



    public bool IsPlayerReady(ulong clientId)
    {
        // return playerReadyDic[clientId];
        return playerReadyDic.ContainsKey(clientId) && playerReadyDic[clientId];
    }
}

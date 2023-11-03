using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Unity.Services.Lobbies.Models;

using UnityEngine.UI;

public class Character_select : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button readyButton;
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI lobbyCodeText;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            SceneManager.LoadScene(0);

        });
        readyButton.onClick.AddListener(() =>
        {
            characterSelectReady.Instance.setPlayerReady();
        });
    }
    private void Start()
    {
        Lobby lobby = GameLobby.Instance.GetLobby();
        lobbyNameText.text = "Lobby Name: " + lobby.Name;
        lobbyCodeText.text = "Lobby Code: " + lobby.LobbyCode;
    }
}

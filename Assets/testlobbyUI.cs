using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;
public class testlobbyUI : MonoBehaviour
{
    [SerializeField] private Button creategameButton;
    [SerializeField] private Button joingamebutton;

    private void Awake()
    {
        creategameButton.onClick.AddListener(() =>
        {
          // NetworkManager.Singleton.StartHost();
          GameNetworkManager.Instance.StartHost();

            NetworkManager.Singleton.SceneManager.LoadScene("character_select", loadSceneMode: LoadSceneMode.Single);
        });
        joingamebutton.onClick.AddListener(() =>
        {
           // NetworkManager.Singleton.StartClient();
            GameNetworkManager.Instance.StartClient();

        });
    }

}

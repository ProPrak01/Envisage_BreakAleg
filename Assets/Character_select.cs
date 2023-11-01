using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

using UnityEngine.UI;

public class Character_select : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button readyButton;


    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();

        });
    }
}

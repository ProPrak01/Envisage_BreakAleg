using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterColorSelectSingleUI : MonoBehaviour
{
    [SerializeField] private int colorId;
    [SerializeField] private Image image;
    [SerializeField] private GameObject selectedGameObject;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameNetworkManager.Instance.ChangePlayerColor(colorId);
        });
    }

    private void Start()
    {
        GameNetworkManager.Instance.OnPlayerDataNetworkListChanged += GameNetworkManager_OnPlayerDataNetworkListChanged;
        image.color = GameNetworkManager.Instance.GetPlayerColor(colorId);
        UpdateIsSelected();
    }
    private void GameNetworkManager_OnPlayerDataNetworkListChanged(object sender, System.EventArgs e)
    {
        UpdateIsSelected();


    }
    private void UpdateIsSelected()
    {
       if( GameNetworkManager.Instance.GetPlayerData().colorId == colorId)
        {
            selectedGameObject.SetActive(true);

        }
        else
        {
            selectedGameObject.SetActive(false);
        }
    }
}

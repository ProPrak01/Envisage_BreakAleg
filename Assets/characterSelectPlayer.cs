using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class characterSelectPlayer : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject ready;
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private Text playerNameText;
  //  [SerializeField] private Button kickPlayer;

   /** private void Awake()
    {

        kickPlayer.onClick.AddListener(() =>
        {
            PlayerData playerdata = GameNetworkManager.Instance.GetPlayerDataFromPlayerIndex(playerIndex);
            GameLobby.Instance.KickPlayer(playerdata.playerId.ToString()) ;
            GameNetworkManager.Instance.KickPlayer(playerdata.clientId);
        });
    }
   **/
    private void Start()
    {
        GameNetworkManager.Instance.OnPlayerDataNetworkListChanged += GameNetworkMultiplayer_OnPlayerDataNetworkListChanged;
        characterSelectReady.Instance.OnReadyChanged += characterSelectReady_OnReadyChanged;

        UpdatePlayer();
    }

    private void characterSelectReady_OnReadyChanged(object sender, System.EventArgs e)
    {
        UpdatePlayer();
    }


    private void GameNetworkMultiplayer_OnPlayerDataNetworkListChanged(object sender, System.EventArgs e)
    {
        UpdatePlayer();
    }
    private void UpdatePlayer()
    {
        if (GameNetworkManager.Instance.IsPlayerIndexConnected(playerIndex))
        {
            show();



            PlayerData playerData = GameNetworkManager.Instance.GetPlayerDataFromPlayerIndex(playerIndex);
            ready.SetActive(characterSelectReady.Instance.IsPlayerReady(playerData.clientId));


            playerNameText.text = playerData.playerName.ToString();


            playerVisual.SetPlayerColor(GameNetworkManager.Instance.GetPlayerColor(playerData.colorId));
        }
        else
        {
            hide();
        }
    }
    private void show()
    {
        gameObject.SetActive(true);

    }
    private void hide()
    {
        gameObject.SetActive(false);

    }
}

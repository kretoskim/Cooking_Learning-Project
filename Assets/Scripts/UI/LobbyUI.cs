using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private LobbyCreateUI lobbyCreateUI;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button joinWithCodeButton;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private Transform lobbyContainer;
    [SerializeField] private Transform lobbyTemplate;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => {KitchenGameLobby.Instance.LeaveLobby(); Loader.Load(Loader.Scene.MainMenuScene);});
        quickJoinButton.onClick.AddListener(() => {KitchenGameLobby.Instance.QuickJoin();});
        createLobbyButton.onClick.AddListener(() => {lobbyCreateUI.Show();});
        joinWithCodeButton.onClick.AddListener(() => {KitchenGameLobby.Instance.JoinWithCode(joinCodeInputField.text);});

        lobbyTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        playerNameInputField.text = KitchenGameMultiplayer.Instance.GetPlayerName();
        playerNameInputField.onValueChanged.AddListener((string newText) => {KitchenGameMultiplayer.Instance.SetPlayerName(newText);});
        
        KitchenGameLobby.Instance.OnLobbyListChanged += KitchenGameLobby_OnLobbyListChanged;
        UpdateLobbyList(new List<Lobby>());
    }

    private void KitchenGameLobby_OnLobbyListChanged(object sender, KitchenGameLobby.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.lobbyList);
    }
    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        foreach(Transform child in lobbyContainer)
        {
           if(child == lobbyTemplate) continue;
           Destroy(child.gameObject); 
        }
        foreach(Lobby lobby in lobbyList)
        {
            Debug.Log($" → Adding lobby: {lobby.Name} (ID: {lobby.Id})");
            Transform lobbyTransform = Instantiate(lobbyTemplate, lobbyContainer);
            lobbyTemplate.gameObject.SetActive(true);
            lobbyTransform.GetComponent<LobbyListSingleUI>().SetLobby(lobby);
        }
    }
}

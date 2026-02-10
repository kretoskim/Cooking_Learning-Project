using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HostDisconnectUI : MonoBehaviour
{
    [SerializeField] Button playAgainButton;
    private void Awake()
    {
        playAgainButton.onClick.AddListener(() => {Loader.Load(Loader.Scene.MainMenuScene);});
    }
    private void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
        Hide();
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        if(NetworkManager.Singleton.IsClient && clientId == NetworkManager.Singleton.LocalClientId)
        {
            //Server is shutting down
            Show();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnDestroy() 
    {
        NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_OnClientDisconnectCallback;
    }
}

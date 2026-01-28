using System;
using UnityEngine;

public class WaitingForOtherPlayersUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.IsLocalPlayerReadyChanged += GameManager_IsLocalPlayerReadyChanged;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if(GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }
    private void GameManager_IsLocalPlayerReadyChanged(object sender, EventArgs e)
    {
        if(GameManager.Instance.IsLocalPlayerReady())
        {
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
}

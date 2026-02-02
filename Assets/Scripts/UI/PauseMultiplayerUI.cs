using System;
using UnityEngine;

public class PauseMultiplayerUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnMultiplayerGamePause += GameManager_OnMultiplayerGamePause;
        GameManager.Instance.OnMultiplayerGameUnpaused += GameManager_OnMultiplayerGameUnpause;
        
        Hide();
    }

    private void GameManager_OnMultiplayerGamePause(object sender, EventArgs e)
    {
        Show();
    }

    private void GameManager_OnMultiplayerGameUnpause(object sender, EventArgs e)
    {
        Hide();
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

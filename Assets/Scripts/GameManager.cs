using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.Netcode;

public class GameManager : NetworkBehaviour 
{
    public static GameManager Instance { get; private set;}
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public event EventHandler IsLocalPlayerReadyChanged;
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
      private bool isLocalPlayerReady;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer; 
    private float gamePlayingTimerMax = 100f;
    private bool isGamePaused = false;
    private Dictionary<ulong, bool> playerReadyDictionary;
  

    private void Awake()
    {
        Instance = this;

        state = State.WaitingToStart;

        playerReadyDictionary = new Dictionary<ulong, bool>();
    }
    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(state == State.WaitingToStart)
        {
            isLocalPlayerReady = true;

            SetPlayerReadyServerRpc();

            IsLocalPlayerReadyChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        playerReadyDictionary[serverRpcParams.Receive.SenderClientId] = true;

        bool allClientsReady = true;
        foreach(ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if(!playerReadyDictionary.ContainsKey(clientId) || !playerReadyDictionary[clientId])
            {
                //This player is NOT ready
                allClientsReady = false;
                break;
            }
        }

        Debug.Log("allClientsReady:" + allClientsReady);
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if(countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;    
        } 
        //Debug.Log(state);  
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }
    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public bool IsLocalPlayerReady()
    {
        return isLocalPlayerReady;
    }
    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer/gamePlayingTimerMax);
    }
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if(isGamePaused)
        {
            Time.timeScale = 0f; 

            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f; 

            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }     
    }
}

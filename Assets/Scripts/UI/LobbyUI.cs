using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private LobbyCreateUI lobbyCreateUI;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button joinWithCodeButton;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private TMP_InputField playerNameInputField;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => {KitchenGameLobby.Instance.LeaveLobby(); Loader.Load(Loader.Scene.MainMenuScene);});
        quickJoinButton.onClick.AddListener(() => {KitchenGameLobby.Instance.QuickJoin();});
        createLobbyButton.onClick.AddListener(() => {lobbyCreateUI.Show();});
        joinWithCodeButton.onClick.AddListener(() => {KitchenGameLobby.Instance.JoinWithCode(joinCodeInputField.text);});
    }

    private void Start()
    {
        playerNameInputField.text = KitchenGameMultiplayer.Instance.GetPlayerName();
        playerNameInputField.onValueChanged.AddListener((string newText) => {KitchenGameMultiplayer.Instance.SetPlayerName(newText);});
    }
}

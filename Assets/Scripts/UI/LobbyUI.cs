using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button joinWithCodeButton;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private LobbyCreateUI lobbyCreateUI;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => {Loader.Load(Loader.Scene.MainMenuScene);});
        quickJoinButton.onClick.AddListener(() => {KitchenGameLobby.Instance.QuickJoin();});
        createLobbyButton.onClick.AddListener(() => {lobbyCreateUI.Show();});
        joinWithCodeButton.onClick.AddListener(() => {KitchenGameLobby.Instance.JoinWithCode(joinCodeInputField.text);});
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyCreateUI : MonoBehaviour
{
    public static LobbyCreateUI instance {get; private set;}
    [SerializeField] private Button privateLobbyButton;
    [SerializeField] private Button publicLobbyButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_InputField lobbyNameInputField;
    

    private void Awake()
    {
        privateLobbyButton.onClick.AddListener(() => {KitchenGameLobby.Instance.CreateLobby(lobbyNameInputField.text, true);});
        publicLobbyButton.onClick.AddListener(() => {KitchenGameLobby.Instance.CreateLobby(lobbyNameInputField.text, false);});
        closeButton.onClick.AddListener(() => {Hide();});
    }
    private void Start()
    {
        Hide();
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

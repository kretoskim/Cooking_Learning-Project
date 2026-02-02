using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance {get; private set;}
     [SerializeField] private Button soundEffectsButton;
     [SerializeField] private Button musicButton;
     [SerializeField] private Button closeButton;
     [SerializeField] private Button moveUpButton;
     [SerializeField] private Button moveDownButton;
     [SerializeField] private Button moveLeftButton;
     [SerializeField] private Button moveRightButton;
     [SerializeField] private Button interactButton;
     [SerializeField] private Button interactAltButton;
     [SerializeField] private Button pauseButton;
      [SerializeField] private Button gamepadinteractButton;
     [SerializeField] private Button gamepadinteractAltButton;
     [SerializeField] private Button gamepadpauseButton;
     [SerializeField] private TextMeshProUGUI soundEffectsText;
     [SerializeField] private TextMeshProUGUI musicText;
     [SerializeField] private TextMeshProUGUI moveUpText;
     [SerializeField] private TextMeshProUGUI moveDownText;
     [SerializeField] private TextMeshProUGUI moveLeftText;
     [SerializeField] private TextMeshProUGUI moveRightText;
     [SerializeField] private TextMeshProUGUI interactText;
     [SerializeField] private TextMeshProUGUI interactAltText;
     [SerializeField] private TextMeshProUGUI pauseText;
     [SerializeField] private TextMeshProUGUI gamepadinteractText;
     [SerializeField] private TextMeshProUGUI gamepadinteractAltText;
     [SerializeField] private TextMeshProUGUI gamepadpauseText;
     [SerializeField] private Transform pressToRebindKeyTransform;
     private Action onCloseButtonAction;

    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });
        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
        gamepadinteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Gamepad_Interact); });
        gamepadinteractAltButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Gamepad_InteractAlternate); });
        gamepadpauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Gamepad_Pause); });   
    }
    private void Start()
    {
        GameManager.Instance.OnLocalGamePaused += GameManager_OnGamePaused;
        
        UpdateVisual();

        HidePressToRebindKey();
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadinteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadinteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadpauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }
    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        soundEffectsButton.Select();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }
    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
         HidePressToRebindKey();
         UpdateVisual();   
        });
    }
}

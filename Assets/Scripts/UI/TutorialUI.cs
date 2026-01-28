using UnityEngine;
using TMPro;
using System;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI KeyMoveUpText;
    [SerializeField] private TextMeshProUGUI KeyMoveDownText;
    [SerializeField] private TextMeshProUGUI KeyMoveLeftText;
    [SerializeField] private TextMeshProUGUI KeyMoveRightText;
    [SerializeField] private TextMeshProUGUI KeyInteractText;
    [SerializeField] private TextMeshProUGUI KeyInteractAltText;
    [SerializeField] private TextMeshProUGUI KeyPauseText;
    [SerializeField] private TextMeshProUGUI KeyGamepadInteractText;
    [SerializeField] private TextMeshProUGUI KeyGamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI KeyGamepadPauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.IsLocalPlayerReadyChanged += GameManager_IsLocalPlayerReadyChanged;

        UpdateVisual();

        Show();
    }

    private void GameManager_IsLocalPlayerReadyChanged(object sender, EventArgs e)
    {
        if(GameManager.Instance.IsLocalPlayerReady())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        KeyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        KeyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        KeyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        KeyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        KeyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        KeyInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        KeyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        KeyGamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        KeyGamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        KeyGamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
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

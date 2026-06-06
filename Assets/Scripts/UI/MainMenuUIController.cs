using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button languageButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
        languageButton.onClick.AddListener(OnLanguageClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveListener(OnStartClicked);
        languageButton.onClick.RemoveListener(OnLanguageClicked);
        quitButton.onClick.RemoveListener(OnQuitClicked);
    }

    private void OnStartClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
    }

    private void OnLanguageClicked()
    {
        if (GameManager.Instance != null && GameManager.Instance.LocalizationManager != null)
        {
            GameManager.Instance.LocalizationManager.ToggleLocale();
        }
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }
}
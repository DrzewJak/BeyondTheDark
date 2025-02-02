using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Buttons")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backFromSettingsButton;
    [SerializeField] private Button backFromCreditsButton;

    [Header("Game Start")]
    [SerializeField] private string firstGameSceneName = "GameScene";

    [Header("Additional Effects")]
    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource buttonClickSound;

    private void Start()
    {
        // Initialize button listeners
        SetupButtonListeners();

        ShowMainMenu();
    }

    private void SetupButtonListeners()
    {
        // Main Menu Buttons
        startGameButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettingsMenu);
        creditsButton.onClick.AddListener(OpenCreditsMenu);
        quitButton.onClick.AddListener(QuitGame);

        // Back Buttons
        backFromSettingsButton.onClick.AddListener(ShowMainMenu);
        backFromCreditsButton.onClick.AddListener(ShowMainMenu);
    }

    private void StartGame()
    {
        PlayButtonClickSound();
        
        SceneManager.LoadScene(firstGameSceneName);
    }

    private void OpenSettingsMenu()
    {
        PlayButtonClickSound();
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    private void OpenCreditsMenu()
    {
        PlayButtonClickSound();
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    private void ShowMainMenu()
    {
        PlayButtonClickSound();
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    private void QuitGame()
    {
        PlayButtonClickSound();

        #if UNITY_EDITOR
            // Quit play mode in Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Quit the application
            Application.Quit();
        #endif
    }

    private void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();
        }
    }

    // Optional: Fade in/out menu music
    public void SetMenuMusicVolume(float volume)
    {
        if (menuMusic != null)
        {
            menuMusic.volume = volume;
        }
    }

    // Optional: Start menu music on enable
    private void OnEnable()
    {
        if (menuMusic != null)
        {
            menuMusic.Play();
        }
    }

    // Optional: Stop menu music on disable
    private void OnDisable()
    {
        if (menuMusic != null)
        {
            menuMusic.Stop();
        }
    }
}
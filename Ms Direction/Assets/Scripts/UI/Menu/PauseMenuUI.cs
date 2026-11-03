using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pauseUI;
    public GameObject pausePanel;
    public GameObject pauseOptionsPanel;

    [Header("Scenes")]
    public string mainMenuSceneName = "MainMenu";

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;

        pauseUI.SetActive(true);
        pausePanel.SetActive(true);
        pauseOptionsPanel.SetActive(false);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;

        pauseUI.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenOptions()
    {
        pausePanel.SetActive(false);
        pauseOptionsPanel.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        pauseOptionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

#if UNITY_EDITOR
        Debug.Log("Quit Game");
#endif
    }
}
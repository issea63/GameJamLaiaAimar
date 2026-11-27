using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour
{
    [SerializeField]GameObject menuUI;

    private void Start()
    {
        menuUI = GameObject.FindWithTag("MenuUI");
        Time.timeScale = 1f;
        if (menuUI != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            menuUI.SetActive(false);
        }
    }
    public void OnMenu()
    {
        if (menuUI != null)
        {
            Cursor.lockState = CursorLockMode.None;
            menuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
    public void Resume()
    {
        if (menuUI != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            menuUI.SetActive(false);
        }
    }
}

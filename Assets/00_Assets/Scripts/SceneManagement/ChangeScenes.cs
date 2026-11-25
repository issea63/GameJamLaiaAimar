using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Play()
    {
        SceneLoadManager.instance.LoadRandomLevel();
    }
    public void ChangeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void ExitGame()
    { 
        Application.Quit();
    }
}

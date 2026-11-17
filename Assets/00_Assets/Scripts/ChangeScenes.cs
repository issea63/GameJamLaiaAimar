using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void ChangeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void ExitGame()
    { 
        Application.Quit();
    }
}

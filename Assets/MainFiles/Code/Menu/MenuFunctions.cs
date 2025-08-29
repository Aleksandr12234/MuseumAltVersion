using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    public void Setup()
    {
        //SceneManager.LoadSceneAsync("MainScene");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitPressed()
    {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
}

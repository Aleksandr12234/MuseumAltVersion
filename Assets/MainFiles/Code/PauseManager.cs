using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _scenesObjects;
    [SerializeField] private GameObject _pause;
    void Update()
    {
        if (_scenesObjects.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            _scenesObjects.SetActive(false);
            _pause.SetActive(true);
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

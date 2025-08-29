using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private Image _progressBar;

    public void Start()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneToLoad);
        while (!operation.isDone)
        {
            Debug.Log(operation.progress/0.9f);
            _progressBar.fillAmount=operation.progress/0.9f;
            yield return null;
        }
    }
}

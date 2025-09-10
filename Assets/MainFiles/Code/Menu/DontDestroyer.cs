using UnityEngine;

public class DontDestroyer : MonoBehaviour
{
    public static DontDestroyer instance;

    void Start()
    {
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

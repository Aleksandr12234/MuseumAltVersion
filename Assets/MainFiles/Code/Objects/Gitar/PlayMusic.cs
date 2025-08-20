using UnityEngine;

public class PlayMusic : MonoBehaviour, IUsebleObject
{
    [SerializeField] private AudioSource _audio;
    private bool isPlaying;

    void Start()
    {
        isPlaying = false;
        _audio.Pause();
    }

    void LateUpdate()
    {
        if (!isPlaying && _audio.isPlaying)
        {
            _audio.Pause();
        }
        isPlaying = false;
    }

    public void ActiveAction()
    {
        isPlaying = true;
        if (!_audio.isPlaying) _audio.Play();
    }

    public void Action() { }
}

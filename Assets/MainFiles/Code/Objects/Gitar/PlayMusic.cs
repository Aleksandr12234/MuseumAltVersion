using UnityEngine;

public class PlayMusic : MonoBehaviour, IUsebleObject
{
    [SerializeField] private AudioSource _audio;
    private bool isPlaying;

    public AudioManager AudioManager;

    void Start()
    {
        isPlaying = false;
        _audio.Pause();
    }

    void LateUpdate()
    {
        if (!isPlaying && _audio.isPlaying)
        {
            //AudioSourceExtensions.FadeOut(_audio, this, 0.2f); - bugs, dont' play on second enter. No much time for solving
            _audio.Pause();
            AudioManager.UnmuteAmbient();
        }
        isPlaying = false;
    }

    public void ActiveAction()
    {
        isPlaying = true;
        if (!_audio.isPlaying) _audio.Play();
        AudioManager.MuteAmbient();
    }

    public void Action() { }
}

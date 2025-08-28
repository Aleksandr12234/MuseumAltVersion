using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;
using static UnityEngine.UI.Button;

public class TapeRecoreder : MonoBehaviour, IUsebleObject
{

    private AudioSource _source;

    public AudioManager AudioManager;

    void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.Pause();
    }

    public void Action()
    {
        if (_source.isPlaying)
        {
            AudioSourceExtensions.FadeOut(_source, this, 0.1f);
            _source.Pause();
            AudioManager.UnmuteAmbient();
            //here may be placed animation control (rotation of rollers)

        }
        else
        {
            AudioSourceExtensions.FadeIn(_source, this, 0.1f);
            _source.Play();
            AudioManager.MuteAmbient();
        }
    }
}

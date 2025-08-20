using UnityEngine;
using UnityEngine.Video;

public class OnOff : MonoBehaviour, IUsebleObject
{

    private VideoPlayer _player;
    private Material _material;

    void Start()
    {
        _player = GetComponent<VideoPlayer>();
        _material = GetComponent<Renderer>().material;
        _player.Pause();
        _material.SetColor("_EmissionColor", Color.black);
    }

    public void Action()
    {
        if (_player.isPlaying)
        {
            _player.Pause();
            _material.SetColor("_EmissionColor", Color.black);

        }
        else
        {
            _player.Play();
            _material.SetColor("_EmissionColor", Color.white);
        }
    }
}

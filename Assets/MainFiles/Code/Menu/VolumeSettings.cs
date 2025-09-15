using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _parametrName;
    [SerializeField] private TextMeshProUGUI _output;
    private int _volume=100;

    public void Start()
    {
        _audioMixer.GetFloat(_parametrName, out float сurrentVolume);
        _volume = (int)Math.Round(Math.Pow(10, сurrentVolume/20f) * 100);
        UpdateValues();
    }

    public void UpVolume()
    {
        _volume ++;
        UpdateValues();
    }

    public void DownVolume()
    {
        _volume --;
        UpdateValues();
    }

    private void UpdateValues()
    {
        _volume = Math.Clamp(_volume, 0, 100);

        float dbValue = _volume > 0 ? 
            20f * Mathf.Log10(_volume / 100f) : 
            -80f;

        _audioMixer.SetFloat(_parametrName, dbValue);
        if (_output != null) _output.text = "" + _volume;
    }
}

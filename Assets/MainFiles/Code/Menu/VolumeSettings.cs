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

    public void UpVolume()
    {
        _volume++;
        UpdateValues();
    }

    public void DownVolume()
    {
        _volume--;
        UpdateValues();
    }

    private void UpdateValues()
    {
        _volume = Math.Clamp(_volume, 0, 100);
        _audioMixer.SetFloat(_parametrName, Math.Clamp(-80f + _volume / 100f * 80f, -80, 0));
        if (_output != null) _output.text = "" + _volume;
    }
}

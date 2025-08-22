using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [SerializeField]private AudioListener PlayerAudioListener;

    [SerializeField] private AudioMixer _audioMixer;

    public int VolumeMaster { get; private set; }
    public int VolumeAmbient { get; private set; }

    public int VolumeObjects { get; private set; }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetMasterVol()
    {
        _audioMixer.SetFloat("MasterVolRaw", VolumeMaster / 10f);
    }
    private void SetAmbientVol()
    {
        _audioMixer.SetFloat("AmbientVolRaw", VolumeMaster / 10f);
    }

    private void SetObjectsVol()
    {
        _audioMixer.SetFloat("ObjectsVolRaw", VolumeMaster / 10f);
    }


    public void MuteAmbient()
    {
        _audioMixer.SetFloat("AmbientVolRaw", -80f);
        //for UI volume setting will need a convertore method - from (0;10) to (-80;20) - or what dBls needs
    }

    public void UnmuteAmbient()
    {
        SetAmbientVol();
    }
}

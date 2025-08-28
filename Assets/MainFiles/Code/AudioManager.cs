using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioListener PlayerAudioListener;
    [SerializeField] private AudioMixer _audioMixer;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1.0f;

    public int VolumeMaster { get; private set; } = 10;
    public int VolumeAmbient { get; private set; } = 10;
    public int VolumeObjects { get; private set; } = 10;

    private const float MinVolume = -80f;
    private const float MaxVolume = 0f;

    private void SetVolume(string parameterName, float volumeDB)
    {
        _audioMixer.SetFloat(parameterName, volumeDB);
    }

    public void MuteAmbient() => StartCoroutine(FadeAmbient(MinVolume));
    public void UnmuteAmbient() => StartCoroutine(FadeAmbient(ConvertToDB(VolumeAmbient)));

    private IEnumerator FadeAmbient(float targetVolume)
    {
        string parameter = "AmbientVolRaw";
        float currentVolume;
        _audioMixer.GetFloat(parameter, out currentVolume);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(currentVolume, targetVolume, elapsedTime / fadeDuration);
            SetVolume(parameter, newVolume);
            yield return null;
        }

        SetVolume(parameter, targetVolume);
    }

    private float ConvertToDB(int volume)
    {
        // Конвертация из линейной шкалы [0;10] в логарифмическую [-80;0]
        return volume == 0 ? MinVolume : Mathf.Log10(volume / 10f) * 20f;
    }

    // Аналогичные методы для других звуковых групп
    public void MuteMaster() => StartCoroutine(FadeChannel("MasterVolRaw", MinVolume));
    public void UnmuteMaster() => StartCoroutine(FadeChannel("MasterVolRaw", ConvertToDB(VolumeMaster)));

    private IEnumerator FadeChannel(string parameter, float targetVolume)
    {
        float currentVolume;
        _audioMixer.GetFloat(parameter, out currentVolume);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(currentVolume, targetVolume, elapsedTime / fadeDuration);
            SetVolume(parameter, newVolume);
            yield return null;
        }

        SetVolume(parameter, targetVolume);
    }
}
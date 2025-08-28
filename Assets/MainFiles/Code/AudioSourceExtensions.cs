using UnityEngine;
using System.Collections;

public static class AudioSourceExtensions
{
    public static void FadeIn(this AudioSource audioSource, MonoBehaviour coroutineRunner, float duration = 1f)
    {
        coroutineRunner.StartCoroutine(FadeAudioSource(audioSource, 0f, 1f, duration, true));
    }

    public static void FadeOut(this AudioSource audioSource, MonoBehaviour coroutineRunner, float duration = 1f)
    {
        coroutineRunner.StartCoroutine(FadeAudioSource(audioSource, audioSource.volume, 0f, duration, true));
    }

    private static IEnumerator FadeAudioSource(AudioSource audioSource, float startVolume, float targetVolume, float duration, bool stopOnComplete = false)
    {
        float currentTime = 0f;

        if (startVolume > targetVolume && !audioSource.isPlaying)
        {
            audioSource.volume = 0f;
            audioSource.Play();
        }

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.SmoothStep(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;

        if (stopOnComplete && Mathf.Approximately(targetVolume, 0f))
        {
            audioSource.Pause();
        }
    }
}
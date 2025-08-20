using UnityEngine;
using UnityEngine.Audio;

public class StepSound : MonoBehaviour
{
    [SerializeField] private AudioResource[] _steps;
    [SerializeField] private AudioSource _audioSourceSteps;

    public void Step()
    {
        if (!_audioSourceSteps.isPlaying)
        {
            int a = Random.Range(0, _steps.Length);
            _audioSourceSteps.resource = _steps[a];
            _audioSourceSteps.Play();
        }
    }
}

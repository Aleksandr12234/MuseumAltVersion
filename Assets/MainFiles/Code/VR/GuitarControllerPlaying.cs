using UnityEngine;
using System.Collections;

public class GuitarControllerPlaying : MonoBehaviour
{
    [SerializeField] private PlayMusic _playMusic;
    [SerializeField] private float _speedThreshold = 0.1f; // Порог скорости
    [SerializeField] private float _bufferTime = 0.2f;

    private bool _isInTrigger;
    private bool _isPlaying;
    private Coroutine _musicBufferCoroutine;


    private void Update()
    {
        if (_isPlaying)
        {
        
            _playMusic.ActiveAction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AffordanceCaller"))
        {
            _isInTrigger = true;
            CheckAndPlayMusic(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("AffordanceCaller"))
        {
            Debug.Log("OnStay");
            _isInTrigger = true;
            CheckAndPlayMusic(other);
        }
    }

    private void CheckAndPlayMusic(Collider other)
    {
        VelocityTracker tracker = other.GetComponent<VelocityTracker>();
        Debug.Log("Check");
        if (tracker != null && tracker.Speed > _speedThreshold)
        {
            Debug.Log("its' good");
            StartMusicWithBuffer();
        }
    }

    private void StartMusicWithBuffer()
    {
        if (!_isPlaying)
        {
            _isPlaying = true;
            _playMusic.ActiveAction();
        }

        // Сбрасываем таймер буфера
        if (_musicBufferCoroutine != null)
        {
            StopCoroutine(_musicBufferCoroutine);
        }
        _musicBufferCoroutine = StartCoroutine(MusicBufferRoutine());
    }

    private IEnumerator MusicBufferRoutine()
    {
        yield return new WaitForSeconds(_bufferTime);
        // Если за время буфера не было нового движения, останавливаем
        _isPlaying = false;
    }

    private IEnumerator StopMusicAfterDelay()
    {
        yield return new WaitForSeconds(_bufferTime);
        _isPlaying = false;
    }
}
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(House))]
[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private House _house;
    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private float _volumeSpeed = 0.2f;

    private void Awake()
    {
        _house = GetComponent<House>();
        _audioSource = GetComponent<AudioSource>();

        _audioSource.volume = 0f;

        _house.OnAlarmVolumeChanging += RunCoroutine;
    }

    private void RunCoroutine(float targetVolume)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (targetVolume == _house.MinAlarmVolume)
            _audioSource.Play();

        _coroutine = StartCoroutine(ChangingVolume(targetVolume));
    }

    private IEnumerator ChangingVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeSpeed * Time.deltaTime);

            yield return null;
        }

        if (_audioSource.volume == 0)
            _audioSource.Stop();
    }
}

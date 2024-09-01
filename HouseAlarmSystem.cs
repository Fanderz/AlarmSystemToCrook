using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HouseAlarmSystem : MonoBehaviour
{
    private float _maxVolume = 1f;

    private AudioSource _audioSource;
    private Coroutine _coroutine;
    private WaitForSeconds wait;

    private void Start()
    {
        gameObject.TryGetComponent(out _audioSource);
        wait = new WaitForSeconds(0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Robber>() != null)
        {
            _audioSource.volume = 0f;
            _audioSource.Play();
        }

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(IncreaseSound());
    }

    private void OnCollisionExit()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(DecreaseSound());

        if (_audioSource.volume == 0f)
            _audioSource.Stop();
    }

    private IEnumerator IncreaseSound()
    {
        while (_audioSource.volume < _maxVolume)
        {
            _audioSource.volume += 0.1f;

            yield return wait;
        }
    }

    private IEnumerator DecreaseSound()
    {
        while (_audioSource.volume > 0f)
        {
            _audioSource.volume -= 0.1f;

            yield return wait;
        }
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class House : MonoBehaviour
{
    private float _maxAlarmVolume = 1f;
    private float _minAlarmVolume = 0f;

    public event Action<float> OnAlarmVolumeChanging;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Robber>() != null)
            OnAlarmVolumeChanging.Invoke(_maxAlarmVolume);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Robber>() != null)
            OnAlarmVolumeChanging.Invoke(_minAlarmVolume);
    }
}

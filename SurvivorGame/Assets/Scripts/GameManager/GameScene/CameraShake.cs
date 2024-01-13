using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeDuration;
    private bool isShaking = false;

    private void Start()
    {
        instance = this;
    }

    public void SetThePlayer()
    {
        cinemachineVirtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Shake(float duration, float intensity)
    {
        if (!isShaking)
        {
            shakeDuration = duration;
            StartCoroutine(ShakeRtn(duration, intensity));
        }
    }

    IEnumerator ShakeRtn(float duration, float intensity)
    {
        isShaking = true;

        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        float startAmplitude = intensity;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float currentAmplitude = Mathf.Lerp(startAmplitude, 0f, elapsed / duration);
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = currentAmplitude;
            yield return null;
        }

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        isShaking = false;
    }
}

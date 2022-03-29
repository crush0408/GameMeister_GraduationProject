using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraActionScript : MonoBehaviour
{
    public static CameraActionScript instance { get; private set; }

    private CinemachineVirtualCamera vCam;
    private CinemachineBasicMultiChannelPerlin bPerlin;

    private bool isShake = false;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("다수의 카메라 액션 스크립트");
            Destroy(this.gameObject);
        }
        else
        {

            instance = this;
            vCam = GetComponent<CinemachineVirtualCamera>();
            bPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }
    public static void ShakeCam(float intensity, float time)
    {
        if (instance.isShake) return;

        instance.isShake = true;
        instance.StartCoroutine(instance.ShakeUpdate(intensity, time));
    }
    public IEnumerator ShakeUpdate(float intensity, float time)
    {
        bPerlin.m_AmplitudeGain = intensity;
        float t = 0;
        while (true)
        {
            yield return null;
            t += Time.deltaTime;
            if(t >= time)
            {
                break;
            }
            bPerlin.m_AmplitudeGain = Mathf.Lerp(intensity,0,t/time);
        }

        bPerlin.m_AmplitudeGain = 0f;
        isShake = false;
    }
}

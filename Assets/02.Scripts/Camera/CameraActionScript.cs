using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraActionScript : MonoBehaviour
{
    public static CameraActionScript instance;
    public CinemachineVirtualCamera player_vCam;
    public GameObject playerCanvas;
    public CinemachineBrain brain;


    bool isShake = false;
    bool isZooming = false;
    bool isMoving = false;

    float tempSize = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            brain = Camera.main.GetComponent<CinemachineBrain>();
            playerCanvas.SetActive(false);
        }
        else
        {
            Destroy(this);
        }
    }


    public static void SwapCamera(CinemachineVirtualCamera cam, float time)
    {
        if (instance.isMoving) return;
        instance.StartCoroutine(instance.SwapCameraCoroutine(cam, time));
    }
    public IEnumerator SwapCameraCoroutine(CinemachineVirtualCamera cam, float time)
    {
        player_vCam.enabled = false;
        cam.enabled = true;
        isMoving = true;
        yield return new WaitForSeconds(time);
        player_vCam.enabled = true;
        cam.enabled = false;
        isMoving = false;
    }
    public static void ShakeCam(float intensity, float time, bool isPlayer, CinemachineVirtualCamera vCam = null)
    {
        if (instance.isShake) return;
        
        if (vCam == null)
        {
            vCam = instance.player_vCam;
        }

        instance.isShake = true;
        instance.StartCoroutine(instance.ShakeUpdate(intensity, time, vCam, isPlayer));
    }
    public IEnumerator ShakeUpdate(float intensity, float time, CinemachineVirtualCamera vCam, bool isPlayer)
    {
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        if (isPlayer)
        {
            playerCanvas.SetActive(true);
        }
        float t = 0;
        while (true)
        {
            yield return null;
            t += Time.deltaTime;
            if (t >= time)
            {
                break;
            }
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.Lerp(intensity, 0, t / time);
        }

        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        if (isPlayer)
        {
            playerCanvas.SetActive(false);
        }
        isShake = false;
    }
    public static void BossSceneCamera(CinemachineVirtualCamera bCam, CinemachineTargetGroup target)
    {
        instance.GroupTargetCamera(bCam, target);
    }
    public void GroupTargetCamera(CinemachineVirtualCamera bCam, CinemachineTargetGroup target)
    {
        bCam.enabled = true;
        target.m_Targets[1].target = GameManager.instance.playerObj.transform;
    }
    public static void ZoomIn(float size, float time, CinemachineVirtualCamera vCam = null)
    {

        if (vCam == null)
        {
            vCam = instance.player_vCam;
        }
        if (instance.isZooming) return;

        instance.isZooming = true;
        instance.StartCoroutine(instance.ZoomInUpdate(size, time, vCam));
    }
    public IEnumerator ZoomInUpdate(float size, float time, CinemachineVirtualCamera vCam)
    {
        tempSize = vCam.m_Lens.OrthographicSize;


        float t = 0;
        while (true)
        {
            yield return null;
            t += Time.deltaTime;
            if (t >= time)
            {
                break;
            }
            vCam.m_Lens.OrthographicSize = Mathf.Lerp(tempSize, size, t / time);
        }
        vCam.m_Lens.OrthographicSize = size;
    }
    public static void ZoomOut(float time, CinemachineVirtualCamera vCam = null)
    {
        if (vCam == null)
        {
            vCam = instance.player_vCam;
        }
        if (!instance.isZooming) return;
        instance.StartCoroutine(instance.ZoomOutUpdate(time, vCam));
    }
    public IEnumerator ZoomOutUpdate(float time, CinemachineVirtualCamera vCam)
    {
        float size = vCam.m_Lens.OrthographicSize;
        float t = 0;
        while (true)
        {
            yield return null;
            t += Time.deltaTime;
            if (t >= time)
            {
                break;
            }
            vCam.m_Lens.OrthographicSize = Mathf.Lerp(size, tempSize, t / time);
        }
        vCam.m_Lens.OrthographicSize = tempSize;
        isZooming = false;
    }
}

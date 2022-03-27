using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class DemonKnife : MonoBehaviour
{
    //여기서 플레이어 피격 처리?
    //public LayerMask whatIsEnemy;

    public int damage;
    private CinemachineVirtualCamera vCam;

    private void Awake()
    {
        vCam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable DamageObj = collision.gameObject.GetComponent<IDamageable>();

            if (DamageObj != null)
            {
                Vector3 contact = collision.transform.position;

                DamageObj.OnDamage(damage, contact);
                vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 4;
                Invoke("Return", 0.3f);
            }
        }
    }

    void Return()
    {
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;

    [Header("총알 생성 간격")]
    public float time = 1f;

    private void Start()
    {
        CreateBullet();
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.transform.SetParent(transform, false);

        Invoke("CreateBullet", time);
    }
}

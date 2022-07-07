using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DonDestroy : MonoBehaviour
{
    public CinemachineVirtualCamera player_vCam;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        player_vCam.Follow = GameObject.Find("Player").transform;
    }
}

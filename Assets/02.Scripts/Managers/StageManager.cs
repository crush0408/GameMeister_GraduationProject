using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public MapData[] mapDatas = null;
    public int index = 0;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Init()
    {
        CinemachineConfiner b = new CinemachineConfiner();
        b.m_BoundingShape2D = mapDatas[index].col;
        b.m_Damping = mapDatas[index].damping;
        CameraActionScript.instance.player_vCam.AddExtension(b);
    }

}
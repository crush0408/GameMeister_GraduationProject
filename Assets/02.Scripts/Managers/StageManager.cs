using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public GameObject player;
    public CinemachineConfiner confiner;

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

    private void Start()
    {
        player = GameManager.instance.playerObj;
        confiner = CameraActionScript.instance.player_vCam.gameObject.GetComponent<CinemachineConfiner>();
        Init();
    }
    public void Init()
    {
        Reset();

        GameObject a = Instantiate(mapDatas[index].mapPrefab, this.transform);
        mapDatas[index].insertData = a.GetComponent<MapManager>().insertData;
        player.transform.position = mapDatas[index].insertData.startPos.position;
        confiner.m_BoundingShape2D = mapDatas[index].insertData.vCamCollider;
    }
    public void Reset()
    {
        if(transform.childCount > 0)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            index++;
            Init();
        }
        else if(Input.GetKeyDown(KeyCode.V))
        {
            index--;
            Init();
        }
    }

}
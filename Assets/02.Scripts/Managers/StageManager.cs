using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public GameObject player;
    public CinemachineConfiner confiner;


    public GameObject[] mapDatas = null;
    public MapInsertData insertData;
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
        //Init(0);
    }
    public void Init(int adjust)
    {
        index += adjust;
        Reset();


        GameObject map = Instantiate(mapDatas[index].gameObject, this.transform);
        insertData = map.GetComponent<MapManager>().insertData;
        map.GetComponent<MapManager>().insertData.door.SetActive(false);
        insertData.boss.GetComponent<EnemyHealth>().OnDead += () => 
        { map.GetComponent<MapManager>().insertData.door.SetActive(true); };
        player.transform.position = insertData.startPos.position;
        confiner.m_BoundingShape2D = insertData.vCamCollider;
    }
    public void Reset()
    {
        if(transform.childCount > 0)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
    }

    
}
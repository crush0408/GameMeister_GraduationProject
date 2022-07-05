using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public GameObject player;
    public CinemachineConfiner confiner;


    public GameObject[] mapDatas = null;
    public MapInsertData insertData;
    public int index = 0;
    public int maxIndex = 0;
    int a = 0;


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
        maxIndex = mapDatas.Length;
    }

    private void Start()
    {
        player = GameManager.instance.playerObj;
        confiner = CameraActionScript.instance.player_vCam.gameObject.GetComponent<CinemachineConfiner>();
        Init(0);
    }

    public void Init(int adjust)
    {
        index += adjust;
        if(index >= maxIndex)
        {
            SceneLoadManager.instance.LoadScene("EndScene");
            return;
        }
        Reset();

        GameObject map = Instantiate(mapDatas[index].gameObject, this.transform);
        insertData = map.GetComponent<MapManager>().insertData;

        if (!insertData.isStore)
        {
            insertData.door.SetActive(false);
            Debug.Log("상점이라 문 없앰");
        }

        if (insertData.rewardItem != null)
        {
            insertData.rewardItem.SetActive(false);
        }

        if(insertData.notFight)
        {
            insertData.enemy = null;
        }

        if(insertData.enemy !=null)
        {
            for (int i = 0; i < insertData.enemy.Length; i++)
            {
                insertData.enemy[i].GetComponent<EnemyHealth>().OnDead += () =>
                {

                    a++;

                    if (a == insertData.enemy.Length)
                    {
                        insertData.door.SetActive(true);
                        if (insertData.rewardItem != null)
                        {
                            insertData.rewardItem.SetActive(true);
                        }
                        GameManager.instance.TypeReward();
                    }
                };
            }
        }
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
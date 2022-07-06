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
    int b = 0;

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

    private void Update()
    {
        if (insertData.isTuto)
        {
            SceneManager.LoadScene("PlayScene");
        }
        if(insertData.enemy!= null)
        {
            b = insertData.enemy.Length;

            Debug.Log(b);
        }
        Debug.Log(a);

        if (index == 2)
        {
            confiner.m_BoundingShape2D = null;
        }
        else
        {
            confiner.m_BoundingShape2D = insertData.vCamCollider;
        }

        if (insertData.enemy != null)
        {

            for (int i = 0; i < b; i++)
            {
                insertData.enemy[i].GetComponent<EnemyHealth>().OnDead += () =>
                {

                    a++;

                    if (a == b)
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

        

        player.transform.position = insertData.startPos.position;
    }
    public void Reset()
    {
        if(transform.childCount > 0)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
    }
}
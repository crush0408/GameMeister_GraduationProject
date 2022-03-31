using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarManager : MonoBehaviour
{
    public GameObject hpBarPrefab;
    public string enemyTagName;

    [SerializeField] List<Transform> enemyTransfroms = new List<Transform>();
    List<GameObject> hpBarList = new List<GameObject>();

    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;

        GameObject[] enemyObjs = GameObject.FindGameObjectsWithTag(enemyTagName);

        for (int i = 0; i < enemyObjs.Length; i++)
        {
            enemyTransfroms.Add(enemyObjs[i].transform);
            GameObject hpBar = Instantiate(hpBarPrefab, enemyObjs[i].transform.position, Quaternion.identity);
            hpBarList.Add(hpBar);
        }
    }

    private void Update()
    {
        for (int i = 0; i < enemyTransfroms.Count; i++)
        {
            hpBarList[i].transform.position = enemyTransfroms[i].position + new Vector3(0, 0.15f, 0);
        }
    }
}

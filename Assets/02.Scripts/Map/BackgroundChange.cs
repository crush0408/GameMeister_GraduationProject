using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChange : MonoBehaviour
{
    public GameObject[] backgrounds;
    public GameObject[] enemies;

    public GameObject curEnemy;

    int num = 0;

    private void Start()
    {
        for(int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
        }

        for(int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].activeInHierarchy)
            {
                curEnemy = enemies[i];
                ChangeBackground(i);
            }
        }
    }

    private void Update()
    {
        if (!curEnemy.activeInHierarchy)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].activeInHierarchy)
                {
                    curEnemy = enemies[i];
                    ChangeBackground(i);
                }
            }
        }
    }

    private void ChangeBackground(int number)
    {
        backgrounds[num].SetActive(false);

        num = number;
        backgrounds[num].SetActive(true);
    }
}

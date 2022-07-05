using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapInsertData
{
    public Transform startPos;
    public GameObject door;
    public GameObject rewardItem;

    public GameObject[] storeItems;

    public GameObject[] enemy;

    public Collider2D vCamCollider;

}

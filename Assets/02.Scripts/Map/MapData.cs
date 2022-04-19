using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{
    public GameObject mapPrefab;
    public Transform startTrm;

    public Collider2D col = null;

    public int enemyCount = 0;
}

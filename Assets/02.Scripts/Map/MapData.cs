using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{
    public GameObject mapPrefab;
    public Transform startTrm;
    public Transform endTrm;
    public Collider2D col = null;
    public float damping = 5f;
    public int enemyCount = 0;
    public GameObject endDoor;
}

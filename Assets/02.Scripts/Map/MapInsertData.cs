using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapInsertData
{
    public Transform startPos;
    public Transform endPos;

    public Collider2D vCamCollider;

    public int enemyCount;
}

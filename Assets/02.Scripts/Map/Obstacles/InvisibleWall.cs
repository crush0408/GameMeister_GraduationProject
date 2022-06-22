using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InvisibleWall : MonoBehaviour
{
    Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tilemap.color = Color.clear;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tilemap.color = Color.white;
    }
}

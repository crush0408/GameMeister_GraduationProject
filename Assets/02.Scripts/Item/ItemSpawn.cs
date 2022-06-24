using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject item;

    private void OnEnable()
    {
        item.SetActive(true);
        item.transform.SetParent(transform);

        //while(item.GetComponent<Transform>().position.y < transform.position.y + 10)
        //{
        //    item.GetComponent<Transform>().Translate(Vector3.up * 30f * Time.deltaTime);
        //}
    }

    private void Update()
    {
        if (item.GetComponent<Transform>().position.y <= transform.position.y + 10)
        {
            item.GetComponent<Transform>().Translate(Vector3.up * 30f * Time.deltaTime);
        }
    }
}

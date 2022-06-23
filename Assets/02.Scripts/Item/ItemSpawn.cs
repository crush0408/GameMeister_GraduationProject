using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject itemPrefab;
    private GameObject item;

    private void OnEnable()
    {
        item = Instantiate(itemPrefab);

        while(item.transform.position.y > gameObject.transform.position.y + 10f)
        {
            item.GetComponent<Transform>().Translate(Vector3.up * 10f);
        }
    }

    //IEnumerator SpawnItem()
    //{
    //    yield return 
    //}
}

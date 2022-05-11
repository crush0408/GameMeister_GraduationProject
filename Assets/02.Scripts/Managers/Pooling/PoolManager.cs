using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public PoolableMono[] prefabs;
    public static PoolManager Instance;
    
    private Dictionary<string, Pool<PoolableMono>> _pools = new Dictionary<string, Pool<PoolableMono>>();


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple PoolManager is running");
        }
        Instance = this;
        //CreatePool(prefabGo.GetComponent<PoolableMono>());

        for (int i = 0; i < prefabs.Length; i++)
        {
            CreatePool(prefabs[i]);
        }
    }

    public void CreatePool(PoolableMono prefab)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, transform);
        _pools.Add(prefab.gameObject.name, pool);
        
    }

    public PoolableMono Pop(string prefabName)
    {
        if(!_pools.ContainsKey(prefabName))
        {   
            //Debug.LogError("Prefab doesnt exist on pool");
            return null;
        }

        PoolableMono item = _pools[prefabName].Pop();
        item.Reset();
        return item;
    }

    public void Push(PoolableMono obj)
    {
        _pools[obj.name].Push(obj);
    }

}

using System;
using System.Collections.Generic;
using UnityEngine;

class Pool<T> where T: PoolableMono
{
    private Stack<T> _pool = new Stack<T>();
    private T _prefab; //오리지날 저장
    private Transform _parent;

    public Pool(T prefab, Transform parent, int count = 10)
    {
        _prefab = prefab;
        _parent = parent;
        
        for (int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");  //클론이라는 이름 제거해줘야 다시 쓸 수 있음.
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }

    public T Pop()
    {
        T obj = null;
        if(_pool.Count <= 0)
        {
            obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            
        }
        else
        {
            obj = _pool.Pop();
            obj.gameObject.SetActive(true);
        }
        return obj;
    }
    
    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }
}


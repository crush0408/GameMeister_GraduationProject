using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObject : PoolableMono
{
    

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Return());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Return(){
        yield return new WaitForSeconds(1f);
        //PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {

    }
}

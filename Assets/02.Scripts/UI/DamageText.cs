using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageText : PoolableMono
{
    [SerializeField]
    private TextMesh text;

    public override void Reset()
    {
        int random = Random.Range(0, 10);
        if(random % 2 == 0)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.yellow;
        }
        /*Vector3 randomTrm = new Vector3(Random.Range(-0.51f, 0.5f), Random.Range(-0.1f, 1f),0);

        transform.localPosition += randomTrm;*/

        
    }

    public void PlayFloating(string damageString)
    {
        text.text = damageString;

        float random = Random.Range(1f, 2.5f);
        transform.DOMove(transform.position + new Vector3(Random.Range(-1f,1f),random,0) , 0.25f).OnComplete(() => {
            //transform.DOMove(transform.position + new Vector3(0,-random - 1,0), 0.5f).OnComplete(() =>
            //{

                PushFunc();
            //});
        });
    }
    public void PushFunc()
    {
        PoolManager.Instance.Push(this);
    }
}

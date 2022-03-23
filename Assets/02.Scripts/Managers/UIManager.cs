using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        MGSound.instance.playBgm("somewhere");
    }

    public void Move(GameObject obj, Vector3 targetTransform, float time, Ease easetype)
    {
        obj.transform.DOMove(targetTransform, time).SetEase(easetype);
    }

    public void Shake(GameObject obj, float time)
    {
        obj.transform.DOShakePosition(time);
    }

    public void ReMove(GameObject obj)
    {
        obj.transform.DORewind();
    }
}

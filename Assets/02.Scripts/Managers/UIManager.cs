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

    public void Move(GameObject obj, Vector3 targetTransform, float time, Ease easetype)
    {
        obj.transform.DOMove(targetTransform, time).SetEase(easetype);
    }

    public void Shake(GameObject obj, float time)
    {
        obj.transform.DOShakePosition(time);
    }

    public void Remove(GameObject obj)
    {
        obj.transform.DORewind();
    }

    public void PanelOnOff(GameObject obj, KeyCode key)
    {
        bool isOn = obj.activeSelf ? true : false;

        if (Input.GetKeyDown(key))
        {
            obj.SetActive(!isOn);
        }
    }
}

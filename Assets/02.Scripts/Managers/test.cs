using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject testObj;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UIManager.instance.Move(testObj, new Vector3(5f, 2f, 0f), 3f, DG.Tweening.Ease.Linear);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            UIManager.instance.Shake(testObj, 3f);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            UIManager.instance.ReMove(testObj);
        }
    }
}

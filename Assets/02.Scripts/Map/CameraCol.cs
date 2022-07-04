using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCol : MonoBehaviour
{
    public GameObject cameraCol;
    public GameObject cameraCol2;
    public GameObject cameraCol3;
    public GameObject cameraCol4;
    private void OnTriggerEnter2D(Collider2D Yes)
    {
        Debug.Log("?");
        if (Yes.CompareTag("Player") && !Yes.isTrigger)
        {
            cameraCol.SetActive(true);
            cameraCol2.SetActive(true);
            cameraCol3.SetActive(true);
            cameraCol4.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D Yes)
    {
        Debug.Log("?");
        if (Yes.CompareTag("Player") && !Yes.isTrigger)
        {
            cameraCol.SetActive(false);
            cameraCol2.SetActive(false);
            cameraCol3.SetActive(false);
            cameraCol4.SetActive(false);
        }
    }
}

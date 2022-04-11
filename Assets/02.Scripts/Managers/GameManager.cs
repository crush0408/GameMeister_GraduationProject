using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject playerObj = null;

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
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CameraActionScript.ZoomIn(3f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            CameraActionScript.ZoomOut(3f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CameraActionScript.ShakeCam(4, 0.2f);
        }
    }
    public void GameOver()
    {
        Debug.LogError("PlayerDead");
    }
    
}

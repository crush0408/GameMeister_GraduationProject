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
    }
    
    public void GameOver()
    {
        Debug.LogError("PlayerDead");
    }
    
}

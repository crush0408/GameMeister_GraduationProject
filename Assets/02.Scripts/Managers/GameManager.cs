using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject playerObj = null;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
    public void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.GetComponent<PlayerHealth>().OnDead += GameOver;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("PlayScene");
    }
    
}

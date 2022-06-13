using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
}

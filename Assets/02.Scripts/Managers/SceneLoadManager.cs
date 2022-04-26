using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance;
    [SerializeField] int curSceneNum;   // 현재 씬

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        curSceneNum = 0;
    }


    public void NextScene() // 다음 씬으로 바로 이동
    {
        curSceneNum = SceneManager.GetActiveScene().buildIndex;
        curSceneNum++;
        SceneManager.LoadScene(curSceneNum);
    }

    public void LoadScene(string sceneName) // 입력된 이름의 씬으로 이동
    {
        SceneManager.LoadScene(sceneName);
    }
}

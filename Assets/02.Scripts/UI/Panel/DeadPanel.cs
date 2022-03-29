using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadPanel : MonoBehaviour
{
    public Text deadText;
    public Button replayBtn;
    public Button homeBtn;

    private void Awake()
    {
        replayBtn.onClick.AddListener(Replay);
        homeBtn.onClick.AddListener(Home);
    }

    private void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Home()
    {
        SceneManager.LoadScene(0);
    }
}

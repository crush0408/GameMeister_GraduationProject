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
        
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    public void Start()
    {
        playerObj.GetComponent<PlayerHealth>().OnDead += GameOver;

        // Time.timeScale = 1f;
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        PanelManager.instance.GameoverPanel.SetActive(true);
    }

    public void Main()
    {
        SceneManager.LoadScene(0);
    }

    public void AddCoin(int add)
    {
        PlayerStat.instance.Coin += add;
    }

    public void ChangeType(PlayerStat.PlayerType type)
    {
        PlayerStat.instance.MyType = type;

        switch (PlayerStat.instance.MyType)
        {
            case PlayerStat.PlayerType.NONE:
                PanelManager.instance.playerProfileBackground.color = Color.black;
                break;
            case PlayerStat.PlayerType.ICE:
                PanelManager.instance.playerProfileBackground.color = Color.blue;
                break;
            case PlayerStat.PlayerType.LIGHTNING:
                PanelManager.instance.playerProfileBackground.color = Color.yellow;
                break;
            case PlayerStat.PlayerType.WATER:
                PanelManager.instance.playerProfileBackground.color = Color.cyan;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject playerObj = null;

    // public bool isGetSustain = false;

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
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        PanelManager.instance.GameoverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Main()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1; 
    }

    public bool AddCoin(int add)
    {
        if(PlayerStat.instance.Coin + add >= 0)
        {
            PlayerStat.instance.Coin += add;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void AddStat(int hp, int atk, int def, int pass, int coin, float moveSpeed, float atkSpeed)
    {
        PlayerStat.instance.Coin += coin;

        PlayerStat.instance.HP = PlayerStat.instance.HP + hp > 200 ? 200 : PlayerStat.instance.HP + hp;
        PlayerStat.instance.Attack = PlayerStat.instance.Attack + atk > 300 ? 300 : PlayerStat.instance.Attack + atk;
        PlayerStat.instance.Defense = PlayerStat.instance.Defense + def > 150 ? 150 : PlayerStat.instance.Defense + def;
        PlayerStat.instance.Pass = PlayerStat.instance.Pass + pass > 100 ? 100 : PlayerStat.instance.Pass + pass;
        PlayerStat.instance.MoveSpeed =
            PlayerStat.instance.MoveSpeed + moveSpeed > 1.5f ? 1.5f : PlayerStat.instance.MoveSpeed + moveSpeed;
        PlayerStat.instance.AttackSpeed =
            PlayerStat.instance.AttackSpeed + atkSpeed > 6f ? 6f : PlayerStat.instance.AttackSpeed + atkSpeed;
    }

    public void TypeReward()
    {
        switch (PlayerStat.instance.MyType)
        {
            // 공격력 2, 공격 속도 0.02
            case PlayerStat.PlayerType.ICE:
                AddStat(0, 2, 0, 0, 0, 0, 0.02f);
                break;

            // 공격 속도 0.05
            case PlayerStat.PlayerType.LIGHTNING:
                AddStat(0, 0, 0, 0, 0, 0, 0.05f);
                break;

            // 공격력 5
            case PlayerStat.PlayerType.WATER:
                AddStat(0, 5, 0, 0, 0, 0, 0);
                break;
        }
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

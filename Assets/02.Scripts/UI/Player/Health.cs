using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // public Text healthText;
    [SerializeField] private Image healthBar;

    float health, maxHealth;
    float lerpSpeed;

    private void Start()
    {
        healthBar = GetComponent<Image>();
    }

    private void Update()
    {
        lerpSpeed = 3 * Time.time;

        Lerp();
    }

    public void InitHealth(float hp, float maxHp)
    {
        health = hp;
        maxHealth = maxHp;
    }

    public void SetHP(float hp)
    {
        health = hp;
    }

    private void Lerp()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
        healthBar.color =  Color.Lerp(Color.red, Color.green, (health / maxHealth));
        // healthText.text = "HP : " + health;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Text healthText;
    public Image healthBar;

    float health, maxHealth = 100f;
    float lerpSpeed;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        healthText.text = "Health : " + health + "%";
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Damage(23f);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(44f);
        }

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        ColorChanger();
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed); 
    }

    private void Damage(float damagePoints)
    {
        if(health > 0)
        {
            health -= damagePoints;
        }
    }

    private void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
    }

    private void Heal(float healingPoints)
    {
        if (health < maxHealth)
        {
            health += healingPoints;
        }
    }
}

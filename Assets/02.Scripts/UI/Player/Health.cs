using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
        Color healthColor;
    public Text healthText;
    public Image healthBar;

    float health, maxHealth;
    float lerpSpeed;

    IEnumerator _coroutine;

    private void Start()
    {

    }

    private void Update()
    {

        

        
    }
    public void InitHealth(float hp, float maxHp)
    {
        health = hp;
        maxHealth = maxHp;
    }
    public void SetHP(float hp)
    {
        health = hp;
        _coroutine = HpBarLerpCoroutine();
        StartCoroutine(_coroutine);
    }

 

    IEnumerator HpBarLerpCoroutine()
    {
        while (lerpSpeed <= 1)
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
             healthColor =  Color.Lerp(Color.red, Color.green, (health / maxHealth));
            lerpSpeed += 0.3f;
        }
        healthBar.color = healthColor;
        healthText.text = "HP : " + health;
        _coroutine = null;
        yield return null;
    }
}

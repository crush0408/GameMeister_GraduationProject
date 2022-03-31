using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotsUI : MonoBehaviour
{
    public List<Image> skillSlotImage;
    public List<Text> remainCoolTime;

    private float lerpSpeed;

    private PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();

        // UI È®ÀÎ¿ë
        for (int i = 0; i < skillSlotImage.Count; i++)
        {
            playerAttack.skillList[i].remainCoolTime = playerAttack.skillList[i].initCoolTime;
        }
    }

    private void Update()
    {
        lerpSpeed = 0.2f * Time.time;

        for (int i = 0; i < skillSlotImage.Count; i++)
        {
            skillSlotImage[i].fillAmount = playerAttack.skillList[i].remainCoolTime / playerAttack.skillList[i].initCoolTime;

            remainCoolTime[i].text = playerAttack.skillList[i].remainCoolTime.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotsUI : MonoBehaviour
{
    public List<Image> skillSlotImage;
    public List<Text> remainCoolTime;

    private PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();

        // UI 확인용
        for (int i = 0; i < skillSlotImage.Count; i++)
        {
            playerAttack.skillList[i].remainCoolTime = playerAttack.skillList[i].initCoolTime;
        }
    }

    private void Update()
    {
        for (int i = 0; i < skillSlotImage.Count; i++)
        {
            skillSlotImage[i].fillAmount = Mathf.Lerp(skillSlotImage[i].fillAmount,
               playerAttack.skillList[i].remainCoolTime / playerAttack.skillList[i].initCoolTime,
               Time.deltaTime);

            remainCoolTime[i].text = playerAttack.skillList[i].remainCoolTime > 0 ? playerAttack.skillList[i].remainCoolTime.ToString() : "";
        }
    }
}

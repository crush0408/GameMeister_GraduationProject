using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotsUI : MonoBehaviour
{
    public List<Image> skillSlotImage;

    private PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();
    }

    private void Update()
    {
        for (int i = 0; i < skillSlotImage.Count; i++)
        {
            skillSlotImage[i].fillAmount = playerAttack.skillList[i].remainCoolTime / playerAttack.skillList[i].initCoolTime;
        }
    }
}

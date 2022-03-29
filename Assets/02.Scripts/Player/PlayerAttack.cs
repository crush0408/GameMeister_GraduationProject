using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerInput playerInput;

    [Header("전체 스킬 리스트")]
    public List<SkillObject> skillList;

    [Header("입력 받은 스킬")]
    SkillObject inputSkill;

    public bool isAttacking = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerInput.skillOne)
        {
            InputSkillFunc(playerInput.skillOneName);
        }
        else if (playerInput.skillTwo)
        {
            InputSkillFunc(playerInput.skillTwoName);
        }
        else if (playerInput.ultimate)
        {
            InputSkillFunc(playerInput.ultimateName);
        }
    }
    private void InputSkillFunc(string name)
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            if(skillList[i].skillName == name)
            {
                inputSkill = skillList[i];
                if(inputSkill.coolTime <= 0f )//&& !isAttacking)
                {
                    Attack();
                }
                else
                {
                    Debug.Log("쿨타임 남음 ㅋ");
                }
                break;
            }
            else
            {
                Debug.LogError("해당 스킬을 찾을 수 없습니다.");
            }
        }
    }
    private void Attack()
    {
        isAttacking = true;
        anim.Play(inputSkill.skillName, -1, 0f);
    }
    public void AttackEnd()
    {
        isAttacking = false;
    }
}

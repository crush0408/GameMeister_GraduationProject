using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerInput playerInput;

    [Header("��ü ��ų ����Ʈ")]
    public List<SkillObject> skillList;

    [Header("�Է� ���� ��ų")]
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
                    Debug.Log("��Ÿ�� ���� ��");
                }
                break;
            }
            else
            {
                Debug.LogError("�ش� ��ų�� ã�� �� �����ϴ�.");
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

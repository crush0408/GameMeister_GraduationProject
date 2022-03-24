using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : PlayerInput
{
    [Header("��ü ��ų ����Ʈ")]
    public List<SkillObject> skillList;

    [Header("���� ���� ���� ��ų")]
    public SkillObject currentSkill;

    private Animator anim;

    // ��ų ����
    private int skillExp;
    private float attackDamage;

    [Header("ȿ���� ���ϸ�")]
    public string effectSoundName;

    private bool isAttacking = false;

    private void Start()
    {
        anim = GetComponent<Animator>();

        ChangeSkill(0);
        // �ϳ��� �� ��� ���� �� list�� ��ų ������Ʈ add�ϱ�
    }

    private void Update()
    {
        // �ӽù����...
        if (skillOne)
        {
            ChangeSkill(0);
            Attack();
        }
        else if (skillTwo)
        {
            ChangeSkill(1);
            Attack();
        }
        else if (ultimate)
        {
            ChangeSkill(2);
            Attack();
        }
    }

    private void ChangeSkill(int skillNum)
    {
        currentSkill = skillList[skillNum];
        Set();
    }

    private void Set()
    {
        skillExp = currentSkill.skillExp;
        attackDamage = currentSkill.attackDamage;

        effectSoundName = currentSkill.effectSoundName;
    }

    private void Attack()
    {
        isAttacking = true;

        // ����ġ �ֱ�

        // �ִϸ��̼� ���
        anim.Play(currentSkill.skillName, -1, 0f);
    }

    private void AfterAttack()
    {
        isAttacking = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : MonoBehaviour
{
    [Header("��ü ��ų ����Ʈ")]
    public List<SkillObject> skillList;

    [Header("���� ���� ���� ��ų")]
    public SkillObject currentSkill;

    private Animator anim;
    private PlayerInput _playerInput;

    // ��ų ����
    private float attackDamage;

    [Header("ȿ���� ���ϸ�")]
    public string effectSoundName;

    private bool isAttacking = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        ChangeSkill(0);
        // �ϳ��� �� ��� ���� �� list�� ��ų ������Ʈ add�ϱ�
    }

    private void Update()
    {
        // �ӽù����...
        if (_playerInput.skillOne)
        {
            ChangeSkill(0);
            Attack();
        }
        else if (_playerInput.skillTwo)
        {
            ChangeSkill(1);
            Attack();
        }
        else if (_playerInput.ultimate)
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

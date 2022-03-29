using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : MonoBehaviour
{
    [Header("전체 스킬 리스트")]
    public List<SkillObject> skillList;

    [Header("현재 장착 중인 스킬")]
    public SkillObject currentSkill;

    private Animator anim;
    private PlayerInput _playerInput;

    // 스킬 관련
    private float attackDamage;

    [Header("효과음 파일명")]
    public string effectSoundName;

    private bool isAttacking = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        ChangeSkill(0);
        // 하나씩 얻어갈 경우 열릴 때 list에 스킬 오브젝트 add하기
    }

    private void Update()
    {
        // 임시방편용...
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

        // 경험치 넣기

        // 애니메이션 재생
        anim.Play(currentSkill.skillName, -1, 0f);
    }

    private void AfterAttack()
    {
        isAttacking = false;
    }
}

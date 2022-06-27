using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private RePlayerInput playerInput;

    private PlayerMove playerMove;

    [Header("전체 스킬 리스트")]
    public List<SkillObject> skillList;

    [Header("입력 받은 스킬")]
    public SkillObject inputSkill;
    [SerializeField]

    [Header("VisualTransform관련")]
    public GameObject visualGroup;
    [SerializeField]
    private Vector2 boxSize = Vector2.zero;
    [SerializeField]
    private Transform onePos;
    [SerializeField]
    private Transform spinAttackTrm;
    [SerializeField]
    private Transform sustainAttackTrm;

    public bool isAttacking = false;
    private int combo = 0;

    public bool canJumpAttack = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerInput = GetComponent<RePlayerInput>();

        playerMove = GetComponent<PlayerMove>();

        for (int i = 0; i < skillList.Count; i++)
        {
            StartCoroutine(skillList[i].coolTime());
        }
    }

    void Update()
    {
        // Debug.Log(GetComponent<PlayerMove>().getHit);
        //Debug.Log(skillList[0].remainCoolTime + " " + skillList[1].remainCoolTime + " " + skillList[2].remainCoolTime);
        if (GetComponent<PlayerMove>().getHit == false)
        {

            if (playerInput.basicAtk)
            {
                if (!isAttacking)
                {
                    combo = 0;
                    anim.SetInteger("BasicAttack", combo);
                    anim.SetTrigger("isBagicAttack");
                    isAttacking = true;
                    combo++;
                }
                else if (isAttacking)
                {
                    switch (combo)
                    {
                        case 1:
                            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack1"))
                            {
                                anim.SetInteger("BasicAttack", combo);
                                combo++;
                            }
                            break;
                        case 2:
                            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack2"))
                            {
                                anim.SetInteger("BasicAttack", combo);
                            }
                            break;
                    }
                }

                if (canJumpAttack)
                {
                    canJumpAttack = false;
                    anim.SetBool("JumpAttack", !canJumpAttack);
                }
            }

            else if (playerInput.skillOne)
            {
                InputSkillFunc("FastMagic");
            }
            else if (playerInput.skillTwo)
            {
                InputSkillFunc("SpinAttack");
            }
            else if (playerInput.ultimate)
            {
                InputSkillFunc("SustainMagic");
            }
        }
    }
    private void InputSkillFunc(string name)
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            if (skillList[i].skillName == name)
            {
                inputSkill = skillList[i];

                if (inputSkill.remainCoolTime <= 0f && !isAttacking)
                {
                    if (inputSkill.skillName == "FastMagic")
                    {
                        Collider2D[] cols = Physics2D.OverlapBoxAll(onePos.position, boxSize, 0f);
                        MGSound.instance.playEff("FastTry");
                        foreach (Collider2D col in cols)
                        {
                            IDamageable target = col.GetComponent<IDamageable>();

                            if (target != null && col.gameObject.CompareTag("Enemy"))
                            {
                                //target.OnDamage(inputSkill.attackDamage, this.transform.position);
                                Attack("FastMagic", col.transform, i, "FastHit");
                                MGSound.instance.playEff("FastAttack");
                                break; // 하나만 타겟팅
                            }
                        }
                    }
                    else if (inputSkill.skillName == "SpinAttack")
                    {
                        Attack("SpinAttack", spinAttackTrm, i, "SpinHit");
                        MGSound.instance.playEff("SpinTry");

                    }
                    else if (inputSkill.skillName == "SustainMagic")
                    {
                        Attack("SustainMagic", sustainAttackTrm, i, "SustainHit");
                        MGSound.instance.playEff("SustainTry");
                    }
                    else
                    {
                        Debug.LogError("해당 스킬을 찾을 수 없습니다.");
                    }
                }
                else
                {
                    Debug.Log("쿨타임 남음");
                }
                break;
            }
            else
            {
                Debug.LogError("해당 스킬을 찾을 수 없습니다.");
            }
        }
    }
    private void Attack(string skillName, Transform skillTrm, int index, string hitEffectName)
    {

        isAttacking = true;
        anim.Play(inputSkill.skillName, -1, 0f);
        PoolableMono poolingObject = PoolManager.Instance.Pop(skillName);
        poolingObject.transform.localScale = visualGroup.transform.localScale;
        poolingObject.transform.position = skillTrm.position;
        poolingObject.GetComponent<NonTargetSkill>().damage = skillList[index].attackDamage;    // 어택 데미지 계산
        poolingObject.GetComponent<NonTargetSkill>().hitName = hitEffectName;
        skillList[index].remainCoolTime = skillList[index].initCoolTime;
        if (skillList[1])
        {
            MGSound.instance.playEff("SpinAttack");
        }
        else
        {
            MGSound.instance.playEff("SustainAttack");
        }
        StartCoroutine(skillList[index].coolTime());
    }

    public void AttackEnd()
    {
        isAttacking = false;
    }

    public void BasicAttackEnd(int value)
    {
        if (anim.GetInteger("BasicAttack") != value)
        {
            isAttacking = false;
        }
    }

    public void JumpAttackEnd()
    {
        Debug.Log("점프 어택 끝남");
        canJumpAttack = true;
        anim.SetBool("JumpAttack", !canJumpAttack);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(onePos.position, boxSize);

        }
    }
#endif
}
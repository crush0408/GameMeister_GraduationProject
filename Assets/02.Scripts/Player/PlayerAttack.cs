using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerInput playerInput;
    private SpriteRenderer sr;

    [Header("��ü ��ų ����Ʈ")]
    public List<SkillObject> skillList;

    [Header("�Է� ���� ��ų")]
    SkillObject inputSkill;

    [SerializeField]
    private Vector2 boxSize = Vector2.zero;
    [SerializeField]
    private Transform onePos;
    public Vector3 temp;

    public bool isAttacking = false;
    private int combo = 0;

    private SkillSlotsUI skillSlotsScript;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        sr = GetComponent<SpriteRenderer>();
        skillSlotsScript = GetComponentInChildren<SkillSlotsUI>();
        temp = onePos.localPosition;
        for (int i = 0; i < skillList.Count; i++)
        {
            StartCoroutine(skillList[i].coolTime());
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(skillList[0].remainCoolTime + " " + skillList[1].remainCoolTime + " " + skillList[2].remainCoolTime);
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
            else if(isAttacking)
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
                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack1"))
                        {

                            anim.SetInteger("BasicAttack", combo);
                            
                            
                        }
                        break;
                    

                    
                    
                }
            }
        }
        
        else if (playerInput.skillOne)
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

                if(inputSkill.remainCoolTime <= 0f )//&& !isAttacking)
                {
                    if(inputSkill.skillName == "FastMagic")
                    {
                        if (sr.flipX)
                        {
                            onePos.localPosition = new Vector3(-temp.x, temp.y, temp.z);
                        }
                        else
                        {
                            onePos.localPosition = temp;
                        }

                        Collider2D[] cols = Physics2D.OverlapBoxAll(onePos.position, boxSize ,0f);
                        foreach (Collider2D col in cols)
                        {
                            IDamageable target = col.GetComponent<IDamageable>();

                            if (target != null && col.gameObject.CompareTag("Enemy"))
                            {
                                Attack();
                                target.OnDamage(inputSkill.attackDamage, this.transform.position);
                                PoolableMono poolingObject = PoolManager.Instance.Pop("FastMagic");
                                poolingObject.transform.position = col.transform.position;
                                
                                skillList[i].remainCoolTime = skillList[i].initCoolTime;
                                StartCoroutine(skillList[i].coolTime());
                            }
                        }
                    }
                    else
                    {
                        Attack();   
                    }
                }
                else
                {
                    Debug.Log("��Ÿ�� ����");
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
    
    public void BasicAttackEnd()
    {
        
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

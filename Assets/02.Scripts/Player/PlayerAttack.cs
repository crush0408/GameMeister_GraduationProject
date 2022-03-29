using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerInput playerInput;
    private SpriteRenderer sr;

    [Header("전체 스킬 리스트")]
    public List<SkillObject> skillList;

    [Header("입력 받은 스킬")]
    SkillObject inputSkill;

    [SerializeField]
    private Vector2 boxSize = Vector2.zero;
    [SerializeField]
    private Transform onePos;
    public Vector3 temp;

    public bool isAttacking = false;
    private float combo = 0f;
    void Start()
    {
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        sr = GetComponent<SpriteRenderer>();
        temp = onePos.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!isAttacking && playerInput.basicAtk)
        {
            anim.SetBool("basicAtk", true);
            combo++;
        }
        else if (isAttacking && playerInput.basicAtk)
        {

        }
        */
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
                                target.OnDamage(inputSkill.attackDamage, col.transform.position);
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
    public void BasicAttackEnd()
    {
        combo = 0;
        isAttacking = false;
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

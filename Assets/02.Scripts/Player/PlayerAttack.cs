using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerInput playerInput;

    [Header("��ü ��ų ����Ʈ")]
    public List<SkillObject> skillList = new List<SkillObject>();

    [Header("�Է� ���� ��ų")]
    SkillObject inputSkill = new SkillObject();

    private bool isAttacking = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

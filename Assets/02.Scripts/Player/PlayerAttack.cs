using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerInput playerInput;

    [Header("전체 스킬 리스트")]
    public List<SkillObject> skillList = new List<SkillObject>();

    [Header("입력 받은 스킬")]
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

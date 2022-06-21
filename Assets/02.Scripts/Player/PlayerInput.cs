using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string movementName = "Horizontal";
    public string jumpBtnName = "Jump";
    public string basicAtkName = "BasicAttack";
    public string dashName = "Dash";
    public string skillOneName = "SkillOne";
    public string skillTwoName = "SkillTwo";
    public string ultimateName = "Ultimate";

    public float movement { get; private set; }
    public bool jump { get; private set; }
    public bool jumpKeyUp { get; private set; }
    public bool basicAtk { get; private set; }
    public bool dash { get; private set; }
    public bool skillOne { get; private set; }
    public bool skillTwo { get; private set; }
    public bool ultimate { get; private set; }

    private void Update()
    {
        movement = Input.GetAxisRaw(movementName);
        jump = Input.GetButtonDown(jumpBtnName);
        jumpKeyUp = Input.GetButtonUp(jumpBtnName);
        basicAtk = Input.GetButtonDown(basicAtkName);
        dash = Input.GetButtonDown(dashName);
        skillOne = Input.GetButtonDown(skillOneName);
        skillTwo = Input.GetButtonDown(skillTwoName);
        ultimate = Input.GetButtonDown(ultimateName);
    }
}

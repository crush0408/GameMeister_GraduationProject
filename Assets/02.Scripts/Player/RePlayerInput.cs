using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePlayerInput : MonoBehaviour
{
    public bool movementLeft { get; private set; }
    public bool movementRight { get; private set; }
    public bool jump { get; private set; }
    public bool jumpKeyUp { get; private set; }
    public bool basicAtk { get; private set; }
    public bool dash { get; private set; }
    public bool skillOne { get; private set; }
    public bool skillTwo { get; private set; }
    public bool ultimate { get; private set; }

    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Z, KeyCode.C, KeyCode.X, KeyCode.A, KeyCode.S, KeyCode.Q, KeyCode.E, KeyCode.Escape };
    private void Start()
    {

        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
            Debug.Log(System.Enum.GetName(typeof(KeyAction), i));
        }
    }

    private void Update()
    {
        movementLeft = Input.GetKeyDown(KeySetting.keys[KeyAction.LeftMove]);
        movementRight = Input.GetKeyDown(KeySetting.keys[KeyAction.RightMove]);
        jump = Input.GetKeyDown(KeySetting.keys[KeyAction.Jump]);
        jumpKeyUp = Input.GetKeyDown(KeySetting.keys[KeyAction.Jump]);
        dash = Input.GetKeyDown(KeySetting.keys[KeyAction.Dash]);
        basicAtk = Input.GetKeyDown(KeySetting.keys[KeyAction.BasicAttack]);
        skillOne = Input.GetKeyDown(KeySetting.keys[KeyAction.FastMagicAttackSkill]);
        skillTwo = Input.GetKeyDown(KeySetting.keys[KeyAction.SpinAttackSkill]);
        ultimate = Input.GetKeyDown(KeySetting.keys[KeyAction.SustatinAttackSkill]);

    }
}

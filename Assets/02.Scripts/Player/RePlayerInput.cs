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
    public bool interaction { get; private set; }
    public bool openSetting { get; private set; }

    private void Update()
    {
        movementLeft = Input.GetKey(KeySetting.KeySettingDict[KeyInputType.LeftMove]);
        movementRight = Input.GetKey(KeySetting.KeySettingDict[KeyInputType.RightMove]);
        jump = Input.GetKeyDown(KeySetting.KeySettingDict[KeyInputType.Jump]);
        jumpKeyUp = Input.GetKeyDown(KeySetting.KeySettingDict[KeyInputType.Jump]);
        dash = Input.GetKeyDown(KeySetting.KeySettingDict[KeyInputType.Dash]);
        basicAtk = Input.GetKeyDown(KeySetting.KeySettingDict[KeyInputType.BasicAttack]);
        skillOne = Input.GetKeyDown(KeySetting.KeySettingDict[KeyInputType.FastMagicAttackSkill]);
        skillTwo = Input.GetKeyDown(KeySetting.KeySettingDict[KeyInputType.SpinAttackSkill]);
        ultimate = Input.GetKeyDown(KeySetting.KeySettingDict[KeyInputType.SustatinAttackSkill]);
        interaction = Input.GetKeyDown(KeySetting.KeySettingDict[KeyInputType.Interaction]);
        openSetting = Input.GetKeyDown(KeySetting.KeySettingDict[KeyInputType.OpenMenu]);
    }
}

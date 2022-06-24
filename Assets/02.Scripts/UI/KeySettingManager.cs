using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum KeyAction
{
    LeftMove,
    RightMove,
    Dash,
    Jump,
    BasicAttack,
    FastMagicAttackSkill,
    SpinAttackSkill,
    SustatinAttackSkill,
    Interaction,
    OpenMenu,
    KEYCOUNT
}

public static class KeySetting
{
    public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();
}

public class KeySettingManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Z, KeyCode.C, KeyCode.X, KeyCode.A, KeyCode.S, KeyCode.Q, KeyCode.E, KeyCode.Escape };

    public Text[] text;
    private void Awake()
    {
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }

        for (int j = 0; j < text.Length; j++)
        {
            text[j].text = KeySetting.keys[(KeyAction)j].ToString();
        }
    }

    private void Update()
    {
        for (int j = 0; j < text.Length; j++)
        {
            text[j].text = KeySetting.keys[(KeyAction)j].ToString();
        }
    }

    private void OnGUI()
    {
        Event keyEvent = Event.current;

        if (KeySetting.keys.ContainsValue(keyEvent.keyCode))
        {
            return;
            for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
            {
                KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
            }
        }

        if (keyEvent.isKey)
        {
            KeySetting.keys[(KeyAction)key] = keyEvent.keyCode;
            key = -1;
        }

    }

    int key = -1;
    public void ChangeKey(int num)
    {
        key = num;
    }
}

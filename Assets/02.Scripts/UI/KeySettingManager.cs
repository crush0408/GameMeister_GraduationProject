using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

public class KeySettingManager : MonoBehaviour, IPointerUpHandler
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Z, KeyCode.C, KeyCode.X, KeyCode.A, KeyCode.S, KeyCode.Q, KeyCode.E, KeyCode.Escape };

    public Text[] text;
    public Button[]  btn;
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

        

        if(keyEvent.isKey)
        {
            KeyCode c = KeySetting.keys[(KeyAction)key];
            Debug.Log(c);
            KeySetting.keys[(KeyAction)key] = keyEvent.keyCode;
            key = -1;
            ChangebtnColor();
            if(KeySetting.keys.ContainsValue(keyEvent.keyCode))
            {
                KeyCode a = keyEvent.keyCode;
                KeySetting.keys[(KeyAction)key] = a;
                for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
                {
                    
                }
            }
        }

    }

    int key = -1;
    public void ChangeKey(int num)
    {
        key = num;
        for (int i = 0; i < 10; i++)
        {
            btn[key].GetComponent<Image>().color = Color.red;
        }

        Debug.Log(2);
    }

    public void ChangebtnColor()
    {
        for (int i = 0; i < 10; i++)
        {
            btn[i].GetComponent<Image>().color = Color.white;
            Debug.Log(3);
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(4);
    }
}

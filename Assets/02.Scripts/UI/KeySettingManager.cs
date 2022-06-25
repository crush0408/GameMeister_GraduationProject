using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum KeyAction //Ű ����
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

public static class KeySetting //Ű�� �޴� ��ųʸ�
{
    public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();
}

public class KeySettingManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Z, KeyCode.C, KeyCode.X, KeyCode.A, KeyCode.S, KeyCode.Q, KeyCode.E, KeyCode.Escape };

    public Text[] text; //Ű UI
    public Button[]  btn; //��ư��
    private void Awake()
    {
        //�⺻Ű�� �־��ݴϴ�
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }
        //�⺻Ű �ؽ�Ʈ�� �־��ݴϴ�

        for (int j = 0; j < text.Length; j++)
        {
            text[j].text = KeySetting.keys[(KeyAction)j].ToString();
        }
    }

    private void Update()
    {
        for (int j = 0; j < text.Length; j++)
        {
            //��¥ ���� Ű�� �ٲ������ �ٲٴ� �ڵ�
            text[j].text = KeySetting.keys[(KeyAction)j].ToString(); 
        }
    }

    private void OnGUI() //Update�� ����Ѱǵ� Event ������ ����ϴ� �Լ�
    {
        Event keyEvent = Event.current;//Ű���ޱ�

        

        if(keyEvent.isKey) //Ű�� �Է� �޾��� ��
        {
            KeySetting.keys[(KeyAction)key] = keyEvent.keyCode; //���� Ű ���� �־��ݴϴ�
            key = -1;
            ChangebtnColor(); //�� �ٲ�
            if (KeySetting.keys.ContainsValue(keyEvent.keyCode)) return; //���� ������ �ִ� Ű���� �޾Ҵٸ� ������ �ؼ� None���� ��ȯ�Ѵ�.
        }

    }

    int key = -1; //Ű�� ����
    public void ChangeKey(int num) //�ٲٷ��� �ϴ� Ŭ�� ����
    {
        key = num;
        for (int i = 0; i < 10; i++)
        {
            btn[key].GetComponent<Image>().color = Color.red;
        }

        Debug.Log(2);
    }

    public void ChangebtnColor() //�� ������� �ǵ�����
    {
        for (int i = 0; i < 10; i++)
        {
            btn[i].GetComponent<Image>().color = Color.white;
            Debug.Log(3);
        }

    }
}

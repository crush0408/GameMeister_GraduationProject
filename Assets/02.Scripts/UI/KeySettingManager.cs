using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum KeyAction //키 값들
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

public static class KeySetting //키를 받는 딕셔너리
{
    public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();
}

public class KeySettingManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Z, KeyCode.C, KeyCode.X, KeyCode.A, KeyCode.S, KeyCode.Q, KeyCode.E, KeyCode.Escape };

    public Text[] text; //키 UI
    public Button[]  btn; //버튼들
    private void Awake()
    {
        //기본키를 넣어줍니다
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }
        //기본키 텍스트를 넣어줍니다

        for (int j = 0; j < text.Length; j++)
        {
            text[j].text = KeySetting.keys[(KeyAction)j].ToString();
        }
    }

    private void Update()
    {
        for (int j = 0; j < text.Length; j++)
        {
            //글짜 무슨 키로 바뀌었는지 바꾸는 코드
            text[j].text = KeySetting.keys[(KeyAction)j].ToString(); 
        }
    }

    private void OnGUI() //Update랑 비슷한건데 Event 쓰려면 써야하는 함수
    {
        Event keyEvent = Event.current;//키값받기

        

        if(keyEvent.isKey) //키를 입력 받았을 때
        {
            KeySetting.keys[(KeyAction)key] = keyEvent.keyCode; //받은 키 값을 넣어줍니다
            key = -1;
            ChangebtnColor(); //색 바꿈
            if (KeySetting.keys.ContainsValue(keyEvent.keyCode)) return; //만약 가지고 있는 키값을 받았다면 리턴을 해서 None값을 반환한다.
        }

    }

    int key = -1; //키의 숫자
    public void ChangeKey(int num) //바꾸려고 하는 클릭 감지
    {
        key = num;
        for (int i = 0; i < 10; i++)
        {
            btn[key].GetComponent<Image>().color = Color.red;
        }

        Debug.Log(2);
    }

    public void ChangebtnColor() //색 원래대로 되돌리기
    {
        for (int i = 0; i < 10; i++)
        {
            btn[i].GetComponent<Image>().color = Color.white;
            Debug.Log(3);
        }

    }
}

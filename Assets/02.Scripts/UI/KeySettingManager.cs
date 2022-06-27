using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json.Linq;

public enum KeyInputType //키 값들
{
    LeftMove,
    BasicAttack,
    RightMove,
    FastMagicAttackSkill,
    Jump,
    SpinAttackSkill,
    Dash,
    SustatinAttackSkill,
    Interaction,
    OpenMenu,
    KEYCOUNT
}

public static class KeySetting //키를 받는 딕셔너리
{
    public static Dictionary<KeyInputType, KeyCode> KeySettingDict = new Dictionary<KeyInputType, KeyCode>();
}

public class KeySettingManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.LeftArrow, KeyCode.X, KeyCode.RightArrow, KeyCode.A, KeyCode.C, KeyCode.S, KeyCode.Z, KeyCode.Q, KeyCode.E, KeyCode.Escape };

    public Text[] text; //키 UI
    public Button[] btn; //버튼들
    const string saveFileName = "KeyData.sav";
    public GameObject panel;

    int key = -1; //키의 숫자

    private void Awake()
    {
        //기본키를 넣어줍니다
        for (int i = 0; i < (int)KeyInputType.KEYCOUNT; i++)
        {
            KeySetting.KeySettingDict.Add((KeyInputType)i, defaultKeys[i]);
        }
        //기본키 텍스트를 넣어줍니다

        for (int j = 0; j < text.Length; j++)
        {
            text[j].text = KeySetting.KeySettingDict[(KeyInputType)j].ToString();
        }
    }

    private void Start()
    {
        Save();
        Load();
    }

    private void Update()
    {
        for (int j = 0; j < text.Length; j++)
        {
            //글짜 무슨 키로 바뀌었는지 바꾸는 코드
            text[j].text = KeySetting.KeySettingDict[(KeyInputType)j].ToString();
        }
    }

    private void OnGUI() //Update랑 비슷한건데 Event 쓰려면 써야하는 함수
    {
        Event keyEvent = Event.current;//키값받기

        if (keyEvent.isKey) //키를 입력 받았을 때
        {
            if (KeySetting.KeySettingDict.ContainsValue(keyEvent.keyCode))
            {
                KeyInputType temp = (KeyInputType)0;
                foreach (var item in KeySetting.KeySettingDict)
                {
                    if (item.Value == keyEvent.keyCode)
                    {
                        temp = item.Key; // 밸류가 겹치는 친구의 키를 저장
                    }
                }

                KeySetting.KeySettingDict[temp] = KeySetting.KeySettingDict[(KeyInputType)key]; // 밸류 겹치는 애의 밸류를 지금 변환하지 않은 밸류로 넣어줌
                KeySetting.KeySettingDict[(KeyInputType)key] = keyEvent.keyCode;

            }
            else
            {
                KeySetting.KeySettingDict[(KeyInputType)key] = keyEvent.keyCode;
            }

            key = -1;
            ChangebtnColor(); //색 바꿈

            foreach (KeyValuePair<KeyInputType, KeyCode> author in KeySetting.KeySettingDict)
            {
                if (keyEvent.keyCode == author.Value)
                {
                    //key = -1;
                    ChangebtnColor(); //색 바꿈
                    return;
                }
                else
                {
                    //받은 키 값을 넣어줍니다
                    return;
                }
            }
        }

    }

    public void ChangeKey(int num) //바꾸려고 하는 클릭 감지
    {
        key = num;

        btn[key].GetComponent<Image>().color = Color.red;
    }

    public void ChangebtnColor() //색 원래대로 되돌리기
    {
        for (int i = 0; i < 10; i++)
        {
            btn[i].GetComponent<Image>().color = Color.white;
        }

    }

    public void Reset()
    {
        KeySetting.KeySettingDict.Clear();
        //기본키를 넣어줍니다
        for (int i = 0; i < (int)KeyInputType.KEYCOUNT; i++)
        {
            KeySetting.KeySettingDict.Add((KeyInputType)i, defaultKeys[i]);
        }
        //기본키 텍스트를 넣어줍니다

        for (int j = 0; j < text.Length; j++)
        {
            text[j].text = KeySetting.KeySettingDict[(KeyInputType)j].ToString();
        }
        ChangebtnColor();
        Debug.Log("reset");
    }

    public void Close()
    {
        Save();
        panel.SetActive(false);
    }

    string getFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    public void Save()
    {
        JArray jObj = new JArray();
        jObj.Add(KeySetting.KeySettingDict.Values);

        print(jObj.ToString());

        StreamWriter sw = new StreamWriter(getFilePath(saveFileName));
        sw.WriteLine(jObj);
        sw.Close();
    }

    void Load()
    {
        print("Load to :" + getFilePath(saveFileName));
        StreamReader sr = new StreamReader(getFilePath(saveFileName));

        string str = sr.ReadToEnd();

        sr.Close();
    }
}

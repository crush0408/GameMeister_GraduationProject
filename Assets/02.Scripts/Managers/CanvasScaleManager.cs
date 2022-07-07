using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScaleManager : MonoBehaviour
{
    public static CanvasScaleManager instance;
    public Dropdown dropdown;
    public GameObject a;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        a = GameObject.Find("SettingPanel");
    }

    private void Start()
    {
        DropdownSet();
        a.SetActive(false);
        /*dropdown.onValueChanged.AddListener(delegate
        {
            myDropdownValueChangedHandler(dropdown);
        });*/
    }

    void DropdownSet()
    {
        dropdown.value = DataManager.instance.isWindow;
        SetScreen();
        //Debug.Log("드롭다운 값 : " + dropdown.value);
    }

    public void DropDownValueChange()
    {
        // 선택한 드롭다운 값 확인
        

        // 드롭다운 값 저장
        DataManager.instance.isWindow = (dropdown.value == 0) ? 0 : 1;
        DataManager.instance.ScreenSave();
        Debug.Log("saved : " + DataManager.instance.isWindow);

        SetScreen();
        
        Debug.Log("사이즈 : " + Screen.width + " * " + Screen.height);
    }

    private void SetScreen()
    {
        if(dropdown.value == 0)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else
        {
            Screen.SetResolution(1600, 900, false);
        }
    }

    public Vector2 ReturnResolution()
    {
        return DataManager.instance.isWindow == 0 ? new Vector2(1920, 1080) : new Vector2(1600, 900);
    }
}

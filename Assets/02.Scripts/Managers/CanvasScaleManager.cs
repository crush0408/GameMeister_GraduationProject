using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScaleManager : MonoBehaviour
{
    public static CanvasScaleManager instance;
    public Dropdown dropdown;

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
    }

    void Start()
    {
        dropdown.value = DataManager.instance.isWindow; // 가져오기
        SetScreen();

        dropdown.onValueChanged.AddListener(delegate { SetScreenMode(); });
    }

    public void SetScreenMode()
    {
        DataManager.instance.isWindow = dropdown.value; // 드롭다운 값 넣기
        Debug.Log(DataManager.instance.isWindow);
        DataManager.instance.ScreenSave();
        SetScreen();
        
    }

    public void SetScreen()
    {
        if(DataManager.instance.isWindow == 0)
        {    
            Screen.SetResolution(1920,1080, true);
        }
        else
        {
            Screen.SetResolution(1600,900,false);
        }
    }

    public Vector2 ReturnResolution()
    {
        if (DataManager.instance.isWindow == 0)
        {
            return new Vector2(1920, 1080);
        }
        else
        {
            return new Vector2(1600, 900);
        }
    }
}

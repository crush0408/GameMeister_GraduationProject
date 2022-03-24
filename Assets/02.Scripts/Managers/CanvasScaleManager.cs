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
        dropdown.value = DataManager.instance.isWindow;
        SetScreen();
    }

    public void SetScreenMode()
    {
        DataManager.instance.isWindow = dropdown.value;
        Debug.Log(DataManager.instance.isWindow);
        DataManager.instance.Save();
        SetScreen();
    }

    public Vector2 SetScreen()
    {
        if(DataManager.instance.isWindow == 0){
            
            Screen.SetResolution(1920,1080,true);
            
            return new Vector2(1920,1080);
        }
        else{
            Screen.SetResolution(1600,900,false);
            return new Vector2(1600,900);
        }
    }
}

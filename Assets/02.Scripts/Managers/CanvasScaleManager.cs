using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScaleManager : MonoBehaviour
{
    public static CanvasScaleManager instance;
    public Dropdown dropdown;

    [Header("캔버스 스케일러")]
    public CanvasScaler canvasScaler;

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
    }

    public void SetScreenMode()
    {
        DataManager.instance.isWindow = dropdown.value; // 드롭다운 값 넣기
        Debug.Log(DataManager.instance.isWindow);
        DataManager.instance.ScreenSave();
        SetScreen();
    }

    public Vector2 SetScreen()
    {
        if(DataManager.instance.isWindow == 0)
        {    
            Screen.SetResolution(1920,1080, true);

            return new Vector2(1920, 1080);
            // canvasScaler.referenceResolution = new Vector2(1920,1080);
        }
        else
        {
            Screen.SetResolution(1600,900,false);

            return new Vector2(1600, 900);
            // canvasScaler.referenceResolution =  new Vector2(1600,900);
        }
    }
}

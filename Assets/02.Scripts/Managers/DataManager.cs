using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public float BGMmasterSoundVolume;  // MGSound에서 참조
    public float EFFmasterSoundVolume;  // MGSound에서 참조
    public int isWindow;                // 0(false)이면 FullScreen, 1(true)이면 Window
    public int isFirst; // 첫 플레이인지 (컷씬 재생에 필요)

    private void Awake()
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

        Init();
    }

    private void Start()
    {
        MGSound.instance.BGMVol();
    }

    private void Init() // 초기화(초기 설정)
    {
        if (!PlayerPrefs.HasKey("BGM")) PlayerPrefs.SetFloat("BGM", 0.5f);
        BGMmasterSoundVolume = PlayerPrefs.GetFloat("BGM");

        if (!PlayerPrefs.HasKey("EFF")) PlayerPrefs.SetFloat("EFF", 0.5f);
        EFFmasterSoundVolume = PlayerPrefs.GetFloat("EFF");

        if (!PlayerPrefs.HasKey("Display")) PlayerPrefs.SetInt("Display", 0);
        isWindow = PlayerPrefs.GetInt("Display");

        // 컷씬용 초기화
        if (!PlayerPrefs.HasKey("IsFirst")) PlayerPrefs.SetInt("IsFirst", 1);
        isFirst = PlayerPrefs.GetInt("IsFirst");
    }

    public void ScreenSave()  // 저장
    {
        PlayerPrefs.SetInt("Display", isWindow);
    }
}

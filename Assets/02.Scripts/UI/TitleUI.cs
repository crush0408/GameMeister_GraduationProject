using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [Header("스타트 화면 버튼")]
    public Button startBtn;
    public Button settingsBtn;
    public Button quitBtn;

    [Header("설정 패널 버튼")]
    public Button closeBtn;

    public Dropdown dropdown;

    [Header("설정 패널")]
    public GameObject settingPanel;

    [Header("캔버스 스케일러")]
    public CanvasScaler canvasScaler;

    [Header("Slider")]
    public Slider bgmSlider;
    public Slider effSlider;

    [Header("시작 BGM")]
    public string bgmName;

    float bgmVol = 1f;
    float effVol = 1f;

    private void Start()
    {
        // 버튼에 OnClick 리스너 추가
        startBtn.onClick.AddListener(StartBtn);
        settingsBtn.onClick.AddListener(SettingsPanel);
        closeBtn.onClick.AddListener(SettingsPanel);
        quitBtn.onClick.AddListener(QuitBtn);

        // 슬라이더에 OnValueChanged 리스너 추가
        bgmSlider.onValueChanged.AddListener(BGMControl);
        effSlider.onValueChanged.AddListener(EFFControl);

        dropdown.onValueChanged.AddListener(delegate { SetScreen(); });

        bgmVol = DataManager.instance.BGMmasterSoundVolume;
        effVol = DataManager.instance.EFFmasterSoundVolume;

        bgmSlider.value = bgmVol;
        effSlider.value = effVol;

        MGSound.instance.playBgm(bgmName);

        settingPanel.SetActive(false);
    }

    // 시작화면 버튼
    public void StartBtn()
    {
        // SceneLoadManager.instance.NextScene();  // 다음 씬으로 연결
        Debug.Log("현재 씬 넘버 : " + SceneLoadManager.instance.curSceneNum);
    }

    public void SettingsPanel()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
        Debug.Log("현재 설정 패널 상태 : " + settingPanel.activeSelf);
    }

    public void QuitBtn()
    {
        Application.Quit();
        Debug.Log("isPlaying : " + Application.isPlaying);
    }

    // 설정 패널
    public void BGMControl(float bgmValue)
    {
        bgmVol = bgmValue;
        DataManager.instance.BGMmasterSoundVolume = bgmVol;
        MGSound.instance.BGMVol();  // 중간 볼륨 조절

        PlayerPrefs.SetFloat("BGM", bgmVol);
    }

    public void EFFControl(float effValue)
    {
        effVol = effValue;
        DataManager.instance.EFFmasterSoundVolume = effVol;

        PlayerPrefs.SetFloat("EFF", effVol);
    }

    // UI 크기 맞춤 // 참조 없음 ????????????
    public void SetScreen()
    {
        canvasScaler.referenceResolution = CanvasScaleManager.instance.ReturnResolution();
    }

    

}

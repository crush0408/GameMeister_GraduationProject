using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public SaveData(int _hp, int _atk, int _def)
    {
        hp = _hp;
        atk = _atk;
        def = _def;
    }

    public int hp;
    public int atk;
    public int def;
}

public static class SaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/GameData/";

    public static void Save(SaveData saveData, string saveFileName)
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string saveJson = JsonUtility.ToJson(saveData);

        string saveFilePath = SavePath + saveFileName + ".json";
        File.WriteAllText(saveFilePath, saveJson);
        Debug.Log("저장 경로 : " + saveFilePath);
    }

    public static SaveData Load(string saveFileName)
    {
        string saveFilePath = SavePath + saveFileName + ".json";

        if (!File.Exists(saveFilePath))
        {
            Debug.LogError("파일 경로가 잘못되었습니다.");
            return null;
        }

        string saveFile = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(saveFile);
        return saveData;
    }
}


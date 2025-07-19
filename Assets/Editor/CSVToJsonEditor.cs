using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class CSVToJsonEditor : EditorWindow
{
    private string csvPath = "C:/Users/김용현/OneDrive/바탕 화면/유니티/2D Log 2021/CSV/character_data.csv";
    private string jsonPath = "C:/Users/김용현/OneDrive/바탕 화면/유니티/2D Log 2021/Assets/Resources/Data/Character/character_data.json";

    [MenuItem("Tools/CSV TO JSON")]
    public static void ShowWindow()
    {
        GetWindow<CSVToJsonEditor>("CSV to JSON Converter");
    }

    void OnGUI()
    {
        GUILayout.Label("CSV → JSON 변환기", EditorStyles.boldLabel);

        GUILayout.Space(10);
        csvPath = EditorGUILayout.TextField("CSV 파일 경로", csvPath);
        jsonPath = EditorGUILayout.TextField("JSON 저장 경로", jsonPath);

        GUILayout.Space(10);
        if (GUILayout.Button("변환 실행"))
        {
            ConvertCSVToJson(csvPath, jsonPath);
        }
    }

    void ConvertCSVToJson(string csvFilePath, string jsonFilePath)
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV 파일이 존재하지 않습니다: " + csvFilePath);
            return;
        }

        var lines = File.ReadAllLines(csvFilePath);
        if (lines.Length < 2)
        {
            Debug.LogWarning("CSV에 데이터가 부족합니다.");
            return;
        }

        string[] headers = lines[0].Split(',');
        var dataList = new List<Dictionary<string, string>>();

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');
            var entry = new Dictionary<string, string>();

            for (int j = 0; j < headers.Length && j < values.Length; j++)
            {
                entry[headers[j]] = values[j];
            }

            dataList.Add(entry);
        }

        string json = JsonConvert.SerializeObject(dataList, Formatting.Indented);
        File.WriteAllText(jsonFilePath, json, System.Text.Encoding.UTF8);

        Debug.Log($"CSV → JSON 변환 완료!\nJSON 저장 위치: {jsonFilePath}");
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }
}

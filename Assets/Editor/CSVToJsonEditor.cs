using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class CSVToJsonEditor : EditorWindow
{
    private string csvPath = "C:/Users/�����/OneDrive/���� ȭ��/����Ƽ/2D Log 2021/CSV/character_data.csv";
    private string jsonPath = "C:/Users/�����/OneDrive/���� ȭ��/����Ƽ/2D Log 2021/Assets/Resources/Data/Character/character_data.json";

    [MenuItem("Tools/CSV TO JSON")]
    public static void ShowWindow()
    {
        GetWindow<CSVToJsonEditor>("CSV to JSON Converter");
    }

    void OnGUI()
    {
        GUILayout.Label("CSV �� JSON ��ȯ��", EditorStyles.boldLabel);

        GUILayout.Space(10);
        csvPath = EditorGUILayout.TextField("CSV ���� ���", csvPath);
        jsonPath = EditorGUILayout.TextField("JSON ���� ���", jsonPath);

        GUILayout.Space(10);
        if (GUILayout.Button("��ȯ ����"))
        {
            ConvertCSVToJson(csvPath, jsonPath);
        }
    }

    void ConvertCSVToJson(string csvFilePath, string jsonFilePath)
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV ������ �������� �ʽ��ϴ�: " + csvFilePath);
            return;
        }

        var lines = File.ReadAllLines(csvFilePath);
        if (lines.Length < 2)
        {
            Debug.LogWarning("CSV�� �����Ͱ� �����մϴ�.");
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

        Debug.Log($"CSV �� JSON ��ȯ �Ϸ�!\nJSON ���� ��ġ: {jsonFilePath}");
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class MapTest : MonoBehaviour
{
    private void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10, 10, 200, 200), "Map Test");

        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if (GUI.Button(new Rect(20, 20, 180, 180), "Map Test"))
        {
            // JSON ���� ���
            string jsonFilePath = $"Assets/Resources/Map/Base2/Map_001.json";

            // JSON ���� �б�
            string jsonContent = File.ReadAllText(jsonFilePath);

            // JSON�� ��ü�� ��ȯ
            MapData mapData = JsonConvert.DeserializeObject<MapData>(jsonContent);

            // ����Ͽ� Ȯ��
            Debug.Log($"Map Name: {mapData.name}");
            Debug.Log($"Bounds: xMin={mapData.bounds.xMin}, xMax={mapData.bounds.xMax}, yMin={mapData.bounds.yMin}, yMax={mapData.bounds.yMax}");
            Debug.Log("Monster List:");
            foreach (var monster in mapData.monsterList)
            {
                Debug.Log(monster);
            }

            
            // �ؽ�Ʈ ���� ���
            string textFilePath = $"Assets/Resources/Map/Base2/Map_001.txt";

            // �ؽ�Ʈ ���� �б�
            string[] lines = File.ReadAllLines(textFilePath);

            // xMin, xMax, yMin, yMax ���� ����
            string[] boundsLine = lines[0].Split(':');
            int xMin = int.Parse(boundsLine[1].Trim());
            int xMax = int.Parse(lines[1].Split(':')[1].Trim());
            int yMin = int.Parse(lines[2].Split(':')[1].Trim());
            int yMax = int.Parse(lines[3].Split(':')[1].Trim());

            // CollisionTiles �б�
            List<string> collisionTiles = new List<string>();
            int i = 0;
            while (i < lines.Length && lines[i] != "itemTiles")
            {
                collisionTiles.Add(lines[i]);
                i++;
            }

            // itemTiles �б�
            List<string> itemTiles = new List<string>();
            while (i < lines.Length && lines[i] != "monsterTiles")
            {
                itemTiles.Add(lines[i]);
                i++;
            }

            // monsterTiles �б�
            List<string> monsterTiles = new List<string>();
            while (i < lines.Length)
            {
                monsterTiles.Add(lines[i]);
                i++;
            }

            // ����Ͽ� Ȯ��
            Debug.Log($"Bounds: xMin={xMin}, xMax={xMax}, yMin={yMin}, yMax={yMax}");
            Debug.Log("Collision Tiles:");
            foreach (var tile in collisionTiles)
            {
                Debug.Log(tile);
            }
            Debug.Log("Item Tiles:");
            foreach (var tile in itemTiles)
            {
                Debug.Log(tile);
            }
            Debug.Log("Monster Tiles:");
            foreach (var tile in monsterTiles)
            {
                Debug.Log(tile);
            }
        }
    }

    public Grid CurrentGrid { get; private set; }

    public void LoadMap()
    {
        GameObject go = GameManager.Resouce.Instantiate($"Map/Base2/Map_001");
        go.name = "Map";

        GameObject collision = Util.FindChild(go, "CollisionMap", true);
        if (collision != null)
            collision.SetActive(false);

        CurrentGrid = go.GetComponent<Grid>();

        // Collision ���� ����
        TextAsset txt = GameManager.Resouce.Load<TextAsset>($"Map/Animal/Assets/Resources/Map/Base2/Map_001.txt");//���Ϸε�
        StringReader reader = new StringReader(txt.text); //�ε����� ��Ʈ������ ��ȯ


        ////��ȯ�Ѱ��� ����
        //MinX = int.Parse(reader.ReadLine());
        //MaxX = int.Parse(reader.ReadLine());
        //MinY = int.Parse(reader.ReadLine());
        //MaxY = int.Parse(reader.ReadLine());

        //int xCount = MaxX - MinX + 1;
        //int yCount = MaxY - MinY + 1;
        //_collision = new bool[yCount, xCount];

        //for (int y = 0; y < yCount; y++)
        //{
        //    string line = reader.ReadLine();
        //    for (int x = 0; x < xCount; x++)
        //    {
        //        if (line[x] == '1')
        //            _collision[y, x] = true;
        //        else if (line[x] == '0')
        //        {
        //            _collision[y, x] = false;
        //            listY.Add(y);
        //            listX.Add(x);
        //        }
        //        else if (line[x] == '2')
        //        {
        //            _collision[y, x] = false;
        //            StairUpListY.Add(y);
        //            StairUpListX.Add(x);
        //        }

        //        else if (line[x] == '3')
        //            _collision[y, x] = false;
        //        else if (line[x] == '4')
        //            _collision[y, x] = false;
        //    }
        //}
    }

    public void DestroyMap()
    {
        GameObject map = GameObject.Find("Map");
        if (map != null)
        {
            GameObject.Destroy(map);
            CurrentGrid = null;
        }
    }
}

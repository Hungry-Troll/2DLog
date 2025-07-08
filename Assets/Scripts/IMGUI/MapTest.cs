using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using UnityEngine.Tilemaps;
using UnityEditor;

public class MapTest : MonoBehaviour
{
    bool showTextField = false;
    string userInput = "Map Path";
    private void OnGUI()
    {
        // Make a background box
        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.fontSize = 30;

        GUI.Box(new Rect(10, 10, 200, 480), "Map Test", boxStyle);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 30;

        if (GUI.Button(new Rect(20, 80, 180, 100), "Map Test", buttonStyle))
        {
            TestMapData();
        }

        if (GUI.Button(new Rect(20, 200, 180, 100),"LoadMap", buttonStyle))
        {
            showTextField = true;
        }

        GUIStyle textFieldStyle = new GUIStyle(GUI.skin.textField);
        textFieldStyle.fontSize = 30;

        if (showTextField)
        {
            userInput = GUI.TextField(new Rect(Screen.width / 4, Screen.height / 2, Screen.width / 2, 100), userInput, textFieldStyle);
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + 250 / 2, 180, 100), "Ȯ��", buttonStyle))
            {
                LoadMap(userInput);
                showTextField = false; // �ٽ� ���߰� �ʹٸ� �� �� ����
            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 250 / 2, 180, 100), "���", buttonStyle))
            {
                showTextField = false;
                //userInput = ""; // �Է� �ʱ�ȭ���� ���δ� ����
            }
        }
    }


    public void TestMapData()
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

    public Grid currentGrid { get; private set; }
    bool[,] isCollision;
    bool[,] isItem; // ���� ���������� �� �� �ʿ��Ѱ�?
    bool[,] isMonster; // ���� ���������� �� �� �ʿ��Ѱ�?
    GameObject map;
    GameObject dummy;
    MapData mapData;
    Tilemap baseTilemap;
    Sprite[] itemSprites;
    Sprite[] monsterSprites;

    public void LoadMap(string userInput)
    {
        map = CreateMap(userInput);
        Init(map);
        mapData = ParsingJsonMap(userInput);
        ParsingTextMap(userInput, mapData);
    }

    private GameObject CreateMap(string userInput)
    {
        var resource = Resources.Load<GameObject>($"Prefabs/{userInput}");
        return GameObject.Instantiate(resource);
    }

    private void Init(GameObject map)
    {
        // TODO
        // �� �����͸� ���ÿ� ���� �ؾߵ�
        currentGrid = map.GetComponent<Grid>();
        GameObject collisionMap = Util.FindChild(map, "CollisionMap", true);
        GameObject itemTile = Util.FindChild(map, "Item", true);
        GameObject monsterTile = Util.FindChild(map, "Monster", true);
        Tilemap baseTilemap = Util.FindChild<Tilemap>(map, "BaseMap", true);
        itemSprites = Resources.LoadAll<Sprite>("Sprite/Item/Item");
        monsterSprites = Resources.LoadAll<Sprite>("Sprite/Monster/Monster");

        if (collisionMap != null)
            collisionMap.SetActive(false);

        if (itemTile != null)
            itemTile.SetActive(false);

        if (monsterTile != null)
            monsterTile.SetActive(false);
    }

    private MapData ParsingJsonMap(string userInput)
    {
        // JSON ���� ���
        string jsonFilePath = $"Assets/Resources/{userInput}.json";

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

        return mapData;
    }

    public void ParsingTextMap(string userInput, MapData mapData)
    {
        // �ؽ�Ʈ ���� ���
        string textFilePath = $"Assets/Resources/{userInput}.txt";

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
        int row = 5;
        while (row < lines.Length && lines[row] != "itemTiles")
        {
            collisionTiles.Add(lines[row]);
            row++;
        }

        // itemTiles �б�
        List<string> itemTiles = new List<string>();
        row++;
        while (row < lines.Length && lines[row] != "monsterTiles")
        {
            itemTiles.Add(lines[row]);
            row++;
        }

        // monsterTiles �б�
        List<string> monsterTiles = new List<string>();
        row++;
        while (row < lines.Length)
        {
            monsterTiles.Add(lines[row]);
            row++;
        }

        // ����Ͽ� Ȯ��
        Debug.Log($"Bounds: xMin={xMin}, xMax={xMax}, yMin={yMin}, yMax={yMax}");
        Debug.Log("Collision Tiles:");

        int xCount = xMax - xMin + 1;
        int yCount = yMax - yMin + 1;

        isCollision = new bool[yCount, xCount];

        for (int y = 0; y < yCount; y++)
        {
            string line = collisionTiles[y];
            int flippedY = yCount - 1 - y;
            for (int x = 0; x < xCount; x++)
            {
                if (line[x] == '1')
                    isCollision[flippedY, x] = true;
                else
                    isCollision[flippedY, x] = false;
            }
        }

        isItem = new bool[itemTiles.Count, xCount];
        int itemIndex = 0;
        GameObject dummyItem = Resources.Load<GameObject>("Prefabs/Item/item");

        for (int y = 0; y < itemTiles.Count; y++)
        {
            string line = itemTiles[y];
            int flippedY = yCount - 1 - y;
            for (int x = 0; x < xCount; x++)
            {
                // ���� �� ��ǥ
                int cellX = x + xMin;
                int cellY = flippedY + yMin;

                // ���� ��ǥ�� ��ȯ
                Vector3 worldPos = baseTilemap.CellToWorld(new Vector3Int(cellX, cellY, 0))
                                   + baseTilemap.cellSize / 2f;
                if (line[x] == '0')
                {
                    // ���� ���� ������Ʈ ����
                    // ������ ���̿� �ش� ��������Ʈ�� ����
                    // �ִϸ��̼� �߰�
                    // ������ �߰�
                   
                    GameObject item = GameObject.Instantiate(dummyItem);
                    SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = Array.Find(itemSprites, sprite => sprite.name.Equals(mapData.itemList[itemIndex]));
                    Animator animator = item.GetComponent<Animator>();
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>($"Animations/Item/Controller/{mapData.itemList[itemIndex]}");
                    //
                    // ������ �߰�
                    //
                    item.transform.position = worldPos;
                    itemIndex++;
                }
            }
        }

        isMonster = new bool[monsterTiles.Count, xCount];
        int monsterIndex = 0;
        GameObject dummyMonster = Resources.Load<GameObject>("Prefabs/Monster/monster");

        for (int y = 0; y < monsterTiles.Count; y++)
        {
            string line = monsterTiles[y];
            int flippedY = yCount - 1 - y;
            for (int x = 0; x < xCount; x++)
            {
                // ���� �� ��ǥ
                int cellX = x + xMin;
                int cellY = flippedY + yMin;

                // ���� ��ǥ�� ��ȯ
                Vector3 worldPos = baseTilemap.CellToWorld(new Vector3Int(cellX, cellY, 0))
                                   + baseTilemap.cellSize / 2f;
                if (line[x] == '0')
                {
                    // ���� ���� ������Ʈ ����
                    // ������ ���̿� �ش� ��������Ʈ�� ����
                    // �ִϸ��̼� �߰�
                    // ������ �߰�
                    GameObject monster = GameObject.Instantiate(dummyMonster);
                    SpriteRenderer spriteRenderer = monster.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = Resources.Load<Sprite>($"Sprite/Monster/{mapData.monsterList[monsterIndex]}");
                    Animator animator = monster.GetComponent<Animator>();
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>($"Animations/Monster/{mapData.itemList[itemIndex]}");
                    //
                    // ������ �߰�
                    //
  
                    monster.transform.position = worldPos;
                    monsterIndex++;
                }
            }
        }
    }
    

    public void DestroyMap()
    {
        GameObject map = GameObject.Find("Map");
        if (map != null)
        {
            GameObject.Destroy(map);
            currentGrid = null;
        }
    }

    // ĳ�� ����
    //Sprite GetItemSprite(string name)
    //{
    //    if (!spriteCache.TryGetValue(name, out var sprite))
    //    {
    //        sprite = Resources.Load<Sprite>($"Sprite/Item/{name}");
    //        if (sprite == null)
    //        {
    //            Debug.LogWarning($"[��������Ʈ ����] Sprite/Item/{name}");
    //        }
    //        spriteCache[name] = sprite;
    //    }
    //    return sprite;
    //}
}

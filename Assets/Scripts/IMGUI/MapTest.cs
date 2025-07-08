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
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + 250 / 2, 180, 100), "확인", buttonStyle))
            {
                LoadMap(userInput);
                showTextField = false; // 다시 감추고 싶다면 이 줄 유지
            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 250 / 2, 180, 100), "취소", buttonStyle))
            {
                showTextField = false;
                //userInput = ""; // 입력 초기화할지 여부는 선택
            }
        }
    }


    public void TestMapData()
    {
        // JSON 파일 경로
        string jsonFilePath = $"Assets/Resources/Map/Base2/Map_001.json";

        // JSON 파일 읽기
        string jsonContent = File.ReadAllText(jsonFilePath);

        // JSON을 객체로 변환
        MapData mapData = JsonConvert.DeserializeObject<MapData>(jsonContent);

        // 출력하여 확인
        Debug.Log($"Map Name: {mapData.name}");
        Debug.Log($"Bounds: xMin={mapData.bounds.xMin}, xMax={mapData.bounds.xMax}, yMin={mapData.bounds.yMin}, yMax={mapData.bounds.yMax}");
        Debug.Log("Monster List:");
        foreach (var monster in mapData.monsterList)
        {
            Debug.Log(monster);
        }


        // 텍스트 파일 경로
        string textFilePath = $"Assets/Resources/Map/Base2/Map_001.txt";

        // 텍스트 파일 읽기
        string[] lines = File.ReadAllLines(textFilePath);

        // xMin, xMax, yMin, yMax 값을 추출
        string[] boundsLine = lines[0].Split(':');
        int xMin = int.Parse(boundsLine[1].Trim());
        int xMax = int.Parse(lines[1].Split(':')[1].Trim());
        int yMin = int.Parse(lines[2].Split(':')[1].Trim());
        int yMax = int.Parse(lines[3].Split(':')[1].Trim());

        // CollisionTiles 읽기
        List<string> collisionTiles = new List<string>();
        int i = 0;
        while (i < lines.Length && lines[i] != "itemTiles")
        {
            collisionTiles.Add(lines[i]);
            i++;
        }

        // itemTiles 읽기
        List<string> itemTiles = new List<string>();
        while (i < lines.Length && lines[i] != "monsterTiles")
        {
            itemTiles.Add(lines[i]);
            i++;
        }

        // monsterTiles 읽기
        List<string> monsterTiles = new List<string>();
        while (i < lines.Length)
        {
            monsterTiles.Add(lines[i]);
            i++;
        }

        // 출력하여 확인
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
    bool[,] isItem; // 추후 제거할지도 모름 꼭 필요한가?
    bool[,] isMonster; // 추후 제거할지도 모름 꼭 필요한가?
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
        // 맵 데이터를 스택에 저장 해야됨
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
        // JSON 파일 경로
        string jsonFilePath = $"Assets/Resources/{userInput}.json";

        // JSON 파일 읽기
        string jsonContent = File.ReadAllText(jsonFilePath);

        // JSON을 객체로 변환
        MapData mapData = JsonConvert.DeserializeObject<MapData>(jsonContent);

        // 출력하여 확인
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
        // 텍스트 파일 경로
        string textFilePath = $"Assets/Resources/{userInput}.txt";

        // 텍스트 파일 읽기
        string[] lines = File.ReadAllLines(textFilePath);

        // xMin, xMax, yMin, yMax 값을 추출
        string[] boundsLine = lines[0].Split(':');
        int xMin = int.Parse(boundsLine[1].Trim());
        int xMax = int.Parse(lines[1].Split(':')[1].Trim());
        int yMin = int.Parse(lines[2].Split(':')[1].Trim());
        int yMax = int.Parse(lines[3].Split(':')[1].Trim());

        // CollisionTiles 읽기
        List<string> collisionTiles = new List<string>();
        int row = 5;
        while (row < lines.Length && lines[row] != "itemTiles")
        {
            collisionTiles.Add(lines[row]);
            row++;
        }

        // itemTiles 읽기
        List<string> itemTiles = new List<string>();
        row++;
        while (row < lines.Length && lines[row] != "monsterTiles")
        {
            itemTiles.Add(lines[row]);
            row++;
        }

        // monsterTiles 읽기
        List<string> monsterTiles = new List<string>();
        row++;
        while (row < lines.Length)
        {
            monsterTiles.Add(lines[row]);
            row++;
        }

        // 출력하여 확인
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
                // 실제 맵 좌표
                int cellX = x + xMin;
                int cellY = flippedY + yMin;

                // 월드 좌표로 변환
                Vector3 worldPos = baseTilemap.CellToWorld(new Vector3Int(cellX, cellY, 0))
                                   + baseTilemap.cellSize / 2f;
                if (line[x] == '0')
                {
                    // 더미 게임 오브젝트 생성
                    // 생성된 더미에 해당 스프라이트로 변경
                    // 애니메이션 추가
                    // 데이터 추가
                   
                    GameObject item = GameObject.Instantiate(dummyItem);
                    SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = Array.Find(itemSprites, sprite => sprite.name.Equals(mapData.itemList[itemIndex]));
                    Animator animator = item.GetComponent<Animator>();
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>($"Animations/Item/Controller/{mapData.itemList[itemIndex]}");
                    //
                    // 데이터 추가
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
                // 실제 맵 좌표
                int cellX = x + xMin;
                int cellY = flippedY + yMin;

                // 월드 좌표로 변환
                Vector3 worldPos = baseTilemap.CellToWorld(new Vector3Int(cellX, cellY, 0))
                                   + baseTilemap.cellSize / 2f;
                if (line[x] == '0')
                {
                    // 더미 게임 오브젝트 생성
                    // 생성된 더미에 해당 스프라이트로 변경
                    // 애니메이션 추가
                    // 데이터 추가
                    GameObject monster = GameObject.Instantiate(dummyMonster);
                    SpriteRenderer spriteRenderer = monster.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = Resources.Load<Sprite>($"Sprite/Monster/{mapData.monsterList[monsterIndex]}");
                    Animator animator = monster.GetComponent<Animator>();
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>($"Animations/Monster/{mapData.itemList[itemIndex]}");
                    //
                    // 데이터 추가
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

    // 캐싱 예시
    //Sprite GetItemSprite(string name)
    //{
    //    if (!spriteCache.TryGetValue(name, out var sprite))
    //    {
    //        sprite = Resources.Load<Sprite>($"Sprite/Item/{name}");
    //        if (sprite == null)
    //        {
    //            Debug.LogWarning($"[스프라이트 없음] Sprite/Item/{name}");
    //        }
    //        spriteCache[name] = sprite;
    //    }
    //    return sprite;
    //}
}

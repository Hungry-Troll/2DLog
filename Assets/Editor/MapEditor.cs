using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using UnityEditor;
using Newtonsoft.Json;

public class MapEditor
{
    // Editor 폴더에 있는 클래스는 Asset 폴더와 어셈블리가 다름
    [System.Serializable]
    public class MapData
    {
        public string name;
        public BoundsData bounds;
        public List<string> itemList;
        public List<string> monsterList;
    }

    [System.Serializable]
    public class BoundsData
    {
        public int xMin, xMax, yMin, yMax;
    }

    // 콜라이더 맵 체크
    // %(Ctrl) #(Shift) &(Alt) 단축키 세팅
    [MenuItem("Tools/GenerateMap %#s")]
    private static void GenerateMap()
    {
        GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Map/Base2");

        foreach (GameObject go in gameObjects)
        {
            Tilemap baseMap = Util.FindChild<Tilemap>(go, "BaseMap", true);
            Tilemap collisionMap = Util.FindChild<Tilemap>(go, "CollisionMap", true);
            Tilemap stairUpMap = Util.FindChild<Tilemap>(go, "StairUp", true);
            Tilemap stairDownMap = Util.FindChild<Tilemap>(go, "StairDown", true);
            Tilemap trapMap = Util.FindChild<Tilemap>(go, "Trap", true);
            Tilemap itemMap = Util.FindChild<Tilemap>(go, "Item", true);
            Tilemap monsterMap = Util.FindChild<Tilemap>(go, "Monster", true);

            var data = new MapData
            {
                name = go.name,
                bounds = new BoundsData
                {
                    xMin = baseMap.cellBounds.xMin,
                    xMax = baseMap.cellBounds.xMax,
                    yMin = baseMap.cellBounds.yMin,
                    yMax = baseMap.cellBounds.yMax
                },
                itemList = new(),
                monsterList = new(),
            };

            data = GetCollisionData(baseMap, collisionMap, stairUpMap, stairDownMap, trapMap, data);
            data = GetItemData(baseMap, itemMap, data);
            data = GetMonsterData(baseMap, monsterMap, data);

            string json = JsonConvert.SerializeObject(data, Formatting.Indented); 
            string path = $"Assets/Resources/Map/Base2/{go.name}.json";
            File.WriteAllText(path, json, System.Text.Encoding.UTF8);

            // 에셋 데이터베이스 새로고침
            AssetDatabase.Refresh();

            Debug.Log($"Map JSON 저장됨: {path}");
        }
    }

    private static MapData GetCollisionData(Tilemap baseMap,
                                     Tilemap collisionMap,
                                     Tilemap stairUpMap,
                                     Tilemap stairDownMap,
                                     Tilemap trapMap,
                                     MapData data)
    {
        // 사람눈으로 보기 쉬운 txt 파일
        using (var writer = File.CreateText($"Assets/Resources/Map/Base2/{data.name}.txt"))
        {
            writer.WriteLine($"xMin:{data.bounds.xMin}");
            writer.WriteLine($"xMax:{data.bounds.xMax}");
            writer.WriteLine($"yMin:{data.bounds.yMin}");
            writer.WriteLine($"yMax:{data.bounds.yMax}");
            writer.WriteLine("collisionTiles");
            //x,y 최대값으로 계산하면 한줄씩 더 생김
            for (int y = data.bounds.yMax; y >= data.bounds.yMin; y--)
            {
                for (int x = data.bounds.xMin; x <= data.bounds.xMax; x++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    TileBase collistionTile = collisionMap.GetTile(pos);
                    TileBase stairUpTile = stairUpMap.GetTile(pos);
                    TileBase stairDownTile = stairDownMap.GetTile(pos);
                    TileBase trapTile = trapMap.GetTile(pos);
                    if (collistionTile != null)
                        writer.Write("1");
                    else if (stairUpTile != null)
                        writer.Write("2");
                    else if (stairDownTile != null)
                        writer.Write("3");
                    else if (trapTile != null)
                        writer.Write("4");
                    else
                        writer.Write("0");
                }
                writer.WriteLine();
            }
        }

        return data;
    }

    private static MapData GetItemData(Tilemap baseMap,
                                Tilemap itemMap,
                                MapData data)
    {
        List<string> itemList = new();

        // 사람눈으로 보기 쉬운 txt 파일
        using (var writer = File.AppendText($"Assets/Resources/Map/Base2/{data.name}.txt"))
        {
            writer.WriteLine("itemTiles");

            //x,y 최대값으로 계산하면 한줄씩 더 생김
            for (int y = data.bounds.yMax; y >= data.bounds.yMin; y--)
            {
                for (int x = data.bounds.xMin; x <= data.bounds.xMax; x++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    TileBase itemTile = itemMap.GetTile(pos);
                    if (itemTile != null)
                    {
                        writer.Write("0");
                        Tile t = itemTile as Tile;
                        if (t != null && t.sprite != null)
                            itemList.Add(t.sprite.name);
                    }
                    else
                        writer.Write("1");
                }
                writer.WriteLine();
            }
            data.itemList = itemList; // json 파일에 아이템 리스트 반환
        }
        return data;
    }


    private static MapData GetMonsterData(Tilemap baseMap,
                                    Tilemap monsterMap,
                                    MapData data)
    {
        List<string> monsterList = new();
        // 사람눈으로 보기 쉬운 txt 파일
        using (var writer = File.AppendText($"Assets/Resources/Map/Base2/{data.name}.txt"))
        {
            writer.WriteLine("monsterTiles");
            //x,y 최대값으로 계산하면 한줄씩 더 생김
            for (int y = data.bounds.yMax; y >= data.bounds.yMin; y--)
            {
                for (int x = data.bounds.xMin; x <= data.bounds.xMax; x++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    TileBase monsterTile = monsterMap.GetTile(pos);
                    if (monsterTile != null)
                    {
                        writer.Write("0");
                        Tile t = monsterTile as Tile;
                        if (t != null && t.sprite != null)
                            monsterList.Add(t.sprite.name);
                    }
                    else
                        writer.Write("1");
                }
                writer.WriteLine();
            }
            data.monsterList = monsterList; // json 파일에 monsterList 반환
        }
        return data;
    }

    //private static void GenerateMap()
    //{

    //    GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Map/Base2");


    //    foreach (GameObject go in gameObjects)
    //    {
    //        Tilemap tmBase = Util.FindChild<Tilemap>(go, "Tilemap", true);
    //        Tilemap cm = Util.FindChild<Tilemap>(go, "CollisionMap", true); //1
    //        Tilemap stairUp = Util.FindChild<Tilemap>(go, "StairUp", true); //2
    //        Tilemap stairDown = Util.FindChild<Tilemap>(go, "StairDown", true); //3
    //        Tilemap trap = Util.FindChild<Tilemap>(go, "Trap", true); //4
    //        /*if (tm == null)
    //            return;*/

    //        /*            //블럭에 못들어가는 좌표 넣음
    //                    List<Vector3Int> blocked = new List<Vector3Int>();

    //                    foreach (Vector3Int pos in tm.cellBounds.allPositionsWithin)
    //                    {
    //                        TileBase tile = tm.GetTile(pos);
    //                        if (tile != null)
    //                        {
    //                            blocked.Add(pos);
    //                        }
    //                    }*/

    //        //콜라이더 맵 자료 추출
    //        using (var writer = File.CreateText($"Assets/Resources/Map/Base2/{go.name}.txt"))
    //        {

    //            writer.WriteLine(tmBase.cellBounds.xMin);
    //            writer.WriteLine(tmBase.cellBounds.xMax);
    //            writer.WriteLine(tmBase.cellBounds.yMin);
    //            writer.WriteLine(tmBase.cellBounds.yMax);
    //            //x,y 최대값으로 계산하면 한줄씩 더 생김
    //            for (int y = tmBase.cellBounds.yMax; y >= tmBase.cellBounds.yMin; y--)
    //            {
    //                for (int x = tmBase.cellBounds.xMin; x <= tmBase.cellBounds.xMax; x++)
    //                {
    //                    TileBase _tile = cm.GetTile(new Vector3Int(x, y, 0));
    //                    TileBase _stairUp = stairUp.GetTile(new Vector3Int(x, y, 0));
    //                    TileBase _stairDown = stairDown.GetTile(new Vector3Int(x, y, 0));
    //                    TileBase _trap = trap.GetTile(new Vector3Int(x, y, 0));
    //                    if (_tile == null && _stairUp == null && _stairDown == null && _trap == null)
    //                        writer.Write("0");
    //                    else if (_tile != null)
    //                        writer.Write("1");
    //                    else if (_stairUp != null)
    //                        writer.Write("2");
    //                    else if (_stairDown != null)
    //                        writer.Write("3");
    //                    else if (_trap != null)
    //                        writer.Write("4");
    //                }
    //                writer.WriteLine();
    //            }
    //        }
    //    }
    //}
}

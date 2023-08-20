using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class MapEditor
{

#if UNITY_EDITOR

    // 콜라이더 맵 체크
    // %(Ctrl) #(Shift) &(Alt) 단축키 세팅
    [MenuItem("Tools/GenerateMap %#g")]
    private static void GenerateMap()
    {

        GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Map/Base2");


        foreach (GameObject go in gameObjects)
        {
            Tilemap tmBase = Util.FindChild<Tilemap>(go, "Tilemap", true);
            Tilemap tm = Util.FindChild<Tilemap>(go, "CollisionMap", true); //1
            Tilemap StairUp = Util.FindChild<Tilemap>(go, "StairUp", true); //2
            Tilemap StairDown = Util.FindChild<Tilemap>(go, "StairDown", true); //3
            Tilemap Trap = Util.FindChild<Tilemap>(go, "Trap", true); //4
            /*if (tm == null)
                return;*/

            /*            //블럭에 못들어가는 좌표 넣음
                        List<Vector3Int> blocked = new List<Vector3Int>();

                        foreach (Vector3Int pos in tm.cellBounds.allPositionsWithin)
                        {
                            TileBase tile = tm.GetTile(pos);
                            if (tile != null)
                            {
                                blocked.Add(pos);
                            }
                        }*/

            //콜라이더 맵 자료 추출
            using (var writer = File.CreateText($"Assets/Resources/Map/Base2/{go.name}.txt"))
            {

                writer.WriteLine(tmBase.cellBounds.xMin);
                writer.WriteLine(tmBase.cellBounds.xMax);
                writer.WriteLine(tmBase.cellBounds.yMin);
                writer.WriteLine(tmBase.cellBounds.yMax);
                //x,y 최대값으로 계산하면 한줄씩 더 생김
                for (int y = tmBase.cellBounds.yMax; y >= tmBase.cellBounds.yMin; y--)
                {
                    for (int x = tmBase.cellBounds.xMin; x <= tmBase.cellBounds.xMax; x++)
                    {
                        TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));
                        TileBase _StairUp = StairUp.GetTile(new Vector3Int(x, y, 0));
                        TileBase _StairDown = StairDown.GetTile(new Vector3Int(x, y, 0));
                        TileBase _Trap = Trap.GetTile(new Vector3Int(x, y, 0));
                        if (tile == null && _StairUp == null && _StairDown == null && _Trap == null)
                            writer.Write("0");
                        else if (tile != null)
                            writer.Write("1");
                        else if (_StairUp != null)
                            writer.Write("2");
                        else if (_StairDown != null)
                            writer.Write("3");
                        else if (_Trap != null)
                            writer.Write("4");
                    }
                    writer.WriteLine();
                }
            }
        }


#endif
    }
}

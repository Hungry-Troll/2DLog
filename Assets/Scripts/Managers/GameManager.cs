using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;



public class GameManager : MonoBehaviour
{
    static GameManager s_instance;
    public static GameManager Instance { get { return s_instance; } }


    #region Contents
    MapManager _map = new MapManager();
    ObjectManager _obj = new ObjectManager();
    UIManager _ui = new UIManager();
    SummonManager _summon = new SummonManager();
    DataManager _data = new DataManager();
    InvenManager _inven = new InvenManager();

    public static MapManager Map { get { return Instance._map; } }
    public static ObjectManager Obj { get { return Instance._obj; } }
    public static UIManager Ui { get { return Instance._ui; } }
    public static SummonManager Summon { get { return Instance._summon; } }
    public static DataManager Data { get { return Instance._data; } }
    public static InvenManager Inven { get { return Instance._inven; } }

    #endregion
    #region Core
    SceneManagerEx _scene = new SceneManagerEx();
    ResourceManager _resouce = new ResourceManager();
    TurnManager _Turn = new TurnManager();
    EventManager _evt = new EventManager();
    


    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static TurnManager TurnM { get { return Instance._Turn; } }
    public static ResourceManager Resouce { get { return Instance._resouce; } }
    public static EventManager evt { get { return Instance._evt; } }
    #endregion

    void Awake()
    {
        Init();
    }

    void Start()
    {
        
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if (go == null)
            {
                go = new GameObject { name = "GameManager" };
                go.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<GameManager>();
        }

        s_instance._data.Init();
        s_instance._Turn.Init();

    }

}

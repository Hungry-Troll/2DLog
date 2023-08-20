using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { return s_instance; } }


    InputManager _input = new InputManager();
    //ResourceManager _resouce = new ResourceManager();

    
    public static InputManager Input { get { return Instance._input; } }
    //public static ResourceManager Resouce { get { return Instance._resouce; } }

    void Start()
    {
        init();
    }

   
    static void init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("Managers");
            if (go == null)
            {
                go = new GameObject { name = "Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }

    void Update()
    {
        Input.OnUpdate();
        //GameObject tank = Managers.Resouce.Instantiate("Tank"); 생성코드예시
        //Resouce.Destroy(tank);삭제코드예시
    }
}

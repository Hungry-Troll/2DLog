using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    private Define.Scene _sceneType;
    public Define.Scene SceneType 
    {
        get { return _sceneType; }
        protected set { _sceneType = value; }
    } 

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            GameManager.Resouce.Instantiate("UI/EventSystem").name = "@EventSystem";

        //GameManager.Resouce.Instantiate("UI/UI_Buttons");
    }

    public abstract void Clear();
}

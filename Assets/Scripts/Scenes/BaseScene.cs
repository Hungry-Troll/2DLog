using System;
using System.Collections;
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

    public Action _sceneChange;

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        UnityEngine.Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            GameManager.Resouce.Instantiate("UI/EventSystem").name = "@EventSystem";

        //    많아지면 ADDEvent 함수로 뺄 것
        _sceneChange = () =>
        {
            StartCoroutine(SceneChange(Define.Scene.DungeonSelect));
        };
    }

    public abstract void Clear();

    protected IEnumerator SceneChange(Define.Scene sceneName)
    {
        SystemManager.Instance.FadeOnOff();
        yield return GameManager.Yield.WaitForSecond(1.0f);
        GameManager.Scene.LoadScene(sceneName);
        yield return GameManager.Yield.WaitForSecond(2.0f);
    }
}

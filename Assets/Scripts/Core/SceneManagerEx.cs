﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneManagerEx 
{
    //public Action _SceneChangeAction;
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>();} }

    public void LoadScene(Define.Scene type)
    {
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(type));
        //_SceneChangeAction.Invoke();
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }
}

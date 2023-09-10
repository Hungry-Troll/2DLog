using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : Singleton<SystemManager>
{
    //TODO
    //우선 SceneChange를 들고 있는다

    GameObject _ui_SceneChage;
    public void FadeOnOff()
    {
        if(_ui_SceneChage == null)
        {
            // 매니저 클래스들이 모노가 없음 여기서 만들기만 할 것
            string name = "UI_SceneChange";
            _ui_SceneChage = GameManager.Resouce.Instantiate($"UI/Popup/{name}");
            _ui_SceneChage.transform.SetParent(this.transform);
            _ui_SceneChage.SetActive(false);
        }
        _ui_SceneChage.SetActive(true); //실질적인 로직은 UI_SceneChage에서 처리함
    }
}

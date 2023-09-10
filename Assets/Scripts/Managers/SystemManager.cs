using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : Singleton<SystemManager>
{
    //TODO
    //�켱 SceneChange�� ��� �ִ´�

    GameObject _ui_SceneChage;
    public void FadeOnOff()
    {
        if(_ui_SceneChage == null)
        {
            // �Ŵ��� Ŭ�������� ��밡 ���� ���⼭ ����⸸ �� ��
            string name = "UI_SceneChange";
            _ui_SceneChage = GameManager.Resouce.Instantiate($"UI/Popup/{name}");
            _ui_SceneChage.transform.SetParent(this.transform);
            _ui_SceneChage.SetActive(false);
        }
        _ui_SceneChage.SetActive(true); //�������� ������ UI_SceneChage���� ó����
    }
}

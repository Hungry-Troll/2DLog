using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    //팝업관리 매니저
    public int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvase(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent <Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //overrideSorting = 부모가 어떤값이건 sort오더를 가짐
        canvas.overrideSorting = true;
        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
            

    }

    public T MakeSubItme<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameManager.Resouce.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameManager.Resouce.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);

        _sceneUI = sceneUI;
        _order++;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    //팝업 열 때 사용함 프리팹명하고 스크립트명을 동일하게 만들어서 사용하면 됨
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {

        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameManager.Resouce.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);
        _order++;

        go.transform.SetParent(Root.transform);

            return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;
        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failde!");
        }
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        GameManager.Resouce.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    public void CloseSceneUI()
    {
        if (_sceneUI == null)
            return;

        GameManager.Resouce.Destroy(_sceneUI.gameObject);
        _sceneUI = null;
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    #region old_Code
    /*    GameObject[] bt = new GameObject[4];
        Button[] DirButton = new Button[4];

        public void Init()
        {
            ButtonFind();
        }

        public void ButtonFind()
        {
            bt[0] = GameObject.FindGameObjectWithTag("Up");
            bt[1] = GameObject.FindGameObjectWithTag("Down");
            bt[2] = GameObject.FindGameObjectWithTag("Left");
            bt[3] = GameObject.FindGameObjectWithTag("Right");


            int i;
            for (i = 0; i < 4; i++)
            {
                DirButton[i] = bt[i].GetComponent<Button>();
            }
            return;
        }

        public void ButtonOn()
        {
            int i;
            for (i = 0; i < 4; i++)//버튼 활성화
            {
                DirButton[i].interactable = true;
            }
        }

        public void ButtonOff()
        {
            int i;

            for (i = 0; i < 4; i++)//버튼 비활성화
            {
                DirButton[i].interactable = false;
            }
        }*/
    #endregion
}

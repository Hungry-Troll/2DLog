using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven : UI_Scene
{
    //인벤토리좌표관련
    List<int> rPosX = new List<int>();
    List<int> rPosY = new List<int>();
    int rCount;
    GameObject item;
    Vector3 inVec;


    GameObject gridPanel;

    enum Buttons
    {
        State_Button,
        Bag_Button,
        Potion_Button,
        Book_Button,
        Skill_Button,
        Controller_Button,
    }

    enum GameObjects
    {
        GridPanel
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        //인벤토리 좌표 초기화
        //int rCount = 23;

        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        gridPanel = Get<GameObject>((int)GameObjects.GridPanel);

        //foreach (Transform child in gridPanel.transform)
            //GameManager.Resouce.Destroy(child.gameObject);
        //클릭시 사용 각각 함수를 만들어서 사용해야 됨
        GetButton((int)Buttons.State_Button).gameObject.BindEvent(StateButtonClicked);
        GetButton((int)Buttons.Bag_Button).gameObject.BindEvent(BagButtonClicked);
        GetButton((int)Buttons.Potion_Button).gameObject.BindEvent(PotionButtonClicked);
        GetButton((int)Buttons.Book_Button).gameObject.BindEvent(BookButtonClicked);
        GetButton((int)Buttons.Skill_Button).gameObject.BindEvent(SkillButtonClicked);
        GetButton((int)Buttons.Controller_Button).gameObject.BindEvent(ControllerButtonClicked);

        GameManager.Ui.ShowSceneUI<UI_DirBase>();

    }

    public void StateButtonClicked(PointerEventData data)
    {
        GameManager.Ui.CloseSceneUI();
        GameManager.Ui.ShowSceneUI<UI_State>();
    }

    public void BagButtonClicked(PointerEventData data)
    {
        GameManager.Ui.CloseSceneUI();
        GameManager.Ui.ShowSceneUI<UI_Bag>();
/*        ///////////인벤토리 위치 정렬코드
        InvenArrayF();
        for (int i = 0; i < 12; i++)
        {

            int random = Random.Range(0, 3);
            if (random == 1)
            {
                item = GameManager.Ui.MakeSubItme<UI_Inven_Item>(parent: gridPanel.transform).gameObject;
            }
            else if (random == 2)
            {
                item = GameManager.Ui.MakeSubItme<UI_Inven_Ring>(parent: gridPanel.transform).gameObject;
            }
            else
            {
                item = GameManager.Ui.MakeSubItme<UI_Inven_Empty>(parent: gridPanel.transform).gameObject;
            }
            inVec.Set(rPosX[i], rPosY[i], 0);
            item.GetComponent<RectTransform>().anchoredPosition = inVec;
            //GameObject item = GameManager.Ui.MakeSubItme<UI_Inven_Item>(parent : gridPanel.transform).gameObject;
            *//*UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"집행검{ i}번");*//*
        }*/
    }

    public void PotionButtonClicked(PointerEventData data)
    {

    }

    public void BookButtonClicked(PointerEventData data)
    {

    }

    public void SkillButtonClicked(PointerEventData data)
    {

    }

    public void ControllerButtonClicked(PointerEventData data)
    {
        GameManager.Ui.CloseSceneUI();
        GameManager.Ui.ShowSceneUI<UI_DirBase>();
    }

    public void InvenArrayF()
    {
        ////인벤토리 아이콘 슬롯 가로 숫자
        int rCount = 6;
        ////인벤토리 r트랜스폼 x 좌표
        int x = 44;
        int y = -60;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < rCount; j++)
            {
                rPosX.Add(x);
                rPosY.Add(y);
                x += 120;
            }
            x = 44;
            y -= 120;
        }
    }
}



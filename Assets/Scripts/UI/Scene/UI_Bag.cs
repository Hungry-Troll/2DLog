using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Bag : UI_Scene
{
    //인벤토리좌표관련
    List<int> rPosX = new List<int>();
    List<int> rPosY = new List<int>();
    int rCount;
    GameObject item;
    Vector3 inVec;


    GameObject gridPanel;
    GameObject invenPanel;
    GameObject bagPanel;
    enum GameObjects
    {
        BagPanel,
    }

    private void Start()
    {

        Bind<GameObject>(typeof(GameObjects));
        bagPanel = Get<GameObject>((int)GameObjects.BagPanel);
        ///////////인벤토리 위치 정렬코드
        InvenArrayF();
        for (int i = 0; i < 12; i++)
        {

            int random = Random.Range(0, 3);
            if (random == 1)
            {
                // MakeSubItem<이름> 으로 생성 //동일한 스크립트를 만들어야됨
                item = GameManager.Ui.MakeSubItme<UI_Inven_Item>(parent : bagPanel.transform).gameObject;
            }
            else if (random == 2)
            {
                item = GameManager.Ui.MakeSubItme<UI_Inven_Ring>(parent: bagPanel.transform).gameObject;
            }
            else
            {
                item = GameManager.Ui.MakeSubItme<UI_Inven_Empty>(parent: bagPanel.transform).gameObject;
            }
            inVec.Set(rPosX[i], rPosY[i], 0);
            item.GetComponent<RectTransform>().anchoredPosition = inVec;
            //GameObject item = GameManager.Ui.MakeSubItme<UI_Inven_Item>(parent : gridPanel.transform).gameObject;
            /*UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"집행검{ i}번");*/
        }
    }

    void Update()
    {
        
    }

    public void InvenArrayF()
    {
        ////인벤토리 아이콘 슬롯 가로 숫자
        int rCount = 6;
        ////인벤토리 r트랜스폼 x 좌표
        int x = 74;
        int y = -90;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < rCount; j++)
            {
                rPosX.Add(x);
                rPosY.Add(y);
                x += 220;
            }
            x = 74;
            y -= 220;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Empty : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }

    string _name;


    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        //이름 바꾸기
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;


        //클릭하면 아이템 장착 사용 기타등등 넣어볼것
        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log($"아이템 클릭!{_name}"); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }

}

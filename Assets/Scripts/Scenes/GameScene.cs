using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameScene : BaseScene
{
    

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        GameManager.Map.LoadMap();

        GameManager.Summon.PlayerCreat();

        GameManager.Summon.MonsterCreat();

        GameManager.Summon.ItemPool();

        //UI테스트
        //GameManager.Ui.ShowPopupUI<UI_Button>();

        //UI_Button ui = GameManager.Ui.ShowPopupUI<UI_Button>();

        //GameManager.Ui.ClosePopupUI(ui);

        //UI 인벤토리 테스트
        GameManager.Ui.ShowSceneUI<UI_Inven>();
        
        //GameManager.Ui.ShowSceneUI<ItemIcon>();

    }

    


    public override void Clear()
    {
        
    }


}

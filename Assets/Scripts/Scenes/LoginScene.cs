using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;
        // 매니저 클래스들이 모노가 없음 여기서 만들기만 할 것
        var sceneChange = GameManager.Ui.ShowPopupUI<UI_SceneChange>("UI_SceneChange");
        sceneChange.gameObject.SetActive(false);
        DontDestroyOnLoad(sceneChange);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Scene.LoadScene(Define.Scene.Game);
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }


}

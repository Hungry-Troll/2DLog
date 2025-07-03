using UnityEngine;

public class LoginScene : BaseScene
{
    [SerializeField] private TitleController _titleController;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        _titleController._sceneChange = _sceneChange;
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }


}

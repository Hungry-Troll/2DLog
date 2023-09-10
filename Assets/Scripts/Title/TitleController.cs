using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _optionButton;
    [SerializeField] Button _endButton;
    // Start is called before the first frame update
    void Start()
    {
        _startButton.onClick.AddListener(() =>
        {
            StartCoroutine(SceneChange());
        });
        _optionButton.onClick.AddListener(() => { }); // �ɼ�â
        _endButton.onClick.AddListener(() => Application.Quit()); // �����ϱ�

    }

    // �� �̵��� ���
    IEnumerator SceneChange()
    {
        SystemManager.Instance.FadeOnOff();
        yield return GameManager.Yield.WaitForSecond(1.0f);
        GameManager.Scene.LoadScene(Define.Scene.DungeonSelect);
        yield return GameManager.Yield.WaitForSecond(2.0f);
    }

}

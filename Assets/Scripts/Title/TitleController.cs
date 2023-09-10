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
        _optionButton.onClick.AddListener(() => { }); // 옵션창
        _endButton.onClick.AddListener(() => Application.Quit()); // 종료하기

    }

    // 씬 이동시 사용
    IEnumerator SceneChange()
    {
        SystemManager.Instance.FadeOnOff();
        yield return GameManager.Yield.WaitForSecond(1.0f);
        GameManager.Scene.LoadScene(Define.Scene.DungeonSelect);
        yield return GameManager.Yield.WaitForSecond(2.0f);
    }

}

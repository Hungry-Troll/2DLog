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
            //GameManager.Ui.
            GameManager.Scene.LoadScene(Define.Scene.Game);

        });
        _optionButton.onClick.AddListener(() => { }); // 옵션창
        _endButton.onClick.AddListener(() => Application.Quit()); // 종료하기
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

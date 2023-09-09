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
        _optionButton.onClick.AddListener(() => { }); // �ɼ�â
        _endButton.onClick.AddListener(() => Application.Quit()); // �����ϱ�
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

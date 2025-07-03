using System;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _optionButton;
    [SerializeField] Button _endButton;

    public Action _sceneChange { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        _startButton.onClick.AddListener(() =>
        {
            _sceneChange.Invoke();
        });
        _optionButton.onClick.AddListener(() => { }); // �ɼ�â
        _endButton.onClick.AddListener(() => Application.Quit()); // �����ϱ�

    }
}

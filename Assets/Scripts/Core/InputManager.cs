using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using System;
using static Define;

public class InputManager : Singleton<InputManager>
{
    float _downPos;
    float _upPos;

    Action _leftDragAction;
    Action _rightDragAction;
    public Action _LeftDragAction { get { return _leftDragAction; } set { _leftDragAction = value; } }
    public Action _RightDragAction { get { return _rightDragAction; } set { _rightDragAction = value; } }

    //
    Define.UIEvent _uiEvent = Define.UIEvent.None;
    public Define.UIEvent _UiEvent { get { return _uiEvent; } set { _uiEvent = value; } }
    //

    public void Init()
    {
        _uiEvent = Define.UIEvent.Drag;

        var clickStream = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0) && _uiEvent == Define.UIEvent.Drag)
            .Subscribe(_ =>
            {
                _downPos = Input.mousePosition.x;
                Debug.LogError("click");
            }); //터치시


        var upStream = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0) && _uiEvent == Define.UIEvent.Drag)
            .Select(_ => _upPos = Input.mousePosition.x)
            .Subscribe(_ =>
            {
                float distance = _downPos - _upPos;
                float result = Mathf.Abs(distance);
                Debug.LogError("_downPos : " + _downPos);
                Debug.LogError("_upPos : " + _upPos);
                Debug.LogError("result : " + result);

                if (result < 100.0f)
                {
                    Debug.LogError("Short");
                    return;
                }
                    
                if (_downPos >= _upPos)
                    _leftDragAction();
                else
                    _rightDragAction();
                
                Debug.LogError("up");
                // 땔 때 할 일
            });
    }
}

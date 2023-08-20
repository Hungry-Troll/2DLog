using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_DirBase : UI_Scene
{
    protected MoveDir _dir = MoveDir.None;
    protected MoveDir Dir
    {
        get { return _dir; }
        set
        {
            if (_dir == value)
                return;

            _dir = value;
        }
    }

    
}

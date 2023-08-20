using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class UI_Down : UI_DirBase, IPointerClickHandler
{
    MoveDir inputDir;

    public void OnPointerClick(PointerEventData eventData)
    {
        inputDir = MoveDir.Down;
        GameManager.evt.InputDir(inputDir);
    }

}

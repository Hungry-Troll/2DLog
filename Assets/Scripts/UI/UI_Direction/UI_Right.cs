using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class UI_Right : UI_DirBase, IPointerClickHandler
{
    MoveDir inputDir;

    public void OnPointerClick(PointerEventData eventData)
    {
        inputDir = MoveDir.Right;
        GameManager.evt.InputDir(inputDir);
    }

}

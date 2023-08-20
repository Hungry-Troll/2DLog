using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class UI_Left : UI_DirBase, IPointerClickHandler
{
    MoveDir inputDir;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Left");
        inputDir = MoveDir.Left;
        GameManager.evt.InputDir(inputDir);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Skill_01 : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.evt.PlayerSkill_01();
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}

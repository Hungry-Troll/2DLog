using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IBeginDragHandler, IPointerClickHandler, IDragHandler
{

    public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnClickedHandler = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragHandler != null)
            OnBeginDragHandler.Invoke(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (OnClickedHandler != null)
            OnClickedHandler.Invoke(eventData);
    }


    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = eventData.position;
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
        
    }

   
}

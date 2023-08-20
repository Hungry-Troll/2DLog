using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenManager
{
    List<GameObject> _invenItem = new List<GameObject>();

    public void Add(GameObject go)
    {
        _invenItem.Add(go);
    }

    public void Remove(GameObject go)
    {
        _invenItem.Remove(go);
    }
}

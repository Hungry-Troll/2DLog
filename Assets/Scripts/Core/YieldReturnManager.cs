using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldReturnManager
{
    WaitForSeconds wait;
    Dictionary<float, WaitForSeconds> _dic = new Dictionary<float, WaitForSeconds>();
    // Start is called before the first frame update

    public WaitForSeconds WaitForSecond(float num)
    {
        if (_dic.TryGetValue(num, out wait))
            return wait;
        else
        {
            _dic.Add(num, new WaitForSeconds(num));
            return _dic[num];
        }
    }
}

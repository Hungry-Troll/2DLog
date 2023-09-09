using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    public static T instance;

    private void Start()
    {
        if(instance == null)
        {
            //instance
        }
    }
}

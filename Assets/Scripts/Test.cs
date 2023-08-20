using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExEx : MonoBehaviour
{
    
    void Start()
    {
        int i=1;
        int num1 = 1; ;
        switch (i)
        {
            case 1:
                num1++;
                break;
            case 2:
                break;
        }

        if (i == 1)
        {
            num1--;
        }
    }

   
    void Update()
    {
        
    }
}

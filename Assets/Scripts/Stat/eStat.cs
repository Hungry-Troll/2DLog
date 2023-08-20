using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eStat : MonoBehaviour
{

    [SerializeField]
    int _No;
    [SerializeField]
    string _Name;
    [SerializeField]
    int _sum;
    [SerializeField]
    int _magic;
    [SerializeField]
    int _gold;
    [SerializeField]
    string _NickName;
    [SerializeField]
    string _comment;

    public int No { get { return _No; } set { _No = value; } }
    public string Name { get { return _Name; } set { _Name = value; } }
    public int Sum { get { return _sum; } set { _sum = value; } }
    public int magic { get { return _magic; } set { _magic = value; } }
    public int gold { get { return _gold; } set { _gold = value; } }
    public string NickName { get { return _NickName; } set { _NickName = value; } }
    public string comment { get { return _comment; } set { _comment = value; } }
}

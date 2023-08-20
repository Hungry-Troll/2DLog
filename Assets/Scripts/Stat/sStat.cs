using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sStat : MonoBehaviour
{
    [SerializeField]
    int _No;
    [SerializeField]
    string _Name;
    [SerializeField]
    int _cTel;
    [SerializeField]
    int _rTel;
    [SerializeField]
    int _sum;
    [SerializeField]
    int _fear;
    [SerializeField]
    int _fog;
    [SerializeField]
    int _fireDam;
    [SerializeField]
    int _slient;
    [SerializeField]
    int _resurrect;
    [SerializeField]
    int _amnesia;
    [SerializeField]
    int _acquire;
    [SerializeField]
    int _avoid;
    [SerializeField]
    int _enhance;
    [SerializeField]
    int _turn;
    [SerializeField]
    string _NickName;
    [SerializeField]
    string _comment;


    public int No { get { return _No; } set { _No = value; } }
    public string Name { get { return _Name; } set { _Name = value; } }
    public int cTel { get { return _cTel; } set { _cTel = value; } }
    public int rTel { get { return _rTel; } set { _rTel = value; } }
    public int Sum { get { return _sum; } set { _sum = value; } }
    public int fear { get { return _fear; } set { _fear = value; } }
    public int fog { get { return _fog; } set { _fog = value; } }
    public int fireDam { get { return _fireDam; } set { _fireDam = value; } }
    public int slient { get { return _slient; } set { _slient = value; } }
    public int resurrect { get { return _resurrect; } set { _resurrect = value; } }
    public int amnesia { get { return _amnesia; } set { _amnesia = value; } }
    public int acquire { get { return _acquire; } set { _acquire = value; } }
    public int avoid { get { return _avoid; } set { _avoid = value; } }
    public int enhance { get { return _enhance; } set { _enhance = value; } }
    public int turn { get { return _turn; } set { _turn = value; } }
    public string NickName { get { return _NickName; } set { _NickName = value; } }
    public string comment { get { return _comment; } set { _comment = value; } }

}

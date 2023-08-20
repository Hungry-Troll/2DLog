using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iStat : MonoBehaviour
{
    [SerializeField]
    int _No;
    [SerializeField]
    string _Name;
    [SerializeField]
    int _max_hp;
    [SerializeField]
    int _max_mp;
    [SerializeField]
    int _min_attack;
    [SerializeField]
    int _max_attack;
    [SerializeField]
    int _defence;
    [SerializeField]
    int _min_magic_attack;
    [SerializeField]
    int _max_magic_attack;
    [SerializeField]
    int _fire_res;
    [SerializeField]
    int _cold_res;
    [SerializeField]
    int _earth_res;
    [SerializeField]
    int _dark_res;
    [SerializeField]
    int _poison_res;
    [SerializeField]
    int _accuracy;
    [SerializeField]
    int _avoid;
    [SerializeField]
    int _enhance;
    [SerializeField]
    int _str_limit;
    [SerializeField]
    int _dex_limit;
    [SerializeField]
    int _int_limit;
    [SerializeField]
    int _Hand;
    [SerializeField]
    int _enhance_limit;
    [SerializeField]
    string _NickName;
    [SerializeField]
    string _comment;

    public int No { get { return _No; } set { _No = value; } }
    public string Name { get { return _Name; } set { _Name = value; } }
    public int max_hp { get { return _max_hp; } set { _max_hp = value; } }
    public int max_mp { get { return _max_mp; } set { _max_mp = value; } }
    public int min_attack { get { return _min_attack; } set { _min_attack = value; } }
    public int max_attack { get { return _max_attack; } set { _max_attack = value; } }
    public int defence { get { return _defence; } set { _defence = value; } }
    public int min_magic_attack { get { return _min_magic_attack; } set { _min_magic_attack = value; } }
    public int max_magic_attack { get { return _max_magic_attack; } set { _max_magic_attack = value; } }
    public int fire_res { get { return _fire_res; } set { _fire_res = value; } }
    public int cold_res { get { return _cold_res; } set { _cold_res = value; } }
    public int earth_res { get { return _earth_res; } set { _earth_res = value; } }
    public int dark_res { get { return _dark_res; } set { _dark_res = value; } }
    public int poison_res { get { return _poison_res; } set { _poison_res = value; } }
    public int accuracy { get { return _accuracy; } set { _accuracy = value; } }
    public int avoid { get { return _avoid; } set { _avoid = value; } }
    public int enhance { get { return _enhance; } set { _enhance = value; } }
    public int str_limit { get { return _str_limit; } set { _str_limit = value; } }
    public int dex_limit { get { return _dex_limit; } set { _dex_limit = value; } }
    public int int_limit { get { return _int_limit; } set { _int_limit = value; } }
    public int Hand { get { return _Hand; } set { _Hand = value; } }
    public int enhance_limit { get { return _enhance_limit; } set { _enhance_limit = value; } }
    public string NickName { get { return _NickName; } set { _NickName = value; } }
    public string comment { get { return _comment; } set { _comment = value; } }

}

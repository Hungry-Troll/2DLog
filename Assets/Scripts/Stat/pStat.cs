using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pStat : MonoBehaviour
{
    [SerializeField]
    int _No;
    [SerializeField]
    string _Name;
    [SerializeField]
    int _max_hp;
    [SerializeField]
    int _current_hp;
    [SerializeField]
    int _max_mp;
    [SerializeField]
    int _current_mp;
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
    int _rage;
    [SerializeField]
    int _tree;
    [SerializeField]
    int _speed;
    [SerializeField]
    int _invisible;
    [SerializeField]
    int _gold;
    [SerializeField]
    int _Lv;
    [SerializeField]
    int _enhance;
    [SerializeField]
    int _noMove;
    [SerializeField]
    int _turn;
    [SerializeField]
    string _NickName;
    [SerializeField]
    string _comment;

    public int No { get { return _No; } set { _No = value; } }
    public string Name { get { return _Name; } set { _Name = value; } }
    public int max_hp { get { return _max_hp; } set { _max_hp = value; } }
    public int current_hp { get { return _current_hp; } set { _current_hp = value; } }
    public int max_mp { get { return _max_mp; } set { _max_mp = value; } }
    public int current_mp { get { return _current_mp; } set { _current_mp = value; } }
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
    public int rage { get { return _rage; } set { _rage = value; } }
    public int tree { get { return _tree; } set { _tree = value; } }
    public int speed { get { return _speed; } set { _speed = value; } }
    public int invisible { get { return _invisible; } set { _invisible = value; } }
    public int gold { get { return _gold; } set { _gold = value; } }
    public int Lv { get { return _Lv; } set { _Lv = value; } }
    public int enhance { get { return _enhance; } set { _enhance = value; } }
    public int noMove { get { return _noMove; } set { _noMove = value; } }
    public int turn { get { return _turn; } set { _turn = value; } }
    public string NickName { get { return _NickName; } set { _NickName = value; } }
    public string comment { get { return _comment; } set { _comment = value; } }
}

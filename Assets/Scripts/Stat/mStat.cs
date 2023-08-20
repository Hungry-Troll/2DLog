using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mStat : MonoBehaviour
{
    [SerializeField]
    int _No;
    [SerializeField]
    string _Name;
    [SerializeField]
    int _base_damage;
    [SerializeField]
    int _max_damage;
    [SerializeField]
    int _reach;
    [SerializeField]
    int _range;
    [SerializeField]
    string _type;
    [SerializeField]
    int _mp_consume;
    [SerializeField]
    string _property;
    [SerializeField]
    int _skill_level;
    [SerializeField]
    int _turn;
    [SerializeField]
    string _effect;
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
    string _NickName;
    [SerializeField]
    string _icon;
    [SerializeField]
    string _comment;



    public int No { get { return _No; } set { _No = value; } }
    public string Name { get { return _Name; } set { _Name = value; } }
    public int base_damage { get { return _base_damage; } set { _base_damage = value; } }
    public int max_damage { get { return _max_damage; } set { _max_damage = value; } }
    public int reach { get { return _reach; } set { _reach = value; } }
    public int range { get { return _range; } set { _range = value; } }
    public string type { get { return _type; } set { _type = value; } }
    public int mp_consume { get { return _mp_consume; } set { _mp_consume = value; } }
    public string property { get { return _property; } set { _property = value; } }
    public int skill_level { get { return _skill_level; } set { _skill_level = value; } }
    public int turn { get { return _turn; } set { _turn = value; } }
    public string effect { get { return _effect; } set { _effect = value; } }
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
    public string NickName { get { return _NickName; } set { _NickName = value; } }
    public string icon { get { return _icon; } set { _icon = value; } }
    public string comment { get { return _comment; } set { _comment = value; } }
}

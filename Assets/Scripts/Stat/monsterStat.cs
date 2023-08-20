using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterStat : MonoBehaviour
{
    [SerializeField]
    string _Name = null;
    [SerializeField]
    int _current_hp;
    [SerializeField]
    int _max_hp;

    //공격력은 최소 1이상
    [SerializeField]
    int _min_attack;
    [SerializeField]
    int _max_attack;

    //방어력은 0미만으로는 안됨
    [SerializeField]
    int _defence;

    //마법공격력은 최소 1이상
    [SerializeField]
    int _min_magic_attack;
    [SerializeField]
    int _max_magic_attack;

    //마법 저항력은 3단계가 맥스
    [SerializeField]
    int _fire_res;
    [SerializeField]
    int _cold_res;
    [SerializeField]
    int _earth_res;
    [SerializeField]
    int _dark_res;

    //독저항은 1단계가 맥스
    [SerializeField]
    int _poison_res;

    //경험치
    [SerializeField]
    int _exp;

    //스킬확률
    [SerializeField]
    int _skill;

    //회피율
    [SerializeField]
    int _avoid;
    //명중률
    [SerializeField]
    int _accuracy;

    public string Name { get { return _Name; } }
    public int CurrentHp 
    { 
        get 
        {
            if (_current_hp < 0)
                return 0;
            if (_current_hp > _max_hp)
                _current_hp = _max_hp;

            return _current_hp; 
        } 
        set 
        { _current_hp = value; } 
    }
    public int MaxHp { get { return _max_hp;} set { _max_hp = value; }}
    public int MinAttack 
    { 
        get 
        {
            if (_min_attack < 0)
                return 1;
            return _min_attack; 
        } 
        set { _min_attack = value; } 
    }
    public int MaxAttack 
    { 
        get 
        {
            if (_max_attack <= 0)
                return 1;
            return _max_attack; 
        } 
        set 
        { _max_attack = value; }
    }
    public int Defence 
    { 
        get 
        {
            if (_defence < 0)
                return 0;
            return _defence; 
        } 
        set { _defence = value; } 
    }
    public int MinMagicAttack 
    { 
        get 
        {
            if (_min_magic_attack <= 0)
                return 1;
            return _min_magic_attack; 
        } 
        set 
        { _min_magic_attack = value; } 
    }
    public int MaxMagicAttack 
    { 
        get 
        {
            if (_max_magic_attack <= 0)
                return 1;
            return _max_magic_attack; 
        } 
        set 
        { _max_magic_attack = value; } 
    }
    public int FireRes 
    { 
        get 
        {
            if (_fire_res >= 3)
                return 3;
            return _fire_res; 
        } 
        set 
        {  _fire_res = value; } 
    }
    public int ColdRes 
    { 
        get 
        {
            if (_cold_res >= 3)
                return 3;
            return _cold_res; 
        } 
        set 
        { _cold_res = value; } 
    }
    public int EarthRes 
    { 
        get 
        {
            if (_earth_res >= 3)
                return 3;
            return _earth_res; 
        } 
        set 
        { _earth_res = value; } 
    }
    public int DarkRes 
    { 
        get 
        {
            if (_dark_res >= 3)
                return 3;
            return _dark_res; 
        } 
        set 
        { _dark_res = value; } 
    }
    public int PoisonRes 
    {
        get 
        {
            if (_poison_res >= 1)
                return 1;
            return _poison_res; 
        } 
        set { _poison_res = value; } 
    }
    public int Exp { get { return _exp; } }
    public int Skill { get { return _skill; } set { _skill = value; } }
    public int Avoid { get { return _avoid; } set { _avoid = value; } }
    public int Accuracy { get { return _accuracy; } set { _accuracy = value; } }

}

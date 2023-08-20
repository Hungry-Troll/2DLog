using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStat : monsterStat
{
    // 스텟은 추수 컨텐츠에 따라 수정 계속 해야 됨 ex 스킬레벨

    [SerializeField]
    int _level;
    [SerializeField]
    int _gold;
    [SerializeField]
    int _current_exp;
    [SerializeField]
    int _next_exp;
    [SerializeField]
    int _current_mp;
    [SerializeField]
    int _max_mp;
    [SerializeField]
    int _str;
    [SerializeField]
    int _dex;
    [SerializeField]
    int _int;


    // 스킬레벨
    [SerializeField]
    int _axe_skill;
    [SerializeField]
    int _sword_skill;
    [SerializeField]
    int _mace_skill;
    [SerializeField]
    int _bow_skill;
    [SerializeField]
    int _spear_skill;
    [SerializeField]
    int _fire_skill;
    [SerializeField]
    int _earth_skill;
    [SerializeField]
    int _ice_skill;
    [SerializeField]
    int _poison_skill;
    [SerializeField]
    int _alchemy_skill;
    [SerializeField]
    int _summon_skill;
    [SerializeField]
    int _necro_skill;
    [SerializeField]
    int _avoid_skill;
    [SerializeField]
    int _attack_skill;
    [SerializeField]
    int _defence_skill;


    public int Level { get { return _level; } set { _level = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }
    public int NextExp { get { return _next_exp; } set { _next_exp = value; } }
    public int CurrentExp { get { return _current_exp; } set { _current_exp = value; } }
    public int CurrentMp 
    { 
        get 
        {
            if (_current_mp < 0)
                return 0;
            return _current_mp; 
        } 
        set { _current_mp = value; } 
    }
    public int MaxMp
    {
        get
        {
            if (_max_mp < 0)
                _max_mp = 0;
            return _max_mp;
        }
        set { _max_mp = value; }
    }
    public int Str { get { return _str; } set { _str = value; } }
    public int Dex { get { return _dex; } set { _dex = value; } }
    public int Int { get { return _int; } set { _int = value; } }

    //스킬레벨 
    public int AxeSkill { get { return _axe_skill; } set { _axe_skill = value; } }
    public int SwordSkill { get { return _sword_skill; } set { _sword_skill = value; } }
    public int MaceSkill { get { return _mace_skill; } set { _mace_skill = value; } }
    public int BowSkill { get { return _bow_skill; } set { _bow_skill = value; } }
    public int SpearSkill { get { return _spear_skill; } set { _spear_skill = value; } }
    public int FireSkill { get { return _fire_skill; } set { _fire_skill = value; } }
    public int IceSkill { get { return _ice_skill; } set { _ice_skill = value; } }
    public int PoisonSkill { get { return _poison_skill; } set { _poison_skill = value; } }
    public int AlchemySkill { get { return _alchemy_skill; } set { _alchemy_skill = value; } }
    public int SummonSkill { get { return _summon_skill; } set { _summon_skill = value; } }
    public int NecroSkill { get { return _necro_skill; } set { _necro_skill = value; } }
    public int AvoidSkill { get { return _avoid_skill; } set { _avoid_skill = value; } }
    public int AttackSkill { get { return _attack_skill; } set { _attack_skill = value; } }
    public int DefenceSkill { get { return _defence_skill; } set { _defence_skill = value; } }

}

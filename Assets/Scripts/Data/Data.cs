using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region PlayerStat
[Serializable]
public class PlayerStat
{
    public int level;
    public int hp;
    public int attack;
}

[Serializable]
public class PlayerStatData : ILoader<int, PlayerStat>
{
    public List<PlayerStat> stats = new List<PlayerStat>();

    public Dictionary<int, PlayerStat> MakeDict()
    {
        Dictionary<int, PlayerStat> dict = new Dictionary<int, PlayerStat>();

        foreach (PlayerStat stat in stats)
            dict.Add(stat.level, stat);

        return dict;
    }
}
#endregion

[Serializable]
public class MonsterStat
{
    public string name;
    public int hp;
    public int attack;
}

[Serializable]
public class MonsterStatData : ILoader<string, MonsterStat>
{
    public List<MonsterStat> monster = new List<MonsterStat>();

    public Dictionary<string, MonsterStat> MakeDict()
    {
        Dictionary<string, MonsterStat> dict = new Dictionary<string, MonsterStat>();

        foreach (MonsterStat stat in monster)
            dict.Add(stat.name, stat);

        return dict;
    }
}


[Serializable]
public class ItemStat
{
    public int _No;
    public string _Name;
    public int _max_hp;
    public int _max_mp;
    public int _min_attack;
    public int _max_attack;
    public int _defence;
    public int _min_magic_attack;
    public int _max_magic_attack;
    public int _fire_res;
    public int _cold_res;
    public int _earth_res;
    public int _dark_res;
    public int _poison_res;
    public int _accuracy;
    public int _avoid;
    public int _enhance;
    public int _str_limit;
    public int _dex_limit;
    public int _int_limit;
    public int _Hand;
    public int _enhance_limit;
    public string _NickName;
    public string _comment;
}// 상속으로 넘겨줌 나머지는 구분용

[Serializable]
public class ItemStatData : ILoader<int, ItemStat>
{
    public List<ItemStat> itemData = new List<ItemStat>();

    public Dictionary<int, ItemStat> MakeDict()
    {
        Dictionary<int, ItemStat> dict = new Dictionary<int, ItemStat>();

        foreach (ItemStat stat in itemData)
            dict.Add(stat._No, stat);

        return dict;
    }
}

[Serializable]
public class PotionStat : ItemStat
{
    public int _current_hp;
    public int _current_mp;
    public int _rage;
    public int _tree;
    public int _speed;
    public int _invisible;
    public int _gold;
    public int _Lv;
    public int _noMove;
    public int _turn;
}
[Serializable]
public class PotionStatData : ILoader<int, PotionStat>
{
    public List<PotionStat> itemData = new List<PotionStat>();

    public Dictionary<int, PotionStat> MakeDict()
    {
        Dictionary<int, PotionStat> dict = new Dictionary<int, PotionStat>();

        foreach (PotionStat stat in itemData)
            dict.Add(stat._No, stat);

        return dict;
    }
}

[Serializable]
public class ScrollStat
{
    public int _No;
    public string _Name;
    public int _cTel;
    public int _rTel;
    public int _sum;
    public int _fear;
    public int _fog;
    public int _fireDam;
    public int _slient;
    public int _resurrect;
    public int _amnesia;
    public int _acquire;
    public int _avoid;
    public int _enhance;
    public int _turn;
    public string _NickName;
    public string _comment;
}
[Serializable]
public class ScrollStatData : ILoader<int, ScrollStat>
{
    public List<ScrollStat> itemData = new List<ScrollStat>();

    public Dictionary<int, ScrollStat> MakeDict()
    {
        Dictionary<int, ScrollStat> dict = new Dictionary<int, ScrollStat>();

        foreach (ScrollStat stat in itemData)
            dict.Add(stat._No, stat);

        return dict;
    }
}

[Serializable]
public class MagicStat
{
    public int _No;
    public string _Name;
    public int _base_damage;
    public int _max_damage;
    public int _reach;
    public int _range;
    public string _type;
    public int _mp_consume;
    public string _property;
    public int _skill_level;
    public int _turn;
    public string _effect;
    public int _max_hp;
    public int _max_mp;
    public int _min_attack;
    public int _max_attack;
    public int _defence;
    public int _min_magic_attack;
    public int _max_magic_attack;
    public int _fire_res;
    public int _cold_res;
    public int _earth_res;
    public int _dark_res;
    public int _poison_res;
    public int _accuracy;
    public int _avoid;
    public string _NickName;
    public string _icon;
    public string _comment;
}

[Serializable]
public class MagicStatData : ILoader<int, MagicStat>
{
    public List<MagicStat> magicData = new List<MagicStat>();

    public Dictionary<int, MagicStat> MakeDict()
    {
        Dictionary<int, MagicStat> dict = new Dictionary<int, MagicStat>();

        foreach (MagicStat stat in magicData)
            dict.Add(stat._No, stat);

        return dict;
    }
}


[Serializable]
public class EtcStat
{
    public int _No;
    public string _Name;
    public int _sum;
    public int _magic;
    public int _gold;
    public string _NickName;
    public string _comment;
}

[Serializable]
public class EtcStatData : ILoader<int, EtcStat>
{
    public List<EtcStat> etcData = new List<EtcStat>();

    public Dictionary<int, EtcStat> MakeDict()
    {
        Dictionary<int, EtcStat> dict = new Dictionary<int, EtcStat>();

        foreach (EtcStat stat in etcData)
            dict.Add(stat._No, stat);

        return dict;
    }
}

//이하 드랍테이블

[Serializable]
public class ItemTable
{
    public string _No;
    public int _startNum;
    public int _endNum;
}

[Serializable]
public class ItemTableData : ILoader<string, ItemTable>
{
    public List<ItemTable> itemTable = new List<ItemTable>();

    public Dictionary<string, ItemTable> MakeDict()
    {
        Dictionary<string, ItemTable> dict = new Dictionary<string, ItemTable>();
        foreach (ItemTable stat in itemTable)
            dict.Add(stat._No, stat);

        return dict;
    }

}



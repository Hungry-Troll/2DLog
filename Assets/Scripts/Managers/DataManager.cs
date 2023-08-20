using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class Test
{

}

public class DataManager 
{
    //아이템 데이터
    public Dictionary<int, PlayerStat> PlayerStatDict { get; private set; } = new Dictionary<int, PlayerStat>();
    public Dictionary<string, MonsterStat> MonsterStatDict { get; private set; } = new Dictionary<string, MonsterStat>();
    public Dictionary<int, ItemStat> amuletStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> amulet_randartStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> axeStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> axe_randartStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> armorStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> armor_randartStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> bootStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> bowStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> bow_randartStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> gloveStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> helmetStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> helmet_randartStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> maceStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> mace_randartStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> ringStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> ring_randartStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> robeStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> robe_randartStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> shieldStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> spearStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> spear_randartStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> staffStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> swordStatDict { get; private set; } = new Dictionary<int, ItemStat>();
    public Dictionary<int, ItemStat> sword_randartStatDict { get; private set; } = new Dictionary<int, ItemStat>();

    public Dictionary<int, PotionStat> potionStatDict { get; private set; } = new Dictionary<int, PotionStat>();
    public Dictionary<int, ScrollStat> scrollStatDict { get; private set; } = new Dictionary<int, ScrollStat>();
    public Dictionary<int, MagicStat> magicStatDict { get; private set; } = new Dictionary<int, MagicStat>();
    public Dictionary<int, EtcStat> etcStatDict { get; private set; } = new Dictionary<int, EtcStat>();

    //드랍 테이블
    public Dictionary<string, ItemTable> amulet_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> armor_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> axe_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> boot_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> bow_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> etc_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> glove_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> helmet_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> mace_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> magic_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> potion_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> ring_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> robe_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> scroll_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> shield_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> spear_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> staff_TableDict { get; private set; } = new Dictionary<string, ItemTable>();
    public Dictionary<string, ItemTable> sword_TableDict { get; private set; } = new Dictionary<string, ItemTable>();




    //public Dictionary<int, PlayerStat> TestDict = new Dictionary<int, PlayerStat>();
    


    public void Init()
    {
        //아이템
        PlayerStatDict = LoadJson<PlayerStatData, int, PlayerStat>("Player/PlayerStatData").MakeDict();
        MonsterStatDict = LoadJson<MonsterStatData, string, MonsterStat>("Monster/MonsterStatData").MakeDict();
        amuletStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/amulet").MakeDict();
        amulet_randartStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/amulet_randart").MakeDict();
        armorStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/armor").MakeDict();
        armor_randartStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/armor_randart").MakeDict();
        axeStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/axe").MakeDict();
        axe_randartStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/axe_randart").MakeDict();
        bootStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/boot").MakeDict();
        bowStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/bow").MakeDict();
        bow_randartStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/bow_randart").MakeDict();
        gloveStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/glove").MakeDict();
        helmetStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/helmet").MakeDict();
        helmet_randartStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/helmet_randart").MakeDict();
        maceStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/mace").MakeDict();
        mace_randartStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/mace_randart").MakeDict();
        ringStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/ring").MakeDict();
        ring_randartStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/ring_randart").MakeDict();
        robeStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/robe").MakeDict();
        robe_randartStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/robe_randart").MakeDict();
        shieldStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/shield").MakeDict();
        spearStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/spear").MakeDict();
        spear_randartStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/spear_randart").MakeDict();
        staffStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/staff").MakeDict();
        swordStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/sword").MakeDict();
        sword_randartStatDict = LoadJson<ItemStatData, int, ItemStat>("Item/sword_randart").MakeDict();
        potionStatDict = LoadJson<PotionStatData, int, PotionStat>("Item/potion").MakeDict();
        scrollStatDict = LoadJson<ScrollStatData, int, ScrollStat>("Item/scroll").MakeDict();
        magicStatDict = LoadJson<MagicStatData, int, MagicStat>("Item/magic").MakeDict();
        etcStatDict = LoadJson<EtcStatData, int, EtcStat>("Item/etc").MakeDict();

        //드랍테이블
        amulet_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/amulet_table").MakeDict();
        armor_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/armor_table").MakeDict();
        axe_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/axe_table").MakeDict();
        boot_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/boot_table").MakeDict();
        bow_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/bow_table").MakeDict();
        etc_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/etc_table").MakeDict();
        glove_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/glove_table").MakeDict();
        helmet_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/helmet_table").MakeDict();
        mace_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/mace_table").MakeDict();
        magic_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/magic_table").MakeDict();
        potion_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/potion_table").MakeDict();
        ring_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/ring_table").MakeDict();
        robe_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/robe_table").MakeDict();
        scroll_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/scroll_table").MakeDict();
        shield_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/shield_table").MakeDict();
        spear_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/spear_table").MakeDict();
        staff_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/staff_table").MakeDict();
        sword_TableDict = LoadJson<ItemTableData, string, ItemTable>("itemTable/sword_table").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = GameManager.Resouce.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}

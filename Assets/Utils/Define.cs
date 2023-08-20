using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    
    public int Max100 = 100;

    public enum ItemGrade
    {
        FickArti,
        RanArti,
        NoArti,
    }
    //아이템을 선택할 때 사용 예정
    public enum ItemList
    {
        Axe,
        Armor,
        Amulet,
        Boot,
        Bow,
        Etc,
        Glove,
        Helmet,
        Mace,
        Ring,
        Robe,
        Shield,
        Spear,
        Staff,
        Sword,
        Magic,
        Potion,
        Scroll,
        None,
    }

    public enum MapControll
    {
        SumMonster,
        SumPlayerStairUp,
        SumPlayerStairDown,
        SumItem,

    }

    public enum SkillList
    {
        Attack,
        Arrow,
        None,
    }

    public enum MonsterIdleState
    {
        Patrol,
        Follow,
        None,
    }

    public enum CreatureState
    {
        Idle,
        Moving,
        Skill,
        Dead,
        None,
    }

    public enum MoveDir
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }
    
    public enum UIEvent
    {
        Click,
        Drag,
    }
}

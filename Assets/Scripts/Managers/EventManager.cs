using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EventManager 
{
    // Input 
    public Action UpButton;
    public Action DownButton;
    public Action LeftButton;
    public Action RightButton;
    // Input Skill
    public Action Skill_01;


    //턴 관련
    public Action EndTurnP;
    public Action EndTurnM;

    //플레이어 관련
    public Action AttackP;
    //몬스터 관련
    public Action AttackM;


    // Input
    
    public void InputDir(MoveDir inputDir)
    {
        switch (inputDir)
        {
            case MoveDir.Up:
                UpButton();
                break;
            case MoveDir.Down:
                DownButton();
                break;
            case MoveDir.Left:
                LeftButton();
                break;
            case MoveDir.Right:
                RightButton();
                break;
        }
    }

/*    public void PlayerUp() { UpButton(); }
    public void PlayerDown() { DownButton(); }
    public void PlayerLeft() { LeftButton(); }
    public void PlayerRight() { RightButton(); }*/

    public void PlayerSkill_01() { Skill_01(); }

    //턴 관련
    public void PlayerEndTurn() { EndTurnP(); }
    public void MonsterEndTurn() { EndTurnM(); }


    //몬스터 관련
    public void MonsterAttack() { AttackM(); }
    //플레이어 관련
    public void PlayerAttack() { AttackP(); }
}

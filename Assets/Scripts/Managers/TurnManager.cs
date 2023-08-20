using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TurnManager
{


    public enum Turn
    {
        EnemyTurn,
        PlayerTurn,
    }

    public Turn turn = Turn.PlayerTurn;

    public int NowTurn = 0;

    public void Init()
    {
        GameManager.evt.EndTurnP += MonsterTurnCheck;
        GameManager.evt.EndTurnM += PlayerTurnCheck;
    }


    public void PlayerTurnCheck()
    {
        GameManager.TurnM.turn = Turn.PlayerTurn;
        NowTurn++;
    }

    public void MonsterTurnCheck()
    {
        GameManager.TurnM.turn = Turn.EnemyTurn;
        GameManager.Obj._checkPath.Clear();
    }
}

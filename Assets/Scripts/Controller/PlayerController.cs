using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class PlayerController : CreatureController
{

    IEnumerator IEStartSkill;
    Coroutine _coStartSkill;
    


    Coroutine _coSkill;
    Coroutine _coArrow;

    playerStat ps;

    //int Level;

    protected override void Init()
    {
        GameManager.evt.UpButton += Up;
        GameManager.evt.DownButton += Down;
        GameManager.evt.LeftButton += Left;
        GameManager.evt.RightButton += Right;
        GameManager.evt.Skill_01 += Skill01;

        base.Init();

        //나중에 레벨에 따라서 불러오는 값을 변경해야 될 수 있음 Init에 해야되나??
        PlayerStat psDict = GameManager.Data.PlayerStatDict[1];
        ps = GetComponent<playerStat>();
        ps.MaxHp = psDict.hp;
        ps.CurrentHp = ps.MaxHp;
        ps.MinAttack = psDict.attack;
    }
    protected override void UpdateController()
    {
        switch (State)
        {
            case CreatureState.Idle:
                GetDirInput();
                break;
            case CreatureState.Moving:
                //GetDirInput();
                break;
            case CreatureState.Skill:
                break;
            case CreatureState.Dead:
                break;
        }
        base.UpdateController();
    }


    protected override void UpdateAnimation()
    {
        if (State == CreatureState.Idle)
        {
            _animator.Play("idle");
            #region 나중에 4방향 전부 사용하면 그때 사용

            /*            switch (_lastDir)
                        {
                            case MoveDir.Up:
                                _animator.Play("idle");
                                break;
                            case MoveDir.Down:
                                _animator.Play("idle");
                                break;
                            case MoveDir.Left:
                                _animator.Play("idle");
                                break;
                            case MoveDir.Right:
                                _animator.Play("idle");
                                break;
                        }*/
            #endregion
        }
        else if (State == CreatureState.Moving)
        {
            switch (Dir)
            {
                case MoveDir.Up:
                    _animator.Play("Run");
                    break;
                case MoveDir.Down:
                    _animator.Play("Run");
                    break;
                case MoveDir.Left:
                    _spriteRenderer.flipX = true;
                    _animator.Play("Run");
                    break;
                case MoveDir.Right:
                    _spriteRenderer.flipX = false;
                    _animator.Play("Run");
                    break;
            }
        }
        else if (State == CreatureState.Skill)
        {
            switch (SkillDir)
            {
                case MoveDir.Up:
                    _animator.Play("Attack");
                    break;
                case MoveDir.Down:
                    _animator.Play("Attack");
                    break;
                case MoveDir.Left:
                    _spriteRenderer.flipX = true;
                    _animator.Play("Attack");
                    break;
                case MoveDir.Right:
                    _spriteRenderer.flipX = false;
                    _animator.Play("Attack");
                    break;
                case MoveDir.None:
                    break;
            }
        }
        else
        {

        }
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y -1.5f, -10);
    }

    protected override void UpdateIdle()
    {
        //이동 상태로 갈지 확인
        if (Dir != MoveDir.None)
        {
            State = CreatureState.Moving;
            return;
        }

        //스킬 상태로 갈지 확인
/*
        switch (skillList)
        {
            case SkillList.None:
                break;
            case SkillList.Attack:
                SkillAttack();
                break;
            case SkillList.Arrow:
                ArrowAttack();
                break;
        }*/
    }

    public void GetDirInput()
    {
        //플레이어 턴인지 체크
        if (GameManager.TurnM.turn != TurnManager.Turn.PlayerTurn)
            return;

        //Debug.Log("방향키 버튼 입력받음");
        switch (dir)
        {
            case MoveDir.Up:
                Dir = MoveDir.Up;
                break;
            case MoveDir.Down:
                Dir = MoveDir.Down;
                break;
            case MoveDir.Left:
                Dir = MoveDir.Left;
                break;
            case MoveDir.Right:
                Dir = MoveDir.Right;
                break;
        }
    }

    protected override void UpdateMoving()
    {
        Vector3 destPos = GameManager.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
        Vector3 moveDir = destPos - transform.position;

        //도착여부
        float dist = moveDir.magnitude;
        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            MoveToNextPos();

            Dir = MoveDir.None;
            dir = MoveDir.None;
            PlayerEndTurn();
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            State = CreatureState.Moving;
        }

    }

    protected override void UpdateSkill()
    {
        GetSkillCheck();
    }


    public void SkillAttack()
    {
        //State = CreatureState.Skill;


        IEStartSkill = CoStartSkill();
        if (_coStartSkill == null)
            _coStartSkill = StartCoroutine(IEStartSkill);

    }
    public void ArrowAttack()
    {
        State = CreatureState.Skill;
        _coArrow = StartCoroutine("CoStartArrow");
    }
    IEnumerator CoStartSkill()
    {
        // 피격판정
        GameObject go = GameManager.Obj.Find(GetFrontCellPos());
        if (go != null)
        {
            playerStat ps = GetComponent<playerStat>();
            int damage = UnityEngine.Random.Range(ps.MinAttack, ps.MaxAttack + 1);
            MonsterController mc = go.GetComponent<MonsterController>();
            mc.OnDamaged(damage);
        }
        // 대기시간
        PlayerEndTurn();
        yield return new WaitForSeconds(0.5f);
        Dir = MoveDir.None;
        State = CreatureState.Idle;
        skillList = SkillList.None;
        _coStartSkill = null;
    }

    public void OnHitEvent()
    {
        //효과음 재생
    }

    public override void OnDamaged(int damage)
    {
        playerStat ps = GetComponent<playerStat>();
        ps.CurrentHp -= damage;
        Debug.Log("플레이어 hp :" + ps.CurrentHp);
    }

    IEnumerator CoStartArrow()
    {
        GameObject go = GameManager.Resouce.Instantiate("Effect/Arrow");
        ArrowController ac = go.GetComponent<ArrowController>();
        ac.Dir = _lastDir;
        ac.CellPos = CellPos;

        //대기시간

        yield return new WaitForSeconds(1.0f);
        //ac.Dir = MoveDir.None;
        _coArrow = null;
        State = CreatureState.Idle;
        skillList = SkillList.None;
    }
    public void PlayerEndTurn()
    {
        GameManager.TurnM.MonsterTurnCheck();
    }

    public void AttackOrMove()
    {
        if (GameManager.Obj.Find(GetFrontCellPos()) != null)
        {
            State = CreatureState.Skill;
            skillList = SkillList.Attack;
        }
        else
        {
            dir = SkillDir;
            skillList = SkillList.None;
        }
    }
    public void GetSkillCheck()
    {
        switch (skillList)
        {
            case SkillList.None:
                break;
            case SkillList.Attack:
                SkillAttack();
                break;
            case SkillList.Arrow:
                ArrowAttack();
                break;
        }
    }
    public void Up() 
    {
        //플레이어 턴인지 체크
        if (GameManager.TurnM.turn != TurnManager.Turn.PlayerTurn)
            return;

        SkillDir = MoveDir.Up;
        AttackOrMove();
        
    }
    public void Down()
    {
        //플레이어 턴인지 체크
        if (GameManager.TurnM.turn != TurnManager.Turn.PlayerTurn)
            return;

        SkillDir = MoveDir.Down;
        AttackOrMove();
        
    }
    public void Left() 
    {
        //플레이어 턴인지 체크
        if (GameManager.TurnM.turn != TurnManager.Turn.PlayerTurn)
            return;

        SkillDir = MoveDir.Left;
        AttackOrMove();
        
    }
    public void Right()
    {
        //플레이어 턴인지 체크
        if (GameManager.TurnM.turn != TurnManager.Turn.PlayerTurn)
            return;

        SkillDir = MoveDir.Right;
        AttackOrMove();
        
    }


    public void Skill01()
    {
        skillList = SkillList.Arrow;
    }


}

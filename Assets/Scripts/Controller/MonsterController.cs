using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : CreatureController
{

    //MonsterIdleState _monIdleState = MonsterIdleState.None;
    Coroutine _coMonsterEndTurn;
    IEnumerator IEMonsterEndTurn;

    Coroutine _coPatrol;
    IEnumerator IEPatrol;


    Coroutine _coSearch;
    IEnumerator IESearch;
    //bool FindTarget;

    Coroutine _coStartSkill;
    IEnumerator IEStartSkill;

    [SerializeField]
    List<Vector3Int> path;

    [SerializeField]
    Vector3Int _destCellPos;


    [SerializeField]
    float _serchRage = 5.0f; //몬스터 서칭 범위

    monsterStat ms;

    protected override void Init()
    {
        base.Init();

        State = CreatureState.Idle;

        Dir = MoveDir.None;
        _speed = 3.0f;

        //스텟 초기값 대입
        //string myName = gameObject.name; //이름으로 json 파일에서 불러옴
        MonsterStat msDict = GameManager.Data.MonsterStatDict["Orc"];
        ms = GetComponent<monsterStat>();
        ms.MinAttack = msDict.attack;
        ms.MaxHp = msDict.hp;
        ms.CurrentHp = ms.MaxHp;

    }


    /*    public void MonsterTurnStart()
        {
            //몬스터 턴인지 체크
            if (GameManager.TurnM.turn != TurnManager.Turn.EnemyTurn)
                return;
            // 일부로 초기화 하지 않음. 초기화하면 업데이트에 따라서 지속적으로 서치함
            IESearch = CoSearch();
            if (_coSearch == null)
                _coSearch = StartCoroutine(IESearch);

            IEPatrol = CoPatrol();
            if (_coPatrol == null && FindTarget == false)
                _coPatrol = StartCoroutine(IEPatrol);
        }*/

    protected override void UpdateIdle()
    {
        //몬스터 턴인지 체크
        if (GameManager.TurnM.turn != TurnManager.Turn.EnemyTurn)
            return;

        string name = gameObject.name;

        base.UpdateIdle();

        SearchPlayer();
        

        
/*        // 일부로 초기화 하지 않음. 초기화하면 업데이트에 따라서 지속적으로 서치함
        IESearch = CoSearch();
        if (_coSearch == null)
            _coSearch = StartCoroutine(IESearch);

        IEPatrol = CoPatrol();
        if (_coPatrol == null && FindTarget == false)
            _coPatrol = StartCoroutine(IEPatrol);*/

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
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            State = CreatureState.Moving;
        }

    }

    protected override void MoveToNextPos()
    {

        Vector3Int nextPos = _destCellPos;
        Vector3Int moveCellDir = nextPos - CellPos;

        //애니매이션을 위해서 남겨둠, 이동 계산은 a*로 처리함
        if (moveCellDir.x > 0)
            Dir = MoveDir.Right;
        else if (moveCellDir.x < 0)
            Dir = MoveDir.Left;
        else if (moveCellDir.y > 0)
            Dir = MoveDir.Up;
        else if (moveCellDir.y < 0)
            Dir = MoveDir.Down;
        else
            Dir = MoveDir.None;


        if (GameManager.Map.CanGo(nextPos) && GameManager.Obj.Find(nextPos) == null)
        {
            CellPos = nextPos;
        }
        else
        {
            MonsterEndTurn();
            State = CreatureState.Idle;
        }

    }

    public void SearchPlayer()
    {

        _target = GameManager.Obj.Find((go) =>
        {
            PlayerController pc = go.GetComponent<PlayerController>();
            if (pc == null)
                return false;

            Vector3Int dir = (pc.CellPos - CellPos);
            if (dir.magnitude > _serchRage)
                return false;

            return true;

        });

        Vector3Int destPos = new Vector3Int(0, 0, 0);

        if (_target != null)
        {
            destPos = _target.GetComponent<CreatureController>().CellPos;
        }

        if (_target == null)
        {
            //State = CreatureState.Idle;
            MonsterPatrol();
            return;
        }

        path = GameManager.Map.FindPath(CellPos, destPos, ignoreDestCollision: true);
        if (path.Count < 2 || (_target != null && path.Count > 15) || CellPos != path[0]) //길을 못찾거나, 너무 멀경우
        {
            _target = null;
            //State = CreatureState.Idle;
            MonsterPatrol();
            return;
        }

        _destCellPos = path[1];

        //중복 좌표 체크용 계산 (중복인가 false) 뒷 코드는 없으면 공격을 안함
/*        if (GameManager.Obj.PosCheck(GameManager.Obj._checkPath, _destCellPos) == false && destPos != _destCellPos)
        {
            _target = null;
            State = CreatureState.Idle;
            GameManager.Obj.PosCheckPathAdd(_destCellPos);
            yield break;
        }*/

        GameManager.Obj.PosCheckPathAdd(_destCellPos);

        
        //첫 좌표에 몬스터가 있으면 좌표설정이 안되서 몬스터가 공격안하는 버그가 있음 수정필요
        if (destPos == _destCellPos)
        {
                skillList = SkillList.Attack;
                SkillDir = _lastDir;

                if (SkillDir == MoveDir.None && _destCellPos.x > transform.position.x)
                    SkillDir = MoveDir.Right;
                else if (SkillDir == MoveDir.None && _destCellPos.x < transform.position.x)
                    SkillDir = MoveDir.Left;
                else if (_destCellPos.x > transform.position.x )
                    SkillDir = MoveDir.Right;
                else if (_destCellPos.x < transform.position.x )
                    SkillDir = MoveDir.Left;

            State = CreatureState.Skill;
        }

        //플레이어좌표로 이동
        else
        {
            if (GameManager.Obj.PosCheck(GameManager.Obj._checkPath, _destCellPos) == false)
            {
                _target = null;
                MonsterPatrol();
                //State = CreatureState.Idle;
                return;
            }
            State = CreatureState.Moving;
            //FindTarget = true;
        }

    }


    public void MonsterPatrol()
    {
        Vector3Int randPos = CellPos;
        Vector3Int TempPos = CellPos;
/*        int waitSeconds = 1;
        yield return new WaitForSeconds(waitSeconds);*/

        int UpDownLeftRight  = Random.Range(1, 5);

        switch (UpDownLeftRight)
        {
            case 1: //Up
                randPos = CellPos + Vector3Int.up;
                break;
            case 2: //Down
                randPos = CellPos + Vector3Int.down;
                break;
            case 3: //Left
                randPos = CellPos + Vector3Int.left;
                break;
            case 4: //Right
                randPos = CellPos + Vector3Int.right;
                break;
        }

        //중복 좌표 체크용 변수저장
        GameManager.Obj.PosCheckPathAdd(randPos);

        //중복 좌표 체크용 계산 (중복인가 false)
        if (GameManager.Obj.PosCheck(GameManager.Obj._checkPath, randPos) == false)
        {
            _target = null;
            _destCellPos = TempPos;
            //State = CreatureState.Idle;
            MonsterEndTurn();
            return;

        }

        if (GameManager.Map.CanGo(randPos) && GameManager.Obj.Find(randPos) == null) //&& GameManager.Obj.PosCheck(GameManager.Obj._checkPath, _destCellPos) == false)
        {
            _destCellPos = randPos;
            State = CreatureState.Moving;
        }
        #region ARPG용
        //Arpg용
        //코루틴으로 만들것
        /*        for (int i = 0; i < 10; i++)
                {
                    int xRange = Random.Range(-5, 6);
                    int yRange = Random.Range(-5, 6);

                    Vector3Int randPos = CellPos + new Vector3Int(xRange, yRange, 0);
                    //Obj find 는 현재 위치만 계산 됨. 추후 패스 파운드에서 하나더 긁어와서 변수를 넣어 줘야 버그가 안생김
                    if (GameManager.Map.CanGo(randPos) && GameManager.Obj.Find(randPos) == null)
                    {
                        _destCellPos = randPos;
                        State = CreatureState.Moving;
                        yield break;
                    }
                }*/
        #endregion
    }

    protected override void UpdateSkill()
    {
        GetSkillCheck();
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
                //ArrowAttack();
                break;
        }
    }

    public void SkillAttack()
    {
        IEStartSkill = CoStartSkill();
        if (_coStartSkill == null)
            _coStartSkill = StartCoroutine(IEStartSkill);
    }

    IEnumerator CoStartSkill()
    {
        // 피격판정
            ms = GetComponent<monsterStat>();
        int damage = UnityEngine.Random.Range(ms.MinAttack, ms.MaxAttack + 1);
            PlayerController pc = _target.GetComponent<PlayerController>();
            pc.OnDamaged(damage);


        // 대기시간
        MonsterEndTurn();
        yield return new WaitForSeconds(0.5f);
        Dir = MoveDir.None;
        State = CreatureState.Idle;
        skillList = SkillList.None;
        _coStartSkill = null;
    }

    public void OnHitEvent()
    {
        // 효과음 재생 이펙트 재생
    }

    public override void OnDamaged(int damage)
    {
        ms = GetComponent<monsterStat>();
        ms.CurrentHp -= damage;
        Debug.Log("몬스터 hp" + ms.CurrentHp);
    }

    public void MonsterEndTurn()
    {
        GameManager.TurnM.PlayerTurnCheck();
    }

}

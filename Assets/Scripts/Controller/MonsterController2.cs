using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController2 : CreatureController
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
    List<Vector3Int> _path;

    [SerializeField]
    Vector3Int _destCellPos;


    [SerializeField]
    float _serchRage = 5.0f; //���� ��Ī ����

    monsterStat ms;

    protected override void Init()
    {
        base.Init();

        State = CreatureState.Idle;

        Dir = MoveDir.None;
        _speed = 3.0f;

        //���� �ʱⰪ ����
        //string myName = gameObject.name; //�̸����� json ���Ͽ��� �ҷ���
        MonsterStat msDict = GameManager.Data.MonsterStatDict["Orc"];
        ms = GetComponent<monsterStat>();
        ms.MinAttack = msDict.attack;
        ms.MaxHp = msDict.hp;
        ms.CurrentHp = ms.MaxHp;

    }


    /*    public void MonsterTurnStart()
        {
            //���� ������ üũ
            if (GameManager.TurnM.turn != TurnManager.Turn.EnemyTurn)
                return;
            // �Ϻη� �ʱ�ȭ ���� ����. �ʱ�ȭ�ϸ� ������Ʈ�� ���� ���������� ��ġ��
            IESearch = CoSearch();
            if (_coSearch == null)
                _coSearch = StartCoroutine(IESearch);

            IEPatrol = CoPatrol();
            if (_coPatrol == null && FindTarget == false)
                _coPatrol = StartCoroutine(IEPatrol);
        }*/

    protected override void UpdateIdle()
    {
        //���� ������ üũ
        if (GameManager.TurnM.turn != TurnManager.Turn.EnemyTurn)
            return;

        string name = gameObject.name;

        base.UpdateIdle();

        SearchPlayer();



        /*        // �Ϻη� �ʱ�ȭ ���� ����. �ʱ�ȭ�ϸ� ������Ʈ�� ���� ���������� ��ġ��
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

        //��������
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

        //�ִϸ��̼��� ���ؼ� ���ܵ�, �̵� ����� a*�� ó����
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

        _path = GameManager.Map.FindPath(CellPos, destPos, ignoreDestCollision: true);
        if (_path.Count < 2 || (_target != null && _path.Count > 15) || CellPos != _path[0]) //���� ��ã�ų�, �ʹ� �ְ��
        {
            _target = null;
            //State = CreatureState.Idle;
            MonsterPatrol();
            return;
        }

        _destCellPos = _path[1];

        //�ߺ� ��ǥ üũ�� ��� (�ߺ��ΰ� false) �� �ڵ�� ������ ������ ����
        /*        if (GameManager.Obj.PosCheck(GameManager.Obj._checkPath, _destCellPos) == false && destPos != _destCellPos)
                {
                    _target = null;
                    State = CreatureState.Idle;
                    GameManager.Obj.PosCheckPathAdd(_destCellPos);
                    yield break;
                }*/

        GameManager.Obj.PosCheckPathAdd(_destCellPos);


        //ù ��ǥ�� ���Ͱ� ������ ��ǥ������ �ȵǼ� ���Ͱ� ���ݾ��ϴ� ���װ� ���� �����ʿ�
        if (destPos == _destCellPos)
        {
            skillList = SkillList.Attack;
            SkillDir = _lastDir;

            if (SkillDir == MoveDir.None && _destCellPos.x > transform.position.x)
                SkillDir = MoveDir.Right;
            else if (SkillDir == MoveDir.None && _destCellPos.x < transform.position.x)
                SkillDir = MoveDir.Left;
            else if (_destCellPos.x > transform.position.x)
                SkillDir = MoveDir.Right;
            else if (_destCellPos.x < transform.position.x)
                SkillDir = MoveDir.Left;

            State = CreatureState.Skill;
        }

        //�÷��̾���ǥ�� �̵�
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

        int UpDownLeftRight = Random.Range(1, 5);

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

        //�ߺ� ��ǥ üũ�� ��������
        GameManager.Obj.PosCheckPathAdd(randPos);

        //�ߺ� ��ǥ üũ�� ��� (�ߺ��ΰ� false)
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
        #region ARPG��
        //Arpg��
        //�ڷ�ƾ���� �����
        /*        for (int i = 0; i < 10; i++)
                {
                    int xRange = Random.Range(-5, 6);
                    int yRange = Random.Range(-5, 6);

                    Vector3Int randPos = CellPos + new Vector3Int(xRange, yRange, 0);
                    //Obj find �� ���� ��ġ�� ��� ��. ���� �н� �Ŀ�忡�� �ϳ��� �ܾ�ͼ� ������ �־� ��� ���װ� �Ȼ���
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
        // �ǰ�����
        ms = GetComponent<monsterStat>();
        int damage = UnityEngine.Random.Range(ms.MinAttack, ms.MaxAttack + 1);
        PlayerController pc = _target.GetComponent<PlayerController>();
        pc.OnDamaged(damage);


        // ���ð�
        MonsterEndTurn();
        yield return new WaitForSeconds(0.5f);
        Dir = MoveDir.None;
        State = CreatureState.Idle;
        skillList = SkillList.None;
        _coStartSkill = null;
    }

    public void OnHitEvent()
    {
        // ȿ���� ��� ����Ʈ ���
    }

    public override void OnDamaged(int damage)
    {
        ms = GetComponent<monsterStat>();
        ms.CurrentHp -= damage;
        Debug.Log("���� hp" + ms.CurrentHp);
    }

    public void MonsterEndTurn()
    {
        GameManager.TurnM.PlayerTurnCheck();
    }

}

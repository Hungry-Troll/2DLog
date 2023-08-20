using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerControllerEx : MonoBehaviour
{
    Coroutine _moving;


    public float _speed = 3.0f;

    
    public Vector3Int CellPos { get; set; } = new Vector3Int(0, -1, 0);
    CreatureState _state = CreatureState.Idle;
    MoveDir _dir = MoveDir.None; // 방향 체크용

    public Animator _animator;
    public SpriteRenderer _spriteRenderer;

    public CreatureState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;
            _state = value;
        }
    }
    public MoveDir Dir
    {
        get { return _dir; }
        set
        {
            if (_dir == value)
                return;
            _dir = value;
        }
    }

    void Start()
    {
        init();
    }

   
    void Update()
    {
        
    }

    void init()
    {
        GameManager.evt.UpButton += Up;
        GameManager.evt.DownButton += Down;
        GameManager.evt.LeftButton += Left;
        GameManager.evt.RightButton += Right;

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 pos = GameManager.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
        transform.position = pos;
    }

    public void Up()
    {
        Dir = MoveDir.Up;
        GetDirInput();
    }
    public void Down()
    {
        Dir = MoveDir.Down;
        GetDirInput();
    }
    public void Left()
    {
        Dir = MoveDir.Left;
        GetDirInput();
    }
    public void Right()
    {
        Dir = MoveDir.Right;
        GetDirInput();
    }

    public void GetDirInput()
    {
        MonsterCheck();
        ObjectCheck();

        switch (State)
        {
            case CreatureState.Idle:
                break;
            case CreatureState.Skill:
                Skill();
                break;
            case CreatureState.Moving:
                _moving = StartCoroutine("Moving");
                break;
        }
    }

    public Vector3Int GetFrontCellPos()
    {
        Vector3Int cellPos = CellPos;

        switch (Dir)
        {
            case MoveDir.Up:
                cellPos += Vector3Int.up;
                break;
            case MoveDir.Down:
                cellPos += Vector3Int.down;
                break;
            case MoveDir.Left:
                cellPos += Vector3Int.left;
                break;
            case MoveDir.Right:
                cellPos += Vector3Int.right;
                break;
            case MoveDir.None:
                cellPos += Vector3Int.zero;
                break;
        }
        return cellPos;
    }
    public void DirCalculator()
    {

    }

    public void MonsterCheck()
    {
        GameObject go = GameManager.Obj.Find(GetFrontCellPos());
        if (go != null)
            State = CreatureState.Skill;
    }
    public void ObjectCheck()
    {
        Vector3Int destPos = CellPos;

        switch (Dir)
        {
            case MoveDir.Up:
                destPos += Vector3Int.up;
                break;
            case MoveDir.Down:
                destPos += Vector3Int.down;
                break;
            case MoveDir.Left:
                destPos += Vector3Int.left;
                break;
            case MoveDir.Right:
                destPos += Vector3Int.right;
                break;
            case MoveDir.None:
                destPos += Vector3Int.zero;
                break;
        }
        if (GameManager.Obj.Find(destPos) == null)
        {
            CellPos = destPos;
            State = CreatureState.Moving;
        }
    }


    public void Skill()
    {

    }

    IEnumerator Moving()
    {
        Vector3 destPos = GameManager.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
        Vector3 moveDir = destPos - transform.position;

        //도착여부
        float dist = moveDir.magnitude;
        while (dist > float.Epsilon)
        {
            if(dist < float.Epsilon)
            {
                break;
            }
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            State = CreatureState.Moving;
            yield return null;
        }

        State = CreatureState.Idle;

/*        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            State = CreatureState.Idle;
            Dir = MoveDir.None;
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            State = CreatureState.Moving;
        }

        yield return new WaitForSeconds(3.0f);*/
    }

   /* IEnumerator MoveObject(Vector3 end, ButtonAction buttonAction)

    {

        float sqrRemainingDistance = (transform.position - end).magnitude;
        ButtonOff();
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigid.position, end, 8f * Time.deltaTime);
            rigid.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).magnitude;
            anim.SetInteger("Run", 1);//애니메이션

            //ButtonOff();
            yield return null;
        }

        anim.SetInteger("Run", 0);

    }*/

}

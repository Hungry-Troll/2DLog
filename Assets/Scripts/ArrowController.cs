using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ArrowController : CreatureController
{

    protected override void Init()
    {

        //todo 방향

        base.Init();
    }

    protected override void UpdateAnimation() { }

    protected override void MoveToNextPos()
    {
        Vector3Int destPos = CellPos;

            switch (_dir)
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
            
            if (GameManager.Map.CanGo(destPos))
            {
                GameObject go = GameManager.Obj.Find(destPos);
                if (go == null)
                {
                    CellPos = destPos;
                    State = CreatureState.Moving;
                }
                else
                {
                    Debug.Log(go.name);
                    GameManager.Resouce.Destroy(gameObject);
                }

            }
            else
            {
                GameManager.Resouce.Destroy(gameObject);
            }
        

    }

    protected override void UpdateIdle()
    {
        //이동 상태로 갈지 확인
        if (Dir != MoveDir.None)
        {
            State = CreatureState.Moving;
            return;
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

            //Dir = MoveDir.None;
            //dir = MoveDir.None;
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            State = CreatureState.Moving;
        }
    }
}

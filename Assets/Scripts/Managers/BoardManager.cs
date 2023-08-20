using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BoardManager : MonoBehaviour
{

    enum MonsterDir
    {
        Up,
        Down,
        Left,
        Right,
        Stop,
    }
    enum MonsterActionEx
    {
        Idle,
        Move,
        Attack,
        MoveStop,
    }

    GameManager gm = GameManager.Instance;
    Vector3 end;
    SpriteRenderer[] spriteRenderer;
    MonsterDir monsterDir;
    [SerializeField]
    MonsterActionEx[] monsterActionEx;

    int floor;

    List<int> list = new List<int>();
    public GameObject[] monster;
    int [] random;
    int [] randomDir;




    void Start()
    {
        //GameManager.TurnM.A_enemyTurnEx -= MonsterMoveCoordinate;
        //GameManager.TurnM.A_enemyTurnEx += MonsterMoveCoordinate;
    }

    public void MonsterMoveCoordinate()
    {
        //초기화선언
        monster = GameObject.FindGameObjectsWithTag("Monster");
        Vector3[] monsterTr = new Vector3[monster.Length];
        Vector3[] monsterFinalTr = new Vector3[monster.Length];

        Rigidbody2D[] rigid = new Rigidbody2D[monster.Length];
        Animator[] anim = new Animator[monster.Length];
        SpriteRenderer[] spriteRenderer = new SpriteRenderer[monster.Length];
        int[] random = new int[monster.Length];
        monsterActionEx = new MonsterActionEx[monster.Length];


        int i;
        int ii;

        Transform playerTr = GameObject.FindGameObjectWithTag("Player").transform;

        for (i = 0; i < monster.Length; i++) //배열에 데이터 입력
        {
            monsterTr[i] = monster[i].transform.position;

            monsterActionEx[0] = MonsterActionEx.Move;
            monsterActionEx[1] = MonsterActionEx.Move;
            //monsterActionEx[2] = MonsterActionEx.Move;

            if (monsterActionEx[i] == MonsterActionEx.Move)
            {
/*                if (Mathf.Abs(playerTr.position.x - monsterTr[i].x) < float.Epsilon)
                {
                    if (playerTr.position.y > monsterTr[i].y)
                        random[i] = 1; //Up
                    else
                        random[i] = 2; //Down
                }
                else
                {
                    if (playerTr.position.x > monsterTr[i].x)
                        random[i] = 4; //Right
                    else
                        random[i] = 3; //Left
                }*/
                if (Mathf.Abs(playerTr.position.x - monsterTr[i].x) >= Mathf.Abs(playerTr.position.y - monsterTr[i].y))
                {
                    if (playerTr.position.x > monsterTr[i].x)
                        random[i] = 4; //Right
                    else
                        random[i] = 3; //Left
                }
                else
                {
                    if (playerTr.position.y > monsterTr[i].y)
                        random[i] = 1; //Up
                    else
                        random[i] = 2; //Down
                }
            }
            if (monsterActionEx[i] == MonsterActionEx.Idle)
            {
                random[i] = Random.Range(1, 5);
            }
            monsterTr[i] = UpDownLeftRight(random[i], monsterTr[i]); //위치체크
            monsterFinalTr[i] = monsterTr[i];
            rigid[i] = monster[i].GetComponent<Rigidbody2D>();
        }

        if (monster.Length != 1) //몬스터가 1마리이면 동일위치 계산 X
        {
            for (i = 0; i < monster.Length; i++)
            {
                for (ii = 0; ii < monster.Length; ii++)
                {
                    if (monsterFinalTr[i] == monsterFinalTr[ii] && i != ii)
                    {
                        monsterFinalTr[i] = monster[i].transform.position;
                        random[i] = 5; //None
                    }
                }
            }
        }
                //동일 위치 계산
        //방향계산
        for (i = 0; i < monster.Length; i++)
        {
            switch (random[i])
            {
                case 1: //Up
                    monsterDir = MonsterDir.Up;
                    //Up(monster[i], monsterFinalTr[i], rigid[i]);
                    break;
                case 2: //Down
                    monsterDir = MonsterDir.Down;
                    //Down(monster[i], monsterFinalTr[i], rigid[i]);
                    break;
                case 3: //Left
                    monsterDir = MonsterDir.Left;
                    //Left(monster[i], monsterFinalTr[i], rigid[i]);
                    break;
                case 4: //Right
                    monsterDir = MonsterDir.Right;
                    //Right(monster[i], monsterFinalTr[i], rigid[i]);
                    break;
                case 5: //None
                    break;
            }

            Direction(monster[i], monsterFinalTr[i], rigid[i], i);
            ResetEx(i);
        }

        StartCoroutine(MonsterEndTurn());
    }

    public void Direction(GameObject monster, Vector3 monsterFinalTr, Rigidbody2D rigid, int i)
    {
        if (GameManager.TurnM.turn == TurnManager.Turn.PlayerTurn)
            return;

        //monsterDir = MonsterDir.Up;
        Vector3 start = monster.transform.position;
        end = monsterFinalTr;
        TargetCheck(start, monsterDir, i);
        if (monsterActionEx[i] == MonsterActionEx.Attack || monsterActionEx[i] == MonsterActionEx.MoveStop)
        {
            return;
        }
        StartCoroutine(MoveObject(start, end, monster, rigid));
        //StartCoroutine(MoveObject(start, end, monster, rigid));
    }

/*    public void Up(GameObject monster, Vector3 monsterFinalTr, Rigidbody2D rigid)
    {
        if (GameManager.TurnM.turn == TurnManager.Turn.PlayerTurn)
            return;

        monsterDir = MonsterDir.Up;
        Vector3 start = monster.transform.position;
        end = monsterFinalTr;
        TargetCheck(start, monsterDir);
        if (monsterActionEx == MonsterActionEx.Attack || monsterActionEx == MonsterActionEx.MoveStop)
        {
            return;
        }
        StartCoroutine(MoveObject(start, end, monster, rigid));
        //StartCoroutine(MoveObject(start, end, monster, rigid));
    }
    public void Down(GameObject monster, Vector3 monsterFinalTr, Rigidbody2D rigid)
    {
        if (GameManager.TurnM.turn == TurnManager.Turn.PlayerTurn)
            return;

        monsterDir = MonsterDir.Down;
        Vector3 start = monster.transform.position;
        end = monsterFinalTr;
        TargetCheck(start, monsterDir);
        if (monsterActionEx == MonsterActionEx.Attack || monsterActionEx == MonsterActionEx.MoveStop)
        {
            return;
        }
        StartCoroutine(MoveObject(start, end, monster, rigid));
    }
    public void Left(GameObject monster, Vector3 monsterFinalTr, Rigidbody2D rigid)
    {
        if (GameManager.TurnM.turn == TurnManager.Turn.PlayerTurn)
            return;

        monsterDir = MonsterDir.Left;
        Vector3 start = monster.transform.position;
        end = monsterFinalTr;
        TargetCheck(start, monsterDir);
        if (monsterActionEx == MonsterActionEx.Attack || monsterActionEx == MonsterActionEx.MoveStop)
        {
            return;
        }
        StartCoroutine(MoveObject(start, end, monster, rigid));
    }
    public void Right(GameObject monster, Vector3 monsterFinalTr, Rigidbody2D rigid)
    {
        if (GameManager.TurnM.turn == TurnManager.Turn.PlayerTurn)
            return;

        monsterDir = MonsterDir.Right;
        Vector3 start = monster.transform.position;
        end = monsterFinalTr;
        TargetCheck(start, monsterDir);
        if (monsterActionEx == MonsterActionEx.Attack || monsterActionEx == MonsterActionEx.MoveStop)
        {
            return;
        }
        StartCoroutine(MoveObject(start, end, monster, rigid));
    }*/


    Vector3 vec;
    Vector3 temp;
    void TargetCheck(Vector3 start, MonsterDir monsterDir, int i)
    {
        //monsterActionEx = MonsterActionEx.Idle; //초기화

        if (monsterDir == MonsterDir.Up)
        {
            vec = Vector3.up;
            temp = new Vector3(0, 0.5f, 0);
        }
        if (monsterDir == MonsterDir.Down)
        {
            vec = Vector3.down;
            temp = new Vector3(0, -0.5f, 0);
        }
        if (monsterDir == MonsterDir.Left)
        {
            vec = Vector3.left;
            temp = new Vector3(-0.5f, 0, 0);
        }
        if (monsterDir == MonsterDir.Right)
        {
            vec = Vector3.right;
            temp = new Vector3(0.5f, 0, 0);
        }

        Vector3 ReStart = start + temp;

        Debug.DrawRay(ReStart + new Vector3(0, -0.5f, 0), vec, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(ReStart + new Vector3(0, -0.5f, 0), vec, 0.5f, LayerMask.GetMask("Player","Monster"));
        if (rayHit.collider != null)
        {
            if (rayHit.collider.tag == "Player")
            {
                Attack(i);
            }
            if (rayHit.collider.tag == "Monster")
            {
                MoveStop(i);
            }
        }

    }

    IEnumerator MoveObject(Vector3 start, Vector3 end, GameObject monster, Rigidbody2D rigid)
    {

        float sqrRemainingDistance = (start - end).magnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigid.transform.position, end, 8f * Time.deltaTime);
            rigid.MovePosition(newPosition);
            sqrRemainingDistance = (monster.transform.position - end).magnitude;
            //anim.SetInteger("Run", 1);//애니메이션
            yield return null;
         
        }
        //anim.SetInteger("Run", 0);

    }

    Vector2 UpDownLeftRight(int random, Vector2 monsterTr)
    {
        switch (random)
        {
            case 1: //Up
                monsterTr += new Vector2(0, 1);
                break;
            case 2: //Down
                monsterTr += new Vector2(0, -1);
                break;
            case 3: //Left
                monsterTr += new Vector2(-1, 0);
                break;
            case 4: //Right
                monsterTr += new Vector2(1, 0);
                break;
            case 5: //None
                break;
        }
        return monsterTr;
    }



    void Attack(int i)
    {
        if (monsterDir == MonsterDir.Down)
        {
            //anim.SetTrigger("attackDown");
        }
        if (monsterDir == MonsterDir.Up)
        {
            //anim.SetTrigger("attackUp");
        }
        if (monsterDir == MonsterDir.Right || monsterDir == MonsterDir.Left)
        {
            //anim.SetTrigger("attack");
        }
        monsterActionEx[i] = MonsterActionEx.Attack;
    }

    void MoveStop(int i)
    {
        monsterActionEx[i] = MonsterActionEx.MoveStop;
    }

    public void ResetEx(int i)
    {
        monsterActionEx[i] = MonsterActionEx.Idle;
    }





    public IEnumerator MonsterEndTurn()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.evt.MonsterEndTurn();
        //GameManager.TurnM.PlayerTurnCheck();
    }
    IEnumerator WaitMove()
    {
        yield return new WaitForSeconds(0.5f);
        //Move2();
    }


/*    public IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.TurnM.PlayerTurnCheck();
    }
*/











    void MonsterMake()
    {

        list.Add(1);
        list.Add(2);
        list.Insert(1, 999);//삽입
        list.RemoveAt(1);//데이터 위치 삭제
        list.Remove(2);//데이트 동일내용 삭제

    }
   
    void ItemMake()
    {


    }

}

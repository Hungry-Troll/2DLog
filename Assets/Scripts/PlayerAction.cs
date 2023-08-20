using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonAction
{
    Up,
    Down,
    Left,
    Right,
}

enum PlayerActionEx
{
    None,
    Attack,
}


public class PlayerAction : MonoBehaviour
{
    RaycastHit2D rayHit;
    Rigidbody2D rigid;
    Vector2 test;
    Animator anim;
    SpriteRenderer spriteRenderer;
    PlayerActionEx playerActionEx = PlayerActionEx.None;
    GameObject[] bt = new GameObject[4];
    Button[] DirButton = new Button[4];
    ButtonAction buttonAction;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); //뒤집기 할때 사용
                                                         //spriteRenderer.flipX = 조건
        bt[0] = GameObject.FindGameObjectWithTag("Up");
        bt[1] = GameObject.FindGameObjectWithTag("Down");
        bt[2] = GameObject.FindGameObjectWithTag("Left");
        bt[3] = GameObject.FindGameObjectWithTag("Right");

        int i;
        for (i = 0; i < 4; i++)
        {
            DirButton[i] = bt[i].GetComponent<Button>();
        }
        return;


    }


    void Start()
    {
        GameManager.evt.UpButton += Up;
        GameManager.evt.DownButton += Down;
        GameManager.evt.LeftButton += Left;
        GameManager.evt.RightButton += Right;
    }

    void FixedUpdate()
    {



    }

    void Update()
    {

    }




    public void Up()
    {
        if (GameManager.TurnM.turn == TurnManager.Turn.EnemyTurn)
            return;

        buttonAction = ButtonAction.Up;
        Vector2 start = transform.position;
        test = start + new Vector2(0, 1);
        TargetCheck();
        if (playerActionEx == PlayerActionEx.Attack)
        {
            StartCoroutine(ButtonOnOffWait());
            return;
        }
        StartCoroutine(MoveObject(test, buttonAction));
        StartCoroutine(EndTurn());
    }
    public void Down()
    {
        if (GameManager.TurnM.turn == TurnManager.Turn.EnemyTurn)
            return;

        buttonAction = ButtonAction.Down;
        Vector2 start = transform.position;
        test = start + new Vector2(0, -1);
        TargetCheck();
        if (playerActionEx == PlayerActionEx.Attack)
        {
            StartCoroutine(ButtonOnOffWait());
            return;
        }
        StartCoroutine(MoveObject(test, buttonAction));
        StartCoroutine(EndTurn());
    }
    public void Left()
    {
        if (GameManager.TurnM.turn == TurnManager.Turn.EnemyTurn)
            return;

        buttonAction = ButtonAction.Left;
        Vector2 start = transform.position;
        test = start + new Vector2(-1, 0);
        spriteRenderer.flipX = true;
        TargetCheck();
        if (playerActionEx == PlayerActionEx.Attack)
        {
            StartCoroutine(ButtonOnOffWait());
            return;
        }
        StartCoroutine(MoveObject(test, buttonAction));
        StartCoroutine(EndTurn());
    }

    public void Right()
    {
        if (GameManager.TurnM.turn == TurnManager.Turn.EnemyTurn)
            return;

        buttonAction = ButtonAction.Right;
        Vector2 start = transform.position;
        test = start + new Vector2(1, 0);
        spriteRenderer.flipX = false;
        TargetCheck();
        if (playerActionEx == PlayerActionEx.Attack)
        {
            StartCoroutine(ButtonOnOffWait());
            return;
        } 
        StartCoroutine(MoveObject(test, buttonAction));
        StartCoroutine(EndTurn());
    }


    void TargetCheck()
    {
        playerActionEx = PlayerActionEx.None; //playerActionEx 초기화

        if (buttonAction == ButtonAction.Up)
        {
            Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), Vector3.up, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector3.up, 1, LayerMask.GetMask("Monster"));
            if (rayHit.collider != null)
            {
                if (rayHit.collider.tag == "Monster")
                {
                    Attack();
                }
            }
        }

        if (buttonAction == ButtonAction.Down)
        {
            Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector3.down, 1, LayerMask.GetMask("Monster"));
            if (rayHit.collider != null)
            {
                if (rayHit.collider.tag == "Monster")
                {
                    Attack();
                }
            }
        }

        if (buttonAction == ButtonAction.Left)
        {
            Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), Vector3.left, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector3.left, 1, LayerMask.GetMask("Monster"));
            if (rayHit.collider != null)
            {
                if (rayHit.collider.tag == "Monster")
                {
                    Attack();
                }
            }
        }

        if (buttonAction == ButtonAction.Right)
        {
            Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), Vector3.right, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector3.right, 1, LayerMask.GetMask("Monster"));
            if (rayHit.collider != null)
            {
                if (rayHit.collider.tag == "Monster")
                {
                    Attack();
                }
            }
        }

        return;

    }

    IEnumerator MoveObject(Vector3 end, ButtonAction buttonAction)

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
        
    }



    void Attack()
    {
        if (buttonAction == ButtonAction.Down)
        {
            anim.SetTrigger("attackDown");
        }
        if (buttonAction == ButtonAction.Up)
        {
            anim.SetTrigger("attackUp");
        }
        if (buttonAction == ButtonAction.Right || buttonAction == ButtonAction.Left)
        {
            anim.SetTrigger("attack");
        }
        playerActionEx = PlayerActionEx.Attack;
    }


    public void ButtonOn()
    {
        int i;
        for (i = 0; i < 4; i++)//버튼 활성화
        {
            DirButton[i].interactable = true;
        }
    }

    public void ButtonOff()
    {
        int i;

        for (i = 0; i < 4; i++)//버튼 비활성화
        {
            DirButton[i].interactable = false;
        }
    }

    public IEnumerator ButtonOnOffWait()
    {
        ButtonOff();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(EndTurn());
    }

    public IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.evt.PlayerEndTurn();

        //GameManager.TurnM.MonsterTurnCheck();
        ButtonOn();
    }
}

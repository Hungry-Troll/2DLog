using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using DG.Tweening;

public class DungeonSelect : MonoBehaviour
{
    [SerializeField] private List<Image> _dungeonImage;
    
    private List<Define.Dungeon> _dungeonPool;
    public List<Define.Dungeon> _dungeonList;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.Init();
        _dungeonPool = new List<Define.Dungeon>(){ Dungeon.Animal,Dungeon.Base, Dungeon.Test1, Dungeon.Test2, Dungeon.Test3 };
        _dungeonList = new List<Define.Dungeon>();
        FirstSeachDungeon();

        // 인풋매니저 구독
        InputManager.Instance._LeftDragAction += LeftDungeonImage;
        InputManager.Instance._RightDragAction += RightDungeonImage;
    }

    public void FirstSeachDungeon()
    {
        int cnt = 3;
        for (int i = 0; i < cnt; i++)
        {
            var num = Random.Range(0, _dungeonPool.Count - 1);
            _dungeonList.Add(_dungeonPool[num]);
            _dungeonPool.RemoveAt(num);
        }
        SearchDungeon();
    }

    public void SearchDungeon()
    {
        string path = "Sprite/DungeonImage/";
        for (int i = 0; i < _dungeonList.Count; i++)
        {
            string spriteName = _dungeonList[i].ToString();
            var sprite = GameManager.Resouce.Load<Sprite>($"{path}{spriteName}");
            _dungeonImage[i + 1].sprite = sprite;
        }
        _dungeonImage[0].sprite = _dungeonImage[3].sprite;
        _dungeonImage[4].sprite = _dungeonImage[1].sprite;
    }

    public void LeftDungeonImage()
    {
        InputManager.Instance._UiEvent = Define.UIEvent.None;
        StartCoroutine(CoLeftDungeonImage());
    }

    IEnumerator CoLeftDungeonImage()
    {
        float rightPos = -550f;
        Vector2 targetPos = _dungeonImage[4].transform.position;

        for (int i = 0; i < _dungeonImage.Count; i++)
        {
            _dungeonImage[i].transform.DOMoveX(_dungeonImage[i].transform.position.x + rightPos, 0.5f);
            yield return null;
        }
        yield return GameManager.Yield.WaitForSecond(0.5f);
        _dungeonImage[0].transform.position = targetPos;

        Image temp = _dungeonImage[0];
        _dungeonImage.RemoveAt(0);
        _dungeonImage.Insert(4, temp);

        _dungeonImage[0].sprite = _dungeonImage[3].sprite;
        _dungeonImage[4].sprite = _dungeonImage[1].sprite;

        yield return GameManager.Yield.WaitForSecond(0.1f);
        InputManager.Instance._UiEvent = Define.UIEvent.Drag;
    }

    public void RightDungeonImage()
    {
        InputManager.Instance._UiEvent = Define.UIEvent.None;
        StartCoroutine(CoRightDungeonImage());
    }

    IEnumerator CoRightDungeonImage()
    {
        float rightPos = 550f;
        Vector2 targetPos = _dungeonImage[0].transform.position;

        for (int i = 0; i < _dungeonImage.Count; i++)
        {
            _dungeonImage[i].transform.DOMoveX(_dungeonImage[i].transform.position.x + rightPos, 0.5f);
            yield return null;
        }
        yield return GameManager.Yield.WaitForSecond(0.5f);
        _dungeonImage[4].transform.position = targetPos;

        Image temp = _dungeonImage[4];
        _dungeonImage.RemoveAt(4);
        _dungeonImage.Insert(0, temp);

        _dungeonImage[0].sprite = _dungeonImage[3].sprite;
        _dungeonImage[4].sprite = _dungeonImage[1].sprite;

        yield return GameManager.Yield.WaitForSecond(0.1f);
        InputManager.Instance._UiEvent = Define.UIEvent.Drag;

    }

    public void OnDisable()
    {
        // 구독 취소
        InputManager.Instance._LeftDragAction -= LeftDungeonImage;
        InputManager.Instance._RightDragAction -= RightDungeonImage;
    }

}


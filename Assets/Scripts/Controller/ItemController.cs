using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ItemController : CreatureController
{
    
    Vector3Int destPos;
    Vector3Int position;

    Coroutine _coDisappear;
    IEnumerator IEDisappear;

    protected override void Init()
    {
        base.Init();
        ItemController ic = GetComponent<ItemController>();
        position = ic.GetComponent<ItemController>().CellPos;
    }


    public void LateUpdate()
    {
        PlayerSearch();
    }

    protected override void UpdateController()
    {

        IEDisappear = DisappearItem();
        if (_coDisappear == null)
            _coDisappear = StartCoroutine(IEDisappear);
    }

    public void PlayerSearch()
    {
        //타겟 찾기(플레이어)

        if (_target != null)
            return;

        _target = GameManager.Obj.Find((go) =>
        {
            PlayerController pc = go.GetComponent<PlayerController>();
            if (pc == null)
                return false;

            return true;

        });

    }

    IEnumerator DisappearItem()
    {
        if (_target != null)
        {
            destPos = _target.GetComponent<CreatureController>().CellPos;
            State = CreatureState.Dead;
        }
        ////아이템 먹었을 때
        if (position == destPos)
        {
            //추후 사운드 추가 해야 됨
            GameObject effect = GameManager.Resouce.Instantiate("Effect/effect");
            effect.transform.position = position + new Vector3(0.5f, 0.5f);
            effect.GetComponent<Animator>().Play("Play");
            yield return new WaitForSeconds(0.25f);
            GameObject.Destroy(effect);
            GameManager.Obj.ItemRemove(gameObject);
            GameManager.Resouce.Destroy(gameObject);
            //GameManager.Inven
        }

    }

}



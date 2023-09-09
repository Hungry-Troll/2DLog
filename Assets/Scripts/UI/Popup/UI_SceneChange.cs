using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_SceneChange : UI_Popup
{
    [SerializeField] Image _image;

    void OnEnable()
    {
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        _image.DOFade(1.0f, 1.0f);
        yield return GameManager.Yield.WaitForSecond(2.0f);
        _image.DOFade(0.0f, 1.0f);
        yield return GameManager.Yield.WaitForSecond(1.0f);
        gameObject.SetActive(false);
    }
}

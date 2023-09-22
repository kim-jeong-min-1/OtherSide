using DG.Tweening;
using Jungmin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Tutorial : StageManager
{
    [SerializeField] private GameObject obj;
    [SerializeField] private Vector3 objPos;

    [SerializeField] private GameObject titleObj;
    [SerializeField] private Vector3 titleObjPos;

    [SerializeField] private TitleManager titleManager;

    public void StartSet()
    {
        if (GameManager.Instance.stageData.isTutorialClear == true)
        {
            if (postProcessingVolume.profile.TryGet(out colorAdjustments))
            {
                colorAdjustments.saturation.value = 0;
            }

            titleObj.SetActive(true);
            titleObj.transform.DOLocalMove(titleObjPos, 1f);
            titleObj.transform.localPosition = titleObjPos;
            titleManager.isTutorialClear = true;
            titleManager.TutorialClearTitleStart();
            obj.SetActive(false);
        }
        else
        {
            titleObj.SetActive(false);
            obj.SetActive(true);

            player1.isOn = true; player2.isOn = true;
        }
    }

    protected override void ClearCheck()
    {
        int check = 0;
        for (int i = 0; i < portal.Length; i++)
        {
            if (player1.currentNode == portal[i]) check++;
            if (player2.currentNode == portal[i]) check++;
        }

        if (check == 2)
        {
            StageClear();
            isClearStage = true;
        }
    }

    protected override void StageClear()
    {
        StartCoroutine(ClearEvent());
    }

    private IEnumerator ClearEvent()
    {
        GameManager.Instance.stageData.isTutorialClear = true;
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(StageSaturation());
        yield return new WaitForSeconds(3f);
        TitleMove();
        yield break;
    }


    private void TitleMove()
    {
        obj.transform.DOLocalMoveY(10, 1f).SetEase(Ease.InSine).OnComplete(() =>
        {
            titleObj.SetActive(true);
            titleObj.transform.DOLocalMove(titleObjPos, 2f).SetEase(Ease.InSine).OnComplete(() =>
            {
                titleManager.isTutorialClear = true;
                titleManager.TutorialClearTitleStart();
            });
        });
    }
}

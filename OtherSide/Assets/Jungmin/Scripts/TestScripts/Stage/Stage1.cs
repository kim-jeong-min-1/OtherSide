using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = ProductionEvent.Event;
using Jungmin;
using DG.Tweening;
using static Unity.VisualScripting.Metadata;
using UnityEditor;

public class Stage1 : StageManager
{
    [SerializeField] private Transform[] obj;


    private void Start() => StartCoroutine(Stage1_Start());

    protected override void ClearCheck()
    {
        if (player1.currentNode == null) return;

        var player1Check = player1.currentNode.GetComponent<Walkable>().type == WalkableType.ClearPortal;
        var player2Check = player2.currentNode.GetComponent<Walkable>().type == WalkableType.ClearPortal;

        if (player1Check && player2Check)
        {
            StageClear();
            isClearStage = true;
        }
    }

    protected override void StageClear()
    {
        StartCoroutine(Stage1ClearEvent());
    }

    private IEnumerator Stage1ClearEvent()
    {
        GameManager.Instance.stageData.clearStage[0] = true;

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear, 0.6f);
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(StageSaturation());
        //StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3f);
        setting.ClearWnd();
        yield break;
    }

    private IEnumerator CloudMove(int sign)
    {
        obj[1].transform.DOLocalMove(new Vector3(0.5f, 0, sign * 0.5f), 5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(5.2f);

        StartCoroutine(CloudMove(-sign));
        yield break;
    }


    private IEnumerator Stage1_Start()
    {
        StartCoroutine(Event.CameraMove(Camera.main, Camera.main.transform.position + Vector3.up * -6, 180f));
        SoundManager.Instance.PlayBGM(1, 0.1f);
        
        yield return new WaitForSeconds(1f);

        player1.isOn = true; player2.isOn = true;
        StartCoroutine(CloudMove(1));
    }
}

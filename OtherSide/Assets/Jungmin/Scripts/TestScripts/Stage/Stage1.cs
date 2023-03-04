using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = ProductionEvent.Event;
using Jungmin;
using DG.Tweening;

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
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear, 0.6f);
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3f);

        nextSceneName = "Stage1_1";
        GameManager.Instance.LoadStage(nextSceneName);

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

        for (int i = 0; i < 3; i++)
        {
            var v = obj[i].position;
            obj[i].DOMove(new Vector3(v.x, v.y + 30f, v.z), 2.5f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(1 + i);
            obj[i].gameObject.isStatic = true;
        }

        StartCoroutine(CloudMove(1));
    }
}

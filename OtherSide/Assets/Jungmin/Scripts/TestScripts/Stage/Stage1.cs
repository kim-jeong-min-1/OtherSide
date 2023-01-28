using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = ProductionEvent.Event;
using Jungmin;
using DG.Tweening;

public class Stage1 : StageManager
{
    [SerializeField] GameObject clouds;

    private void Start()
    {
        StartCoroutine(CloudMove(1));
        StartCoroutine(Event.CameraMove(Camera.main, Camera.main.transform.position + Vector3.up * -6, 180f));
    }

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
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear, 0.6f);
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3f);

        nextSceneName = "Last_Stage2";
        GameManager.Instance.LoadStage(nextSceneName);

        yield break;
    }

    private IEnumerator CloudMove(int sign)
    {
        clouds.transform.DOLocalMove(new Vector3(0.5f, 0, sign * 0.5f), 5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(5.2f);

        StartCoroutine(CloudMove(-sign));
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = ProductionEvent.Event;
using Jungmin;
using DG.Tweening;

public class Stage3 : StageManager
{
    [SerializeField] InteractBrain interactBrain;
    private bool isPortal = false;

    private void Start()
    {
        StartCoroutine(Event.CameraMove(Camera.main, new Vector3(19f, 15.22f, 18.59f), 180f));    
    }

    protected override void Update()
    {
        base.Update();
        if (!isPortal && player1.currentNode != null) PortalCondition();

        if (player1.currentNode == portal[0] &&
            player2.playerType == PlayerMoveType.Follow && player2.currentNode != portal[0] && player2.isWalking == false)
        {
            player1.OtherPlayerFollowMe(portal[0]);
        }
    }

    protected override void StageClear()
    {
        StartCoroutine(Stage3ClearEvent());
    }

    protected override void ClearCheck()
    {
        if(player1.currentNode == portal[0] && player2.currentNode == portal[0])
        {
            StageClear();
            isClearStage = true;
        }
    }

    private void PortalCondition()
    {
        var neighborNode = player1.currentNode.GetComponent<Walkable>().neighborNode;
        foreach (var node in neighborNode)
        {
            if (player2.currentNode == node.nodePoint && node.isActive)
            {
                StartCoroutine(PortalApeear());
                isPortal = true;
            }
        }      
    }

    private IEnumerator PortalApeear()
    {
        SoundManager.Instance.PlaySFX(SoundEffect.Vibration, 0.8f, 1, 3);
        StartCoroutine(Event.CameraShake(Camera.main, 0.5f, 3));
        StartCoroutine(Event.ObjectAppearance(portal[0].gameObject, portal[0].transform.position + Vector3.up * 12f, 3f));
        yield return new WaitForSeconds(3);

        LayerChange(portal[0], 10);
        yield break;
    }

    private IEnumerator Stage3ClearEvent()
    {
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear, 0.6f);
        yield return new WaitForSeconds(1f);
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
        LayerChange(portal[0], 0);

        StartCoroutine(Event.CameraShake(Camera.main, 0.5f, 3));
        StartCoroutine(Event.ObjectAppearance(portal[0].gameObject, portal[0].transform.position + -Vector3.up * 20f, 5f));
        SoundManager.Instance.PlaySFX(SoundEffect.Vibration, 0.8f, 1, 3.5f);
        yield return new WaitForSeconds(3f);

        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3f);

        nextSceneName = "Stage4";
        GameManager.Instance.LoadStage(nextSceneName);

        yield break;
    }
}


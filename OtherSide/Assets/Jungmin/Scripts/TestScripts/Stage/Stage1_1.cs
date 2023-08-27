using Jungmin;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Event = ProductionEvent.Event;

[System.Serializable]
public class InteractNode
{
    public int index;
    public Walkable node;
}

public class Stage1_1 : StageManager
{
    [SerializeField] private InteractNode[] interactNodes = new InteractNode[2];
    [SerializeField] private Transform interactButton1;
    [SerializeField] private Transform interactButton2;
    [SerializeField] private GameObject interactObject;
    [SerializeField] private Transform[] obj;

    private bool isInteract = false;

    private void Start() => StartCoroutine(Stage1_Start());
    protected override void Update()
    {
        base.Update();
        if(!isInteract) InteractCheck();
    }
    private void InteractCheck()
    {
        if (player1.currentNode == null) return;
        if (player1.currentNode == interactButton1 &&
            player2.currentNode == interactButton2)
        {
            isInteract = true;
            for (int i = 0; i < 2; i++) interactNodes[i].node.neighborNode[interactNodes[i].index].isActive = true;

            SoundManager.Instance.PlaySFX(SoundEffect.Vibration, 0.8f, 1, 2.5f);
            StartCoroutine(Event.CameraShake(Camera.main, 0.5f, 3));

            interactObject.transform.DORotate(new Vector3(0f, -90f, 0f), 2.5f).SetEase(Ease.OutQuad);
            interactObject.transform.DOMove(new Vector3(9.8f, -7.376f, 5.22f), 2.5f).SetEase(Ease.OutQuad);
        }
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
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear, 0.6f);
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3f);

        nextSceneName = "Stage2";
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
        //StartCoroutine(Event.CameraMove(Camera.main, Camera.main.transform.position + Vector3.up * -6, 180f));
        //yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayBGM(1, 0.1f);
        for (int i = 0; i < obj.Length; i++)
        {
            //var v = obj[i].position;
            //obj[i].DOMove(new Vector3(v.x, v.y + 30f, v.z), 2.5f).SetEase(Ease.OutQuad);
            //yield return new WaitForSeconds(1 + i);
            obj[i].gameObject.isStatic = true;
        }



        player1.isOn = true; player2.isOn = true;
        StartCoroutine(CloudMove(1));

        yield return null;
    }
}

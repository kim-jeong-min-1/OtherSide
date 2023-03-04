using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Event = ProductionEvent.Event;
using Jungmin;

[System.Serializable]
public class InteractButton
{
    public Transform button;
    public NodeData[] nodeDatas;
    public GameObject[] obj;
    public Vector3[] objPos;
    [HideInInspector] public bool isActive = false;
}

public class Stage4_1 : StageManager
{
    [SerializeField] private Transform[] startPos = new Transform[2];
    [SerializeField] private List<Vector3> cameraPos = new List<Vector3>();
    [SerializeField] private List<Transform> cameraMovePoints = new List<Transform>();

    [SerializeField] private InteractButton[] buttons = new InteractButton[3];
    private Player curPlayer;
    private uint curCameraPoint;

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
        StartCoroutine(Stage1ClearEvent());
    }
    private IEnumerator Stage1ClearEvent()
    {
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear, 0.6f);
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3f);

        nextSceneName = "Stage5";
        GameManager.Instance.LoadStage(nextSceneName);

        yield break;
    }

    private void Start()
    {
        curPlayer = player1;
        SoundManager.Instance.PlayBGM(4, 0.1f);
        StartCoroutine(PlayerAppearance(player1, 0));
        StartCoroutine(Stage4_1Update());
    }
    private void CameraMove(Player player)
    {
        for (int i = 0; i < cameraMovePoints.Count; i++)
        {
            if (player.currentNode == cameraMovePoints[i])
            {
                Camera.main.transform.DOMove(cameraPos[i], 3f);
                curCameraPoint = (uint)i;
            }
        }
    }
    private void InteractCheck()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if ((buttons[i].button == player1.currentNode ||
                buttons[i].button == player2.currentNode) && !buttons[i].isActive)
            {
                Interact(buttons[i]);
            }
        }
    }
    private void Interact(InteractButton button)
    {
        for (int i = 0; i < button.nodeDatas.Length; i++)
        {
            button.nodeDatas[i].walkable.neighborNode[button.nodeDatas[i].index].isActive
                = button.nodeDatas[i].isConnectNode;
        }

        for (int i = 0; i < button.obj.Length; i++)
            StartCoroutine(Event.ObjectAppearance(button.obj[i], button.objPos[i], 2.5f));

        SoundManager.Instance.PlaySFX(SoundEffect.Vibration, 0.8f, 1, 2.45f);
        button.isActive = true;
    }
    private IEnumerator PlayerAppearance(Player player, int index)
    {
        player.isWalking = true;
        player.transform.DOMove(startPos[index].GetComponent<Walkable>().GetWalkPoint(), 1f);
        yield return new WaitForSeconds(0.8f);
        player.isWalking = false;
    }
    private IEnumerator Stage4_1Update()
    {
        while (true)
        {
            if(curCameraPoint == 2 && curPlayer != player2)
            {
                curPlayer = player2;
                StartCoroutine(PlayerAppearance(player2, 1));
            }

            CameraMove(curPlayer);
            InteractCheck();
            yield return new WaitForSeconds(0.1f);
        }
    }
}

using DG.Tweening;
using Jungmin;
using System.Collections;
using UnityEngine;

public class Stage5_1 : StageManager
{
    [SerializeField] private Walkable[] RotateWalkPoints;
    [SerializeField] private Walkable[] EndWalkPoints;
    [SerializeField] private Walkable TeleportWalkPoint;
    [SerializeField] private Walkable ButtonWalkPoint;

    [SerializeField] private GameObject interactObject1;
    [SerializeField] private GameObject interactObject2;
    [SerializeField] private GameObject interactObject3;
    [SerializeField] private GameObject interactObject4;
    [SerializeField] private GameObject telePortDoor;

    [SerializeField] private GameObject interactRotateObject;

    [SerializeField] private Transform interactButton1;
    [SerializeField] private Transform interactButton2;
    [SerializeField] private Transform interactButton3;
    [SerializeField] private Transform[] warningNode;

    private bool isInteract1, isInteract2, isInteract3;


    protected override void Update()
    {
        base.Update();
        InteractCheck();
    }
    private void InteractCheck()
    {
        if (player2.currentNode == interactButton1 && isInteract1 == false)
        {
            //이벤트 1

            Event_1();

            isInteract1 = true;
        }

        if (player2.currentNode == interactButton2 && isInteract2 == false)
        {
            //이벤트 2

            Event_2();


            isInteract2 = true;
        }

        if ((player1.currentNode == interactButton3 || player2.currentNode == interactButton3) 
            && isInteract3 == false)
        {
            //이벤트 3

            Event_3();


            isInteract3 = true;
        }
    }

    private void Event_1() 
    {
        interactObject1.transform.DOLocalMove(new Vector3(-0.0005924702f, 1.793999f, -0.0005041531f), 2f).SetEase(Ease.OutQuad);
        interactObject2.transform.DOLocalMove(new Vector3(-0.0005922318f, 3.191765f, -0.0005038806f), 2f).SetEase(Ease.OutQuad);
        interactRotateObject.transform.DOLocalRotate(new Vector3(-90f, 90f, 0f), 2f).SetEase(Ease.OutQuad);

        RotateWalkPoints[0].neighborNode[1].isActive = false;
        RotateWalkPoints[0].neighborNode[2].isActive = true;
        
        RotateWalkPoints[1].neighborNode[1].isActive = false;
        RotateWalkPoints[1].neighborNode[2].isActive = true;


        NodeSet(1);
    }
    private void Event_2() 
    {
        interactRotateObject.transform.DOLocalRotate(new Vector3(-90f, 180f, 0f), 2f).SetEase(Ease.OutQuad);
        interactRotateObject.transform.DOLocalMove(new Vector3(0.7998778f, -0.2999988f, -0.200385f), 2f).SetEase(Ease.OutQuad);

        interactObject3.transform.DOLocalMove(new Vector3(0.0002369136f, 0.3998418f, 1.049999f), 2f).SetEase(Ease.OutQuad);
        interactObject3.transform.DOLocalRotate(Vector3.zero, 2f).SetEase(Ease.OutQuad);

        interactObject4.transform.DOLocalMove(new Vector3(-1.563194e-13f, -4.172325e-07f, 1.70299e-06f), 2f).SetEase(Ease.OutQuad); 

        telePortDoor.transform.DOLocalMove(Vector3.down * 3, 2f).SetEase(Ease.OutQuad);


        RotateWalkPoints[0].neighborNode[2].isActive = false;
        RotateWalkPoints[0].neighborNode[3].isActive = true;
        RotateWalkPoints[0].neighborNode[4].isActive = false;


        RotateWalkPoints[1].neighborNode[2].isActive = false;
        RotateWalkPoints[1].neighborNode[3].isActive = true;

       
        NodeSet(2);
    }

    private void Event_3()
    {
        WarningNodeCheck();

        interactRotateObject.transform.DOLocalRotate(new Vector3(-90f, 0f, 0f), 2f).SetEase(Ease.OutQuad);
        interactRotateObject.transform.DOLocalMove(new Vector3(0.7994872f, 1.54f, -0.2003825f), 2f).SetEase(Ease.OutQuad);

        interactObject3.transform.DOLocalMove(new Vector3(0.0001233651f, 0.349846f, 1.000116f), 2f).SetEase(Ease.OutQuad);
        interactObject3.transform.DOLocalRotate(new Vector3(-90f, 0f, -90f), 2f).SetEase(Ease.OutQuad);

        telePortDoor.transform.DOLocalMove(Vector3.zero, 2f).SetEase(Ease.OutQuad);

        RotateWalkPoints[0].neighborNode[3].isActive = false;
        RotateWalkPoints[0].neighborNode[4].isActive = true;


        NodeSet(3);

    }

    private void WarningNodeCheck()
    {
        if (player1.currentNode == warningNode[0] || player1.currentNode == warningNode[1] || player1.currentNode == warningNode[2])
        {
            player1.transform.position = TeleportWalkPoint.transform.position;
        }
        if (player2.currentNode == warningNode[0] || player2.currentNode == warningNode[1] || player2.currentNode == warningNode[2])
        {
            player2.transform.position = TeleportWalkPoint.transform.position;
        }

    }

    private void NodeSet(int num)
    {

        for (int i = 0; i < EndWalkPoints.Length; i++)
        {
            for (int j = 1; j < EndWalkPoints[i].neighborNode.Count; j++)
            {
                EndWalkPoints[i].neighborNode[j].isActive = false;
            }
        }

        switch (num)
        {
            case 1:
                EndWalkPoints[2].neighborNode[1].isActive = true;
                EndWalkPoints[3].neighborNode[1].isActive = true;
                break;
            case 2:
                EndWalkPoints[0].neighborNode[1].isActive = true;
                EndWalkPoints[3].neighborNode[2].isActive = true;

                ButtonWalkPoint.neighborNode[2].isActive = true;
                TeleportWalkPoint.neighborNode[2].isActive = true;
                break;
            case 3:
                EndWalkPoints[4].neighborNode[1].isActive = true;
                TeleportWalkPoint.neighborNode[2].isActive = false;
                break;
            default:
                print("Index Range Out");
                break;
        }
    }

    protected override void ClearCheck()
    {
        if (player1.currentNode == null) return;

        var player1Check = player1.currentNode.GetComponent<Walkable>().type == WalkableType.ClearPortal;
        var player2Check = player2.currentNode.GetComponent<Walkable>().type == WalkableType.ClearPortal;

        if (player1Check)
        {
            player1.gameObject.SetActive(false);
        }

        if (player2Check)
        {
            player2.gameObject.SetActive(false);
        }

        if (player1Check && player2Check)
        {
            StageClear();
            isClearStage = true;
        }
    }

    protected override void StageClear()
    {
        StartCoroutine(StageClearEvent());
    }

    private IEnumerator StageClearEvent()
    {
        GameManager.Instance.stageData.clearStage[9] = true;

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear, 0.6f);
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(StageSaturation());
        yield return new WaitForSeconds(3f);
        GameManager.Instance.LoadStage("Title");
        yield break;
    }
}

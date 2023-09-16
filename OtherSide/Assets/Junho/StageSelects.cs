using System.Collections;
using UnityEngine;
using DG.Tweening;
using Jungmin;
using Unity.VisualScripting;

public class StageSelects : MonoBehaviour
{
    [SerializeField] private Material bg;

    [SerializeField] private StageDatas data;
    [SerializeField] private Vector3[] pos;
    [SerializeField] private Material[] btnMaterials;
    [SerializeField] private MeshRenderer[] btnMesh;
    [SerializeField] private GameObject[] btnLineObjs;

    [SerializeField] private float scrollSpd;
    [SerializeField] private float rotateSpd;
    private int isStage;
    bool isScroll;
    private Vector2 startPos;


    private void OnEnable()
    {
        isStage = data.lastPlayStage;


        int ClearStageNum = 0;

        foreach (var stage in data.clearStage) 
        {
            if(stage == true) ClearStageNum++;
        }

        for (int i = 0; i < ClearStageNum; i++)
        {
            btnMesh[i].material = btnMaterials[i];
        }

        StartCoroutine(BGFade());
    }

    private IEnumerator BGFade()
    {
        isScroll = true;

        float t = 0;
        Color colorA = Color.black;

        while (t < 0.5f)
        {
            yield return null;

            t += Time.deltaTime;

            colorA.a = Mathf.Lerp(0f, 0.85f, t / 0.5f);

            bg.color = colorA;
        }

    }


    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        transform.DOMove(pos[isStage], 2f).SetEase(Ease.OutSine).OnComplete(()=> isScroll = false);
    }
    private void Update()
    {
        if (isScroll == true) return;

        if (Input.GetMouseButtonDown(0)) 
        {
            startPos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(startPos);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.CompareTag("Btn"))
                    GameManager.Instance.LoadStage(clickedObject.name);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            Vector2 endPos = Input.mousePosition;

            if (Mathf.Abs(startPos.y - endPos.y) < 2f) return; 

            if (startPos.y > endPos.y) Scroll(true);
            else Scroll(false);

        }
    }

    private int BestStageChecker()
    {
        int stageNum = 0;

        for (int i = 0; i < data.clearStage.Length; i++)
        {
            if (data.clearStage[i] == true)
            {
                stageNum++;
            }
        }

        if (stageNum >= 9) stageNum = 9;

        return stageNum;
    }

    private void Scroll(bool isUp)
    {
        if (isUp)
        {
            isStage++;
            if (isStage >= btnMaterials.Length)
            {
                isStage = btnMaterials.Length -1; return;
            }
        }
        else
        {
            isStage--;
            if (isStage < 0)
            {
                isStage = 0; return;
            }
        }

        transform.DOMove(pos[isStage], scrollSpd).SetEase(Ease.InSine).OnComplete(() => isScroll = false);
        RotateRandomly();

    }

    private void RotateRandomly()
    {
        // 무작위 시드값으로 회전

        foreach (var btn in btnLineObjs)
        {
            Quaternion randomRotation = Random.rotation;

            btn.transform.DORotateQuaternion(randomRotation, rotateSpd)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    RotateToOriginal();
                });
        }


    }

    private void RotateToOriginal()
    {
        // 원래 회전 각도로 회전

        foreach (var btn in btnLineObjs)
        {
            btn.transform.DOLocalRotate(Vector3.zero, 0.1f)
                .SetEase(Ease.Linear);
        }
    }
}

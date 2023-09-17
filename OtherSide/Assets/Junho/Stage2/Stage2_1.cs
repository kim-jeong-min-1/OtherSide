using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = ProductionEvent.Event;
using GameManager = Jungmin.GameManager;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class Stage2_1 : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private Vector3 playerPos;
    [SerializeField] private Controller player1;
    [SerializeField] private Controller player2;
    [SerializeField] private Transform[] clearPortal;
    [SerializeField] private Transform[] footboard;
    private bool isClear = false;
    private bool isMove = false;

    [SerializeField] private GameObject hideObj;
    [SerializeField] private Vector3 ObjEndPos;
    [SerializeField] private GameObject turnObj;
    [SerializeField] private int maxCheckPoints;
    private int checkPoints;
    public int CheckPoints
    {
        get { return checkPoints; }
        set 
        {
            checkPoints = value; 

            if(checkPoints == maxCheckPoints)
            {
                isMove = true;
                NotifyObservers();
            }
        }
    }

    private Volume postProcessingVolume;
    private ColorAdjustments colorAdjustments;

    [SerializeField] protected Setting setting;

    private void Awake()
    {
        postProcessingVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM(2, 0.1f);

        cam.transform.DOShakePosition(3f,0.25f);
    }
    [SerializeField] private List<IObserver> list_Observers = new List<IObserver>();
    public void ResisterObserver(IObserver observer)
    {
        list_Observers.Add(observer);
    }
    public void RemoveObserver(IObserver observer)
    {
        list_Observers.Remove(observer);
    }
    private void NotifyObservers()
    {
        //turnObj.transform.position = ObjEndPos;
        StartCoroutine(ObjMove());
        foreach (var observer in list_Observers)
        {
            observer.PlayObj();
        }
        list_Observers.Clear();
    }

    float t;
    IEnumerator ObjMove()
    {
        t = 0;
        Vector3 startPos = turnObj.transform.position;
        while (true)
        {
            yield return null;
            t += Time.deltaTime;
            turnObj.transform.position = Vector3.Lerp(startPos, ObjEndPos, t / 0.5f);

            if (turnObj.transform.position == ObjEndPos) break;
        }
        player1.gameObject.transform.localPosition = playerPos;
        player2.gameObject.transform.localPosition = playerPos;
        hideObj.SetActive(false);

    }

    IEnumerator Clear()
    {
        GameManager.Instance.stageData.clearStage[3] = true;

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear, 0.6f);
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(StageSaturation());
        yield return new WaitForSeconds(3f);
        setting.ClearWnd();
        yield break;
    }

    private void ClearCheck()
    {
        if (isMove == false) return;
        print("check");
        if((player2.currentNode == clearPortal[0] && player1.currentNode == clearPortal[1]) && isClear == false)
        {
            isClear = true;
            StartCoroutine(Clear());
        }
    }

    private void Update()
    {
        ClearCheck();
    }
    private IEnumerator StageSaturation()
    {
        if (postProcessingVolume.profile.TryGet(out colorAdjustments))
        {

            // saturation 값을 조절하는 코드

            float t = 0;

            while (t < 1f)
            {

                yield return null;

                t += Time.deltaTime;

                colorAdjustments.saturation.value = Mathf.Lerp(-100f, 0f, t / 1f);
            }
        }
    }
}

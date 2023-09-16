using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Event = ProductionEvent.Event;
using GameManager = Jungmin.GameManager;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class Stage2_Mgr : MonoBehaviour
{
    [SerializeField] private Controller p1;
    [SerializeField] private Transform EndPoint1;
    private bool Ending = false;

    private Volume postProcessingVolume;
    private ColorAdjustments colorAdjustments;

    [SerializeField] protected Setting setting;

    private void Awake()
    {
        postProcessingVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
    }

    void Start()
    {
        //StartCoroutine(Cam_Ctrl.Move(Camera.main.gameObject, 
        //    new Vector3(14, 21.73f, 13), 2));
        StartCoroutine(Stage2_Start());
    }

    void Update()
    {
        if (p1.currentNode == EndPoint1 && !Ending)
        {
            Ending = true;
            StartCoroutine(End());
        }
    }

    private IEnumerator Stage2_Start()
    {
        SoundManager.Instance.PlayBGM(2, 0.1f);

        yield return new WaitForSeconds(1.5f);
        var a = StartCoroutine(Event.CameraMove(Camera.main, new Vector3(20.5f, 29.36f, 12.71f), 10f));
        var b = StartCoroutine(Event.CameraZoom(Camera.main, 6f, 10f));

        yield return new WaitForSeconds(1.5f);
        StopCoroutine(a); StopCoroutine(b);

        yield return new WaitForSeconds(1.5f);
        var c = StartCoroutine(Event.CameraMove(Camera.main, new Vector3(20.46f, 29.36f, 19.46f), 10f));
        var d = StartCoroutine(Event.CameraZoom(Camera.main, 12.5f, 10f));

        yield return new WaitForSeconds(1.5f);
        StopCoroutine(c); StopCoroutine(d);
    }

    private IEnumerator End()
    {
        GameManager.Instance.stageData.clearStage[2] = true;

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear, 0.6f);

        yield return new WaitForSeconds(0.08f);
        StartCoroutine(StageSaturation());
        yield return new WaitForSeconds(3f);

        setting.ClearWnd();
        yield break;
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

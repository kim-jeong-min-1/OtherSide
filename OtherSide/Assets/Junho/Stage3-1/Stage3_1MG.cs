using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = ProductionEvent.Event;
using Jungmin;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
public class Stage3_1MG : MonoBehaviour
{
    [SerializeField] private Walkable[] floor1;
    [SerializeField] private Walkable[] floor1Stone;
    [SerializeField] private Walkable[] floor2;
    [SerializeField] private Walkable[] floor2Stone;

    [SerializeField] private Controller p1;
    [SerializeField] private Controller p2;
    [SerializeField] private Transform clear;


    private bool p1Clear = false;
    private bool p2Clear = false;
    private bool isClear = false;

    private Volume postProcessingVolume;
    private ColorAdjustments colorAdjustments;

    [SerializeField] protected Setting setting;

    private void Awake()
    {
        postProcessingVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
    }
    public void StartChangeNode(bool floor)
    {
        if (floor)
        {
            floor1[0].neighborNode[floor1[0].neighborNode.Count - 1].isActive = true;
            floor1[0].neighborNode[floor1[0].neighborNode.Count - 1].isActive = true;
            floor1[1].neighborNode[floor1[1].neighborNode.Count - 1].isActive = false;
            floor1[1].neighborNode[floor1[1].neighborNode.Count - 1].isActive = false;

            floor1Stone[0].neighborNode[floor1Stone[0].neighborNode.Count - 1].isActive = true;
            floor1Stone[0].neighborNode[floor1Stone[0].neighborNode.Count - 1].isActive = true;
            floor1Stone[1].neighborNode[floor1Stone[1].neighborNode.Count - 1].isActive = false;
            floor1Stone[1].neighborNode[floor1Stone[1].neighborNode.Count - 1].isActive = false;
        }
        else
        {
            floor2[0].neighborNode[floor2[0].neighborNode.Count - 1].isActive = true;
            floor2[0].neighborNode[floor2[0].neighborNode.Count - 1].isActive = true;
            floor2[1].neighborNode[floor2[1].neighborNode.Count - 1].isActive = false;
            floor2[1].neighborNode[floor2[1].neighborNode.Count - 1].isActive = false;

            floor2Stone[0].neighborNode[floor2Stone[0].neighborNode.Count - 1].isActive = true;
            floor2Stone[0].neighborNode[floor2Stone[0].neighborNode.Count - 1].isActive = true;
            floor2Stone[1].neighborNode[floor2Stone[1].neighborNode.Count - 1].isActive = false;
            floor2Stone[1].neighborNode[floor2Stone[1].neighborNode.Count - 1].isActive = false;
        }
    }

    public void EndChangeNode(bool floor)
    {
        if (floor)
        {
            floor1[0].neighborNode[floor1[0].neighborNode.Count - 1].isActive = false;
            floor1[0].neighborNode[floor1[0].neighborNode.Count - 1].isActive = false;
            floor1[1].neighborNode[floor1[1].neighborNode.Count - 1].isActive = true;
            floor1[1].neighborNode[floor1[1].neighborNode.Count - 1].isActive = true;

            floor1Stone[0].neighborNode[floor1Stone[0].neighborNode.Count - 1].isActive = false;
            floor1Stone[0].neighborNode[floor1Stone[0].neighborNode.Count - 1].isActive = false;
            floor1Stone[1].neighborNode[floor1Stone[1].neighborNode.Count - 1].isActive = true;
            floor1Stone[1].neighborNode[floor1Stone[1].neighborNode.Count - 1].isActive = true;

        }
        else
        {
            floor2[0].neighborNode[floor2[0].neighborNode.Count - 1].isActive = false;
            floor2[0].neighborNode[floor2[0].neighborNode.Count - 1].isActive = false;
            floor2[1].neighborNode[floor2[1].neighborNode.Count - 1].isActive = true;
            floor2[1].neighborNode[floor2[1].neighborNode.Count - 1].isActive = true;

            floor2Stone[0].neighborNode[floor2Stone[0].neighborNode.Count - 1].isActive = false;
            floor2Stone[0].neighborNode[floor2Stone[0].neighborNode.Count - 1].isActive = false;
            floor2Stone[1].neighborNode[floor2Stone[1].neighborNode.Count - 1].isActive = true;
            floor2Stone[1].neighborNode[floor2Stone[1].neighborNode.Count - 1].isActive = true;
        }
    }

    public void NodeCut(bool floor)
    {
        if (floor)
        {
            floor1[0].neighborNode[floor1[0].neighborNode.Count - 1].isActive = false;
            floor1[0].neighborNode[floor1[0].neighborNode.Count - 1].isActive = false;
            floor1[1].neighborNode[floor1[1].neighborNode.Count - 1].isActive = false;
            floor1[1].neighborNode[floor1[1].neighborNode.Count - 1].isActive = false;

            floor1Stone[0].neighborNode[floor1Stone[0].neighborNode.Count - 1].isActive = false;
            floor1Stone[0].neighborNode[floor1Stone[0].neighborNode.Count - 1].isActive = false;
            floor1Stone[1].neighborNode[floor1Stone[1].neighborNode.Count - 1].isActive = false;
            floor1Stone[1].neighborNode[floor1Stone[1].neighborNode.Count - 1].isActive = false;
        }
        else
        {
            floor2[0].neighborNode[floor2[0].neighborNode.Count - 1].isActive = false;
            floor2[0].neighborNode[floor2[0].neighborNode.Count - 1].isActive = false;
            floor2[1].neighborNode[floor2[1].neighborNode.Count - 1].isActive = false;
            floor2[1].neighborNode[floor2[1].neighborNode.Count - 1].isActive = false;

            floor2Stone[0].neighborNode[floor2Stone[0].neighborNode.Count - 1].isActive = false;
            floor2Stone[0].neighborNode[floor2Stone[0].neighborNode.Count - 1].isActive = false;
            floor2Stone[1].neighborNode[floor2Stone[1].neighborNode.Count - 1].isActive = false;
            floor2Stone[1].neighborNode[floor2Stone[1].neighborNode.Count - 1].isActive = false;
        }
    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM(3,0.1f);
    }

    private void Update()
    {
        if (isClear == false)
        {
            if (p1Clear == false && p1.currentNode == clear)
            {
                p1Clear = true;
            }

            if (p2Clear == false && p2.currentNode == clear)
            {
                p2Clear = true;
            }

            if (p1Clear && p2Clear)
            {
                isClear = true;
                StartCoroutine(StageClearEvent());
            }
        }
    }

    private IEnumerator StageClearEvent()
    {
        GameManager.Instance.stageData.clearStage[5] = true;
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

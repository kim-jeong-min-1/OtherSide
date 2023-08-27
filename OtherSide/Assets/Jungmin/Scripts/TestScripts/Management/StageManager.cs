using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public abstract class StageManager : MonoBehaviour
{
    [HideInInspector] public bool isClearStage = false;
    [SerializeField] private List<PathCondition> pathConditions;

    [SerializeField] protected Transform[] portal;
    [SerializeField] public Player player1;
    [SerializeField] public Player player2;
    private Volume postProcessingVolume;
    private ColorAdjustments colorAdjustments;

    protected string nextSceneName;
    public void ConnectPathOfStage()
    {
        foreach (PathCondition condition in pathConditions)
        {
            if (!condition.interactionObject.isInteract) continue;

            var interact = condition.interactionObject;

            Vector3 checkVector =
                (interact.interactType == InteractType.Rotate) ? interact.interactionAngle : interact.interactionPosition;

            if ((condition.activeValue == checkVector))
            {
                for (int i = 0; i < condition.nodes.Count; i++)
                {
                    var node = condition.nodes[i];
                    if (node.isConnectNode) node.walkable.neighborNode[node.index].isActive = true;
                    else node.walkable.neighborNode[node.index].isActive = false;
                }
            }
        }
    }
    protected virtual void Awake()
    {
        postProcessingVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
    }

    protected virtual void Update()
    {
        if (isClearStage) return;
        if (pathConditions.Count > 0) ConnectPathOfStage();
        ClearCheck();
    }

    protected abstract void StageClear();

    protected IEnumerator StageSaturation()
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
            print("End");
        }
    }

    protected abstract void ClearCheck();
    protected void LayerChange(Transform transform, int layer)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.layer = layer;
        }
    }
}

[System.Serializable]
public class PathCondition
{
    public Interaction interactionObject;
    public Vector3 activeValue;
    public List<NodeData> nodes;
}

[System.Serializable]
public class NodeData
{
    public Walkable walkable;
    public int index;
    public bool isConnectNode;
}

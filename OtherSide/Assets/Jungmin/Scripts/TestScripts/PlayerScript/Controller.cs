using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class Controller : MonoBehaviour
{
    private const float CLICK_TIME = 0.5f;
    public Transform currentNode;
    public Transform targetNode;

    [SerializeField] protected Queue<Walkable> walkPathQueue = new Queue<Walkable>();

    protected List<Transform> openList = new List<Transform>();
    protected List<Transform> closedList = new List<Transform>();
    protected List<Transform> CheckList = new List<Transform>();
    protected int nodeCount = 0;
    protected bool isEndBuild = false;
    protected float curTime = 0;
    protected float waitTime = 0;

    public bool isWalking = false;
    private Sequence walk;

    protected virtual void Update()
    {
        RayCheckToCurrentNode();
        AnimationCheck();
        TouchScreen();
    }

    protected virtual void TouchScreen()
    {
        curTime = Time.time;
        if (Input.GetMouseButtonDown(0) && curTime >= waitTime)
        {
            waitTime = Time.time + CLICK_TIME;
            curTime = 0;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(ray, out mouseHit) 
                && mouseHit.transform.GetComponent<Walkable>() != null)
            {
                if (mouseHit.transform == targetNode) return;

                StopWalking();
                targetNode = mouseHit.transform;
                if(targetNode != currentNode) FindPathAndWalking();
            }
        }
    }

    protected void FindPathAndWalking()
    {
        List<Transform> pathList = new List<Transform>();
        int pathCount = 100;

        foreach (Node node in currentNode.GetComponent<Walkable>().neighborNode)
        {
            if (!node.isActive) continue;

            CheckList.Add(currentNode);
            closedList.Add(currentNode);

            ExplorePath(node.nodePoint);

            if (openList.Count != 0 && openList[openList.Count - 1] == targetNode
                && pathCount > openList.Count)
            {
                var tempList = openList.ToList();
                pathCount = tempList.Count;
                pathList = tempList;
            }
            ResetList();
        }
        if (pathList.Count != 0) BuildPath(pathList);
        else isEndBuild = true;
    }

    protected void ExplorePath(Transform startNode)
    {
        CheckList.Add(startNode);
        closedList.Add(startNode);
        if (startNode == targetNode)
        {
            openList = CheckList.ToList();
            return;
        }

        var temp = CheckList.ToList();
        foreach (var path in startNode.GetComponent<Walkable>().neighborNode)
        {
            //if (openList[openList.Count - 1] != targetNode &&
            //    startNode.GetComponent<Walkable>().neighborNode.Count >= 3) openList = temp.ToList();

            if (closedList.Contains(path.nodePoint) || !path.isActive)
            {
                continue;
            }
            ExplorePath(path.nodePoint);

            if (CheckList[CheckList.Count - 1] == targetNode
                && (CheckList.Count < openList.Count || openList.Count == 0))
            {
                openList = CheckList.ToList();
            }
            CheckList = temp.ToList();
        }
    }

    protected virtual void BuildPath(List<Transform> pathList)
    {
        foreach (Transform path in pathList)
        {
            var walkable = path.GetComponent<Walkable>();
            nodeCount++;
            walkPathQueue.Enqueue(walkable);
        }
        isEndBuild = true;
        StartCoroutine(FollowPath());
    }

    protected virtual IEnumerator FollowPath()
    {
        walk = DOTween.Sequence();
        isWalking = true;

        for (; walkPathQueue.Count > 0;)
        {
            var path = walkPathQueue.Dequeue();
            if (path.transform == currentNode)
            {
                if (walkPathQueue.Count == 0) StopWalking();
                continue;
            }
            if (path.type == WalkableType.TelePort && targetNode != path.transform) StopWalking();

            {
                Tween tween = path.type switch
                {
                    WalkableType.Basic => transform.DOMove(path.GetWalkPoint(), 0.25f).SetEase(Ease.Linear),

                    WalkableType.TelePort =>
                    path.GetComponent<TelePort>().GetTelePortAction(transform, StopWalking),

                    WalkableType.ClearPortal =>
                    path.GetComponent<ClearPortal>().GetWalkPointAction(transform)
                };

                if (walkPathQueue.Count == 0 && path.type != WalkableType.TelePort)
                    walk.Append(tween).OnComplete(() => StopWalking());
                else if (tween != null)
                    walk.Append(tween);
                transform.SetParent(path.transform);
            }
            #region È¸Àü

            walk.Join(transform.DOLookAt(path.transform.position, .1f, AxisConstraint.Y, Vector3.up));

            #endregion

        }
        yield break;
    }

    private void RayCheckToCurrentNode()
    {
        Ray ray = new Ray(transform.GetChild(0).transform.position, -transform.up);
        RaycastHit playerHit;

        if (Physics.Raycast(ray, out playerHit))
        {
            if (playerHit.transform.GetComponent<Walkable>() != null)
            {
                currentNode = playerHit.transform;
            }
        }
    }

    private void ResetList()
    {
        openList.Clear();
        closedList.Clear();
        CheckList.Clear();
    }

    public void StopWalking()
    {
        Debug.Log(gameObject.name + "stop");

        isWalking = false;
        isEndBuild = false;

        walk.Kill();
        walkPathQueue.Clear();
        nodeCount = 0;

        transform.parent = currentNode;
    }
}

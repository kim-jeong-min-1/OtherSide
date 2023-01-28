using Jungmin;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerType1
{
    Basic,
    Follow,
}

public class Player1 : Controller1
{
    public System.Action<Transform> OtherPlayerFollowMe = null;
    public System.Func<int, bool> MovePlayerDecision = null;

    public PlayerMoveType playerType;
    [SerializeField] private Player1 OtherPlayer = null;

    protected override void Awake()
    {
        base.Awake();
        CheckOtherPlayer();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void CheckOtherPlayer()
    {
        if (OtherPlayer == null) return;

        if (playerType == PlayerMoveType.Follow)
        {
            OtherPlayer.OtherPlayerFollowMe += this.FollowOther;
        }
        else if (playerType == PlayerMoveType.Basic && OtherPlayer.playerType == PlayerMoveType.Basic)
        {
            MovePlayerDecision += this.ShortPathThenOther;
        }
    }

    protected override void TouchScreen()
    {
        if (playerType == PlayerMoveType.Follow) return;
        base.TouchScreen();
    }

    protected override void BuildPath(List<Transform> pathList)
    {
        base.BuildPath(pathList);
        OtherPlayerFollowMe?.Invoke(pathList[pathList.Count - 2]);
    }

    protected override IEnumerator FollowPath()
    {
        if(MovePlayerDecision != null)
        {
            yield return new WaitUntil(() => OtherPlayer.isEndBuild);
            if (!MovePlayerDecision(OtherPlayer.nodeCount)) yield break;
        }

        StartCoroutine(base.FollowPath());
        yield break; 
    }

    private bool ShortPathThenOther(int otherNodeCount)
    {
        if (otherNodeCount <= nodeCount && otherNodeCount != 0)
        {
            return false;
        }
        return true;
    }
    private void FollowOther(Transform target)
    {
        StopWalking();

        this.targetNode = target;
        FindPathAndWalking();
    }

}

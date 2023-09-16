using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENodeSet
{
    Clear,
    NodeSet,
    NewSet
}
public class Stage2NodeSet : MonoBehaviour,IObserver
{
    [SerializeField] private Stage2_1 mg;
    private Walkable myWalkable;
    [SerializeField] private ENodeSet mySet;
    [SerializeField] private List<Node> newSetNode = new List<Node>();
    [SerializeField] private GameObject myObj;
    [SerializeField] private Vector3 movePos;
    private void Start()
    {
        mg.ResisterObserver(this.GetComponent<Stage2NodeSet>());
        myWalkable = GetComponent<Walkable>();
    }
    public void PlayObj()
    {
        switch (mySet)
        {
            case ENodeSet.Clear:
                myWalkable.neighborNode.Clear();
                break;
            case ENodeSet.NodeSet:
                myWalkable.neighborNode = newSetNode;
                break;
            case ENodeSet.NewSet:
                myWalkable.neighborNode = newSetNode;
                print(gameObject);
                myObj.transform.position = movePos;
                break;
            default:
                print(gameObject);
                break;
        }
    }
}

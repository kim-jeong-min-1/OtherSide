using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DesentObj : MonoBehaviour
{
    [SerializeField] private Stage4_Mgr stage4_Mgr;
    [SerializeField] private Controller p1;
    [SerializeField] private Controller p2;
    [SerializeField] private Transform PlayNode;
    [SerializeField] private Walkable Node;
    [SerializeField] private float MinY;
    [SerializeField] private float MaxY;
    [SerializeField] private Walkable[] playNeighborNode;
    [SerializeField] private int[] playNeighborindex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (p1.currentNode == PlayNode || p2.currentNode == PlayNode)
        {
            StartCoroutine(Desent());
        }
        else if(p1.currentNode == stage4_Mgr.EndPoint2 || p2.currentNode == stage4_Mgr.EndPoint1)
        {
            StartCoroutine(Increase());
        }
    }

    private IEnumerator Desent()
    {
        for (int i = 0; i < Node.neighborNode.Count; i++)
        {
            Node.neighborNode[i].isActive = true;
        }
        SetPlayerParent();

        yield return new WaitForSecondsRealtime(1f);

        this.gameObject.transform.DOMoveY(MaxY, 3.5f);
        yield return new WaitForSecondsRealtime(3.5f);

        for (int i = 0; i < playNeighborNode.Length; i++)
        {
            playNeighborNode[i].neighborNode[playNeighborindex[i]].isActive = false;
        }

        

        yield break;
    }

    private IEnumerator Increase()
    {
        for (int i = 0; i < Node.neighborNode.Count; i++)
        {
            Node.neighborNode[i].isActive = false;
        }
        this.gameObject.transform.DOMoveY(MinY , 3.5f);
        yield return new WaitForSeconds(3.5f);

        for (int i = 0; i < playNeighborNode.Length; i++)
        {
            playNeighborNode[i].neighborNode[playNeighborindex[i]].isActive = true;
        }

        yield break;
    }

    private void SetPlayerParent()
    {
        if(p1.currentNode == PlayNode)
        {
            p1.transform.SetParent(transform);
        }
        else if (p2.currentNode == PlayNode)
        {
            p2.transform.SetParent(transform);
        }
    }
}

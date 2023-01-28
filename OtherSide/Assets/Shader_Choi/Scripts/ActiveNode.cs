using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveNode : MonoBehaviour
{
    [SerializeField] private Controller p1;
    [SerializeField] private Controller p2;
    [SerializeField] private Walkable OnNode;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (p1.currentNode == this.gameObject.transform || p2.currentNode == this.gameObject.transform)
        {
            for (int i = 0; i < OnNode.neighborNode.Count; i++)
            {
                OnNode.neighborNode[i].isActive = true;
            }
        }
    }
}

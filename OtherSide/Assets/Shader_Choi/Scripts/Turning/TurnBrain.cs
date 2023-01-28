using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBrain : MonoBehaviour
{
    [SerializeField] private List<Turn_Info> TurnObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Turning();
    }

    private void Turning()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(ray, out mouseHit))
            {
                if (mouseHit.collider.gameObject == gameObject)
                {
                    for (int i = 0; i < TurnObject.Count; i++)
                    {
                        if (!TurnObject[i].isTurn)
                        {
                            TurnObject[i].Turn();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cam_Control;

public class Stage2 : MonoBehaviour
{
    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    
    [SerializeField] private Controller p1;
    [SerializeField] private Controller p2                                                                                                                                                                                           ;

    [SerializeField] Transform End1;
    [SerializeField] Transform End2;

    private bool OneShout;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cam_Ctrl.FadeIn(GameObject.Find("FadeInOut").GetComponent<Image>(), 2));
        StartCoroutine(Cam_Ctrl.Move(GameObject.Find("Cam1") ,new Vector3(-21.87f, 24.18f, 20.38f), 2));
    }

    // Update is called once per frame
    void Update()
    {
        if (p1.currentNode == End1 && p2.currentNode == End2 && !OneShout)
        {
            OneShout = true;
            StartCoroutine(Ending());
        }

        //PlayerChange();
    }

    //private void PlayerChange()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit Hit;  
            
    //        if(Physics.Raycast(ray, out Hit))
    //        {
    //            if(Hit.collider.tag == "Player")
    //            {
    //                player1.isActive = true;
    //                player2.isActive = false;
    //            }
    //            else if (Hit.collider.tag == "Player1")
    //            {
    //                player1.isActive = false;
    //                player2.isActive = true;
    //            }
    //        }
    //    }
    //}

    private IEnumerator Ending()
    {
        StartCoroutine(Cam_Ctrl.FadeOut(GameObject.Find("FadeInOut").GetComponent<Image>(), 2));
        yield return new WaitForSecondsRealtime(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Re_Stage3");

        yield break;
    }
}

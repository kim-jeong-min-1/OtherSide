using Jungmin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    private bool isPause = false;

    [SerializeField] private GameObject Wnds;
    [SerializeField] private GameObject nextStageBtn;


    public void PauseBtn()
    {

        if (isPause == true)
        {
            isPause = false;

            Time.timeScale = 1;
        }
        else
        {
            isPause = true;

            Time.timeScale = 0;
        }

        Wnds.SetActive(isPause);
    }

    public void ClearWnd()
    {
        Wnds.GetComponent<Image>().color = new Color(0,0,0,0);

        Wnds.SetActive(true);
        nextStageBtn.SetActive(true);
    }

    public void ExitBtn()
    {
        Time.timeScale = 1;
        GameManager.Instance.LoadStage("Title");
    }

    public void NextStageBtn(string nextStageName) 
    {
        GameManager.Instance.LoadStage(nextStageName);
    }

}

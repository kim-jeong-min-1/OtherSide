using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Event = ProductionEvent.Event;
using Jungmin;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private Text pText;
    [SerializeField] GameObject Logo;
    private bool isOneTime = false;
    private bool isAlready = false;

    [SerializeField] GameObject StageSelectWnd;
    [SerializeField] Image StageSelectWndBG;

    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(TitleStart());
    }

    // Update is called once per frame
    void Update()
    {
        PressKey();
    }

    void PressKey()
    {
        if (Input.anyKey && !isOneTime && isAlready)
        {
            StageSelectWndOn();
            isOneTime = true;
        }
    }
    IEnumerator TitleStart()
    {

        yield return new WaitForSeconds(3.5f);
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));

        yield return new WaitForSeconds(3.5f);
        Logo.gameObject.SetActive(false);
        StartCoroutine(Event.FadeOut(GameManager.Instance.fadeImage));

        yield return new WaitForSeconds(2f);

        isAlready = true;
        SoundManager.Instance.PlayBGM();
        StartCoroutine(TextAlpha(-1));

    }

    private void StageSelectWndOn()
    {
        StageSelectWndBG.DOFade(0.1f,1);
        StageSelectWnd.transform.DOLocalMoveY(0,1);

        StageSelectWnd.SetActive(true);
    }

    public void StageSelectBtn(string stageName)
    {
        GameManager.Instance.fadeImage.gameObject.SetActive(true);

        StartCoroutine(StartGame(stageName));
    }

    IEnumerator StartGame(string stageName)
    {
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3);
        SoundManager.Instance.StopBGM();
        SceneManager.LoadScene(stageName);
    }

    IEnumerator TextAlpha(int sign)
    {
        while (true)
        {
            Color color = pText.color;
            color.a = color.a + (sign * Time.deltaTime);
            pText.color = color;

            if(pText.color.a >= 1 || pText.color.a <= 0)
            {
                color.a = (pText.color.a >= 1) ? 1 : 0;
                pText.color = color;
                break;
            }
            yield return null;
        }
        StartCoroutine(TextAlpha(-sign));
    }
}

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

    [SerializeField] private GameObject StageSelectWnd;
    [SerializeField] private Image titleObj;

    [SerializeField] private Tutorial tutorialStage;
    public bool isTutorialClear = false;
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(TitleStart());

        GameManager.Instance.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTutorialClear == true)
        PressKey();
    }

    void PressKey()
    {
        if (Input.anyKey && !isOneTime && isAlready)
        {
            isOneTime = true;
            StageSelectWndOn();
        }
    }
    IEnumerator TitleStart()
    {

        yield return new WaitForSeconds(3.5f);
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));

        yield return new WaitForSeconds(3.5f);
        Logo.gameObject.SetActive(false);
        StartCoroutine(Event.FadeOut(GameManager.Instance.fadeImage));

        tutorialStage.StartSet();

        yield return new WaitForSeconds(2f);

        SoundManager.Instance.PlayBGM();

        

    }

    public void TutorialClearTitleStart()
    {
        titleObj.gameObject.SetActive(true);
        pText.gameObject.SetActive(true);

        isAlready = true;
        StartCoroutine(TextAlpha(-1));
    }

    private void StageSelectWndOn()
    {
        titleObj.gameObject.SetActive(false);

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

            if (isOneTime == true)
            {
                pText.gameObject.SetActive(false);

                yield break;
            }
        }
        StartCoroutine(TextAlpha(-sign));
    }
}

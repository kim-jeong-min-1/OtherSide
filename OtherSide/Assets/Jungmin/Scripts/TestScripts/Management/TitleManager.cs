using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Event = ProductionEvent.Event;
using Jungmin;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private Text pText;
    [SerializeField] GameObject Logo;
    private bool isOneTime = false;
    private bool isAlready = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TitleStart());
        StartCoroutine(TextAlpha(-1));
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
            StartCoroutine(StartGame());
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
    }

    IEnumerator StartGame()
    {
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3);
        SoundManager.Instance.StopBGM();
        SceneManager.LoadScene("RE_Stage1");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Event = ProductionEvent.Event;
using GameManager = Jungmin.GameManager;

public class Stage2_Mgr : MonoBehaviour
{
    [SerializeField] private Controller p1;
    [SerializeField] private Transform EndPoint1;
    private bool Ending = false;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Cam_Ctrl.Move(Camera.main.gameObject, 
        //    new Vector3(14, 21.73f, 13), 2));
        StartCoroutine(Stage2_Start());
    }

    // Update is called once per frame
    void Update()
    {
        if (p1.currentNode == EndPoint1 && !Ending)
        {
            Ending = true;
            StartCoroutine(End());
        }
    }

    private IEnumerator Stage2_Start()
    {
        SoundManager.Instance.PlayBGM(2, 0.1f);

        yield return new WaitForSeconds(1.5f);
        var a = StartCoroutine(Event.CameraMove(Camera.main, new Vector3(20.5f, 29.36f, 12.71f), 10f));
        var b = StartCoroutine(Event.CameraZoom(Camera.main, 6f, 10f));

        yield return new WaitForSeconds(1.5f);
        StopCoroutine(a); StopCoroutine(b);

        yield return new WaitForSeconds(1.5f);
        var c = StartCoroutine(Event.CameraMove(Camera.main, new Vector3(20.46f, 29.36f, 19.46f), 10f));
        var d = StartCoroutine(Event.CameraZoom(Camera.main, 12.5f, 10f));

        yield return new WaitForSeconds(1.5f);
        StopCoroutine(c); StopCoroutine(d);
    }

    private IEnumerator End()
    {
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear, 0.6f);
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage2_1");

        yield break;
    }
}

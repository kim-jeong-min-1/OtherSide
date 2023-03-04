using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cam_Control;
using Event = ProductionEvent.Event;
using GameManager = Jungmin.GameManager;


public class Stage4_Mgr : MonoBehaviour
{
    [SerializeField] private CameraShake shake;
    [SerializeField] private Controller p1;
    [SerializeField] private Controller p2;
    public Transform EndPoint1;
    public Transform EndPoint2;
    private bool Ending;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Event.CameraMove(Camera.main, new(-15.82f, 18.29f, 16.96f), 230f));
        SoundManager.Instance.PlaySFX(SoundEffect.Vibration, 0.8f, 0.6f, 3);
        SoundManager.Instance.PlayBGM(4, 0.1f);
        //StartCoroutine(Event.CameraShake(Camera.main, 0.5f, 3));
    }

    // Update is called once per frame
    void Update()
    {
        if (((p1.currentNode == EndPoint1 && p2.currentNode == EndPoint2) || (p1.currentNode == EndPoint2 && p2.currentNode == EndPoint1)) && !Ending)
        {
            Ending = true;

            p1.gameObject.SetActive(Ending);
            p2.gameObject.SetActive(Ending);
            StartCoroutine(End());
        }
    }

    private IEnumerator End()
    {
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear, 0.6f);
        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage4_1");

        yield break;
    }
}

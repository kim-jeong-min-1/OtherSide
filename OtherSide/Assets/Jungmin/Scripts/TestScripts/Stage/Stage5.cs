using Jungmin;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using Event = ProductionEvent.Event;

public class Stage5 : StageManager
{
    [SerializeField] private List<TelePortEvent> telePortEvents;

    [SerializeField] Transform PortalCover1;
    [SerializeField] Transform PortalCover2;
    protected override void ClearCheck()
    {
        int check = 0;
        for (int i = 0; i < portal.Length; i++)
        {
            if (player1.currentNode == portal[i]) check++;
            if (player2.currentNode == portal[i]) check++;
        }
        
        if(check == 2)
        {
            StageClear();
            isClearStage = true;
        }
    }

    protected override void StageClear()
    {
        StartCoroutine(Stage5ClearEvent());
    }
    private IEnumerator Stage5ClearEvent()
    {
        SoundManager.Instance.PlaySFX(SoundEffect.GameClear, 0.6f);
        yield return new WaitForSeconds(1f);
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
        LayerChange(portal[0], 0);

        StartCoroutine(Event.FadeIn(GameManager.Instance.fadeImage));
        yield return new WaitForSeconds(3f);

        nextSceneName = "Title";
        GameManager.Instance.LoadStage(nextSceneName);

        yield break;
    }

    private void TelePortEventCheck()
    {
        for (int i = 0; i < telePortEvents.Count; i++)
        {
            var Tevent = telePortEvents[i];
            if (Tevent.checking && !Tevent.isOneTime)
            {
                Tevent.isOneTime = true;
                StartCoroutine(Event.ObjectAppearance
                    (Tevent.EventObject.gameObject, Tevent.EventObject.activeValues[0], Tevent.moveTime));

                SoundManager.Instance.PlaySFX(SoundEffect.Vibration, 0.8f, 1, Tevent.moveTime + 0.3f);
                StartCoroutine(Event.CameraShake(Camera.main, 0.45f, Tevent.moveTime));
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        TelePortEventCheck();
    }

    [System.Serializable]
    public class TelePortEvent
    {
        public Transform InteractTelePort;
        public Player InteractPlayer;
        public Interaction EventObject;
        public float moveTime;

        public bool isOneTime;
        public bool checking => InteractPlayer.currentNode == InteractTelePort && !isOneTime;
    }

}

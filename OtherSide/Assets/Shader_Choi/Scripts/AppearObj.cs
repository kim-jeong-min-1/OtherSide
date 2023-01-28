using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AppearObj : MonoBehaviour
{
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private Walkable OnNode;
    [SerializeField] private List<Appear_Info> appear_Info;
    [SerializeField] private Controller p1;
    [SerializeField] private Controller p2;
    private bool Shake;
    private bool isSoundOneTime = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((p1.currentNode == this.gameObject.transform || p2.currentNode == this.gameObject.transform))
        {
            if(!isSoundOneTime) SoundManager.Instance.PlaySFX(SoundEffect.Vibration, 0.7f, 1, 1.5f);
            isSoundOneTime = true;

            StartCoroutine(Appear());
        }
        else
        {
            StartCoroutine(Disappear());
        }
    }

    private IEnumerator Appear()
    {
        for (int i = 0; i < appear_Info.Count; i++)
        {
            if (Shake)
            {
                cameraShake.Shake();
            }

            if (i == appear_Info.Count - 1)
            {
                for (int j = 0; j < OnNode.neighborNode.Count; j++)
                {
                    OnNode.neighborNode[j].isActive = true;
                }
            }

            switch (appear_Info[i].appearVec)
            {
                case AppearVec.X:
                    appear_Info[i].gameObject.transform.DOMoveX(appear_Info[i].Distance, appear_Info[i].Time).SetEase(Ease.OutQuint);
                    break;

                case AppearVec.Y:
                    appear_Info[i].gameObject.transform.DOMoveY(appear_Info[i].Distance, appear_Info[i].Time).SetEase(Ease.OutQuint);
                    break;

                case AppearVec.Z:
                    appear_Info[i].gameObject.transform.DOMoveZ(appear_Info[i].Distance, appear_Info[i].Time).SetEase(Ease.OutQuint);
                    break;
            }

            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield break;
    }
    
    private IEnumerator Disappear()
    {
        Shake = false;
        isSoundOneTime = false;

        for (int i = 0; i < appear_Info.Count; i++)
        {
            switch (appear_Info[i].appearVec)
            {
                case AppearVec.X:
                    appear_Info[i].gameObject.transform.DOMoveX(appear_Info[i].Distance - 1.5f * appear_Info[i].Vec, appear_Info[i].Time).SetEase(Ease.OutQuint);
                    break;

                case AppearVec.Y:
                    appear_Info[i].gameObject.transform.DOMoveY(appear_Info[i].Distance - 1.5f * appear_Info[i].Vec, appear_Info[i].Time).SetEase(Ease.OutQuint);
                    break;

                case AppearVec.Z:
                    appear_Info[i].gameObject.transform.DOMoveZ(appear_Info[i].Distance - 1.5f * appear_Info[i].Vec, appear_Info[i].Time).SetEase(Ease.OutQuint);
                    break;
            }

            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Cam_Control
{
    public class Cam_Ctrl : MonoBehaviour
    {
        private Vector3 cameraPos;
        private static float shakeRange;
        private static bool Cancel;

        // 카메라 이동
        public static IEnumerator Move(GameObject Cam ,Vector3 Pos, float Time)
        {
            Cam.transform.DOMove(Pos, Time).SetEase(Ease.OutQuint);

            yield break;
        }

        // 카메라 확대
        public static IEnumerator EnlargementAndReduction(Camera Cam, float Size, float Time)
        {
            Cam.DOOrthoSize(Size, Time).SetEase(Ease.OutQuad);

            foreach (Transform ChildCam in Cam.transform)
            {
                Camera camera = ChildCam.gameObject.GetComponent<Camera>();
                camera.DOOrthoSize(Size, Time).SetEase(Ease.OutQuad);
            }

            yield break;
        }
        // 일단 놔두는 코드
        #region
        // 카메라 확대 (카메라, 거리, 시간)
        //public static IEnumerator Enlargement(GameObject Cam, float Distance, float Time)
        //{
        //    NowPos = Cam.transform;

        //    Cam.transform.DOLocalMove(new Vector3(NowPos.position.x, NowPos.position.y, NowPos.position.z + Distance),
        //        Time).SetEase(Ease.OutQuad); 

        //    yield break;
        //}

        // 카메라 축소 (카메라, 거리, 시간)
        //public static IEnumerator Reduction(GameObject Cam, float Distance, float Time)
        //{
        //    NowPos = Cam.transform;

        //    Cam.transform.DOLocalMove(new Vector3(NowPos.position.x, NowPos.position.y, NowPos.position.z - Distance),
        //        Time).SetEase(Ease.OutQuad);

        //    yield break;    
        //}
        #endregion

        // 페이드 인
        public static IEnumerator FadeIn(Image Fade, float Time)
        {
            Debug.Log(Fade);
            Fade.DOColor(new Color(0, 0, 0, 1), 0);
            Fade.DOColor(new Color(0, 0, 0, 0), Time).SetEase(Ease.InQuad);

            yield break;
        }

        // 페이드 아웃
        public static IEnumerator FadeOut(Image Fade, float Time)
        {
            Fade.DOColor(new Color(0, 0, 0, 0), 0);
            Fade.DOColor(new Color(0, 0, 0, 1), Time).SetEase(Ease.InQuad);

            yield break;
        }
    }
}
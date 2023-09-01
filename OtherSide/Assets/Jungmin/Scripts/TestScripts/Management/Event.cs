using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting.FullSerializer;

namespace ProductionEvent
{
    public static class Event
    {
        public static IEnumerator FadeIn(Image image)
        {
            image.gameObject.SetActive(true);

            Color color1 = image.color;
            color1.a = 0;
            image.color = color1;
            WaitForSeconds wait = new WaitForSeconds(1);
            yield return wait;

            while(image.color.a < 1)
            {
                Color color2 = image.color;
                color2.a += Time.deltaTime * 0.5f;
                image.color = color2;

                yield return new WaitForFixedUpdate();
            }
        }

        public static IEnumerator FadeOut(Image image)
        {
            Color color1 = image.color;
            color1.a = 1;
            image.color = color1;
            WaitForSeconds wait = new WaitForSeconds(1);
            yield return wait;

            while (image.color.a > 0)
            {
                Color color2 = image.color;
                color2.a -= Time.deltaTime * 0.5f;
                image.color = color2;

                yield return new WaitForFixedUpdate();
            }

            image.gameObject.SetActive(false);
        }

        public static IEnumerator CameraZoom(Camera camera, float size, float time)
        {
            float percent = 0;
            float current = 0;

            while (percent < 1)
            {
                current += Time.deltaTime;
                percent = current / time;

                camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, size, percent);

                yield return null;
            }
        }

        public static IEnumerator CameraMove(Camera camera, Vector3 movePos, float time)
        {
            float percent = 0;
            float current = 0;

            while (percent < 1)
            {
                current += Time.deltaTime;
                percent = current / time;

                camera.transform.position = Vector3.Lerp(camera.transform.position, movePos, percent);
                yield return null;
            }
        }

        public static IEnumerator CameraShake(Camera camera, float strength, float time)
        {
            Vector3 existingPosition = camera.transform.localPosition;

            camera.DOShakePosition(time, strength, 10, 5.5f);
            yield return new WaitForSeconds(time + 0.5f);
            camera.transform.localPosition = existingPosition;
        }

        public static IEnumerator ObjectAppearance(GameObject obj, Vector3 movePos, float time)
        {
            obj.transform.DOMove(movePos, time).SetEase(Ease.Linear);
            yield break;
        }
    }

}
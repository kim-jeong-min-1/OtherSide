using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ProductionEvent;
using Event = ProductionEvent.Event;

namespace Jungmin
{
    public class GameManager : Singleton<GameManager>
    {
        [HideInInspector] public StageManager currentStage = null;
        public Image fadeImage;

        protected override void Awake()
        {
            base.Awake();

            SceneManager.sceneLoaded += 
                (Scene scene, LoadSceneMode mode) => { Application.targetFrameRate = 60; };
            SceneManager.sceneLoaded +=
                (Scene scene, LoadSceneMode mode) => { StartCoroutine(Event.FadeOut(fadeImage)); };

            SceneManager.sceneLoaded += InitStage;
        }

        public void LoadStage(string stageName)
        {
            fadeImage.gameObject.SetActive(true);
            SceneManager.LoadScene(stageName);
        }

        public void InitStage(Scene scene, LoadSceneMode mode)
        {
            currentStage = FindObjectOfType<StageManager>();
        }
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ProductionEvent;
using Event = ProductionEvent.Event;
using UnityEngine.Playables;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Jungmin
{
    public class GameManager : Singleton<GameManager>
    {
        [HideInInspector] public StageManager currentStage = null;
        public Image fadeImage;
        public StageDatas stageData;

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

            StageCheck(stageName);

            fadeImage.gameObject.SetActive(true);

            SceneManager.LoadScene(stageName);
        }

        public void InitStage(Scene scene, LoadSceneMode mode)
        {
            currentStage = FindObjectOfType<StageManager>();
        }


        private void OnApplicationQuit()
        {
            SaveGame();
        }


        private void SaveGame()
        {

            if(stageData == null)
            {
                stageData = Resources.Load<StageDatas>("StageData");
            }

            stageData.isLoadData = false;

            // 게임 데이터를 저장
            string jsonData = JsonUtility.ToJson(stageData);
            PlayerPrefs.SetString("GameData", jsonData);
            PlayerPrefs.Save();
        }

        public void LoadGame()
        {
            if (stageData == null)
            {
                stageData = Resources.Load<StageDatas>("StageData");
            }

            if (stageData.isLoadData == true) return;

            // 게임 데이터를 불러오기
            string jsonData = PlayerPrefs.GetString("GameData");
            if (!string.IsNullOrEmpty(jsonData))
            {
                JsonUtility.FromJsonOverwrite(jsonData, stageData);
               // stageData = JsonUtility.FromJson<StageDatas>(jsonData);
            }

            stageData.isLoadData = true; 
        }


        private void StageCheck(string str)
        {
            int num = 0;
            switch (str) 
            {
                case "Stage1": num = 0; break;
                case "Stage1_1": num = 1; break;
                case "Stage2": num = 2; break;
                case "Stage2_1": num = 3; break;
                case "Stage3": num = 4; break;
                case "Stage3_1": num = 5; break;
                case "Stage4": num = 6; break;
                case "Stage4_1": num = 7; break;
                case "Stage5": num = 8; break;
                case "Stage5_1": num = 9; break;
                default: num = 10; break;
            }

            if(num < 10) stageData.lastPlayStage = num;
        }

    }
}
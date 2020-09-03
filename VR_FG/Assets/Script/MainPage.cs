using System;
using Script.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class MainPage : SingleTone<MainPage>
    {
        public GameObject mainPanel;
        public GameObject progressPanel;
        
        public Button playButton;
        public PlayerNameInputField playerNameInputField;

        private void Start()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            Launcher.Instance.Connect();
        }

        public void ShowProgressPanel(bool show)
        {
            mainPanel.SetActive(!show);
            progressPanel.SetActive(show);
        }
    }
}

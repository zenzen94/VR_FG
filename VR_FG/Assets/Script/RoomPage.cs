using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class RoomPage : MonoBehaviour
    {
        public Button leaveRoomButton;

        private void Awake()
        {
            leaveRoomButton.onClick.AddListener(OnLeaveRoomButtonClicked);
        }

        private void OnLeaveRoomButtonClicked()
        {
            Debug.Log("OnLeaveRoomButtonClicked");
            GameManager.Instance.LeaveRoom();
        }
    }
}

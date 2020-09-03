using System;
using Photon.Pun;
using Photon.Realtime;
using Script.Util;
using UnityEngine;

namespace Script
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        private string gameVersion = "1";
        
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        private static Launcher instance;

        public static Launcher Instance
        {
            get
            {
                if (instance == null)
                {
                    var obj = FindObjectOfType<Launcher>();
                    if (obj != null)
                    {
                        instance = obj;
                    }
                    else
                    {
                        var newSingleton = new GameObject("Launcher").AddComponent<Launcher>();
                        instance = newSingleton;
                    }
                }
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        private void Awake()
        {
            var objs = FindObjectsOfType<Launcher>();

            if (objs.Length != 1)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            MainPage.Instance.ShowProgressPanel(false);
        }

        public void Connect()
        {
            MainPage.Instance.ShowProgressPanel(true);
            
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
            
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
            MainPage.Instance.ShowProgressPanel(false);
        }
        
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        }
    }
}

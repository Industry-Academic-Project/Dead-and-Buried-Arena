using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

namespace Server
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public string roomName = "Room1";
        
        ///////// not currently using /////////
        public InputField nicknameInput;
        public GameObject disconnectPanel;
        public GameObject respawnPanel;
        //////////////// end //////////////////
        
        #region Unity Event Functions
        private void Awake()
        {
    #if UNITY_STANDALONE
            Screen.SetResolution(960, 540, false);
    #endif
            // 전송 속도 및 직렬화 속도 조정
            PhotonNetwork.SendRate = 60;
            PhotonNetwork.SerializationRate = 60;
        }

        private void Start()
        {
            // 시작하자마자 자동으로 접속, OnConnectedToMaster 함수 실행
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }

        // 재접속 딜레이
        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(1f);
            PhotonNetwork.JoinRandomRoom();
        }

        void Update()
        {
            // 연결 해제, 사용되지 않음
            if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Disconnect();
            }
        }
        
        #endregion
        
        #region PunCallbacks
        
        public override void OnConnectedToMaster()
        {
            Debug.Log("마스터 서버 접속 완료");
            if (PhotonNetwork.IsConnected)
            {
                Debug.Log("PhotonNetwork.IsConnected: true");
                
                // 마스터 서버 접속 직후 랜덤한 원래 있는 방에 들어가거나 Range로 생성된 정수를 사용해 방을 생성한다.
                print(nameof(OnJoinedLobby));
                PhotonNetwork.JoinRandomOrCreateRoom(
                    null,
                    4,
                    MatchmakingMode.FillRoom,
                    null,
                    null,
                    UnityEngine.Random.Range(0, 1000).ToString(),
                    new RoomOptions() { MaxPlayers = 4 });
                
                // PhotonNetwork.JoinOrCreateRoom(UnityEngine.Random.Range(0, 1000).ToString(),
                //     new RoomOptions() { MaxPlayers = 4 }, null);
            }
            
            // PhotonNetwork.LocalPlayer.NickName = nicknameInput.text;
            // PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions() { MaxPlayers = 6 }, null);
        }
        
        public override void OnJoinedLobby()
        {
            Debug.LogWarning("로비 접속 성공");
           
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("새로운 방이 생성되었습니다. 방 이름: " + PhotonNetwork.CurrentRoom.Name);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("방에 참가하는 데 성공했습니다. 방 이름: " + PhotonNetwork.CurrentRoom.Name);
            PhotonNetwork.LoadLevel("VRTutorial_Fallback");

            // disconnectPanel.SetActive(false);
            // Spawn();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log($"{message}: OnJoinRandom으로 방을 참가하는 데 실패했습니다.");

            // string roomName = UnityEngine.Random.Range(0, 1000).ToString();
            // PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 4 });
            // Debug.LogWarning(roomName);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log($"{message}: 방을 만드는 데 실패했습니다.");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log($"{message}: 방을 참가하는 데 실패했습니다.");
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 });
            
            Debug.LogWarning(PhotonNetwork.CurrentRoom.Name);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarning("연결이 끊겼습니다.");
            // disconnectPanel.SetActive(true);
            // respawnPanel.SetActive(false);
        }

        #endregion
        
        #region Public Methods
        public void Connect()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }

        public void Spawn()
        {
            // PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
            // respawnPanel.SetActive(false);
        }

        public void JoinLobby()
        {
            PhotonNetwork.JoinLobby();
        }

        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 4 });
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(roomName);
        }

        public void JoinOrCreateRoom()
        {
            PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions() { MaxPlayers = 4 }, null);
        }

        public void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        #endregion
        
        #region Private Methods
        #endregion
    }

}

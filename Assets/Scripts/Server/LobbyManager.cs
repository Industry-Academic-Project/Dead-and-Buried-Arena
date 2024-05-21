using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Management;

namespace Server
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        public const string GameVersion = "1";
        [SerializeField] private string sceneNameToJoin;
        [SerializeField] private Button JoinGameBtn;
        [SerializeField] private Text networkStatus;

        private void Start()
        {
            PhotonNetwork.GameVersion = GameVersion;
            JoinGameBtn.interactable = false;

            networkStatus.text = "마스터 서버에 접속 중입니다...";
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            // ConnectUsingSettings() 실패
            JoinGameBtn.interactable = false;
            
            Debug.LogWarning(cause);
            networkStatus.text = "연결에 실패했습니다.\n재접속 중입니다...";

            StartCoroutine(DelayedRetry(2f));
        }

        private IEnumerator DelayedRetry(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            networkStatus.text = "마스터 서버 재접속 중...";
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            // 룸 접속 버튼 활성화
            JoinGameBtn.interactable = true;
            networkStatus.text = "마스터 서버와 연결되었습니다.";
        }

        public void TryConnect()
        {
            // 게임 접속 가능
            JoinGameBtn.interactable = true;
            
            // 마스터 서버에 접속 중
            if (PhotonNetwork.IsConnected)
            {
                networkStatus.text = "룸에 접속 중...";
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // 마스터 서버에 접속 중이 아니라면 마스터 서버 재접속 시도
                networkStatus.text = "연결에 실패했습니다.\n재접속 중입니다...";
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            // 방 접속 실패
            Debug.LogWarning(message);

            networkStatus.text = "방 접속에 실패했습니다.\n새로운 방을 생성 중입니다...";
            
            // 최대 4명을 수용할 수 있는 방 생성
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 });
        }

        public override void OnJoinedRoom()
        {
            networkStatus.text = "방 참가에 성공하였습니다.";
            
            // 로비에 있는 모든 사람의 컴퓨터에 sceneNameToJoin 씬을 로드시킨다.
            PhotonNetwork.LoadLevel(sceneNameToJoin);
        }

    }
}
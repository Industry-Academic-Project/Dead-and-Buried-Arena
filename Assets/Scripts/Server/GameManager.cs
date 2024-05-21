using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Server
{
    public class GameManager : MonoBehaviour
    {
        public GameObject playerPrefab;
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                }

                return _instance;
            }
        }
        
        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        
        private void Start()
        {
            // 생성할 랜덤 위치 지정
            Vector3 randomSpawnPos = Random.insideUnitSphere * 10f;
            // 위치 y값은 0으로 변경
            randomSpawnPos.y = 0f;

            // 네트워크 상의 모든 클라이언트들에서 생성 실행
            // 단, 해당 게임 오브젝트의 주도권은, 생성 메서드를 직접 실행한 클라이언트에게 있음
            PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PhotonNetwork.LeaveRoom();
            }
        }
    }
}
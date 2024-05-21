using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Server
{
    public class PlayerShooter : MonoBehaviourPunCallbacks
    {
        public GameObject bulletPrefab;
        public Transform rFirePosition;
        public Transform lFirePosition;
        

        private void Start()
        {
            if (!photonView.IsMine)
                return;
        }

        private void Update()
        {
            if (!photonView.IsMine)
                return;
            
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                PhotonNetwork.Instantiate("Bullet", rFirePosition.position, rFirePosition.rotation);
            }
        
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                PhotonNetwork.Instantiate("Bullet", lFirePosition.position, lFirePosition.rotation);
            }
        }
        
    }

}
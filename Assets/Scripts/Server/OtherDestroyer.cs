using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

namespace Server
{
    public class OtherDestroyer : MonoBehaviourPun
    {
        [SerializeField] private bool destroyGameObject;
        [SerializeField] private List<Component> destroyList;
        private PhotonView pView;
        private void OnValidate()
        {
            if (destroyGameObject)
            {
                destroyList.Clear();
                Debug.LogWarning($"{nameof(destroyList)}는 {nameof(destroyGameObject)}가 true일 때 채워넣을 수 없습니다.");
            }
        }
        
        private void Start()
        {
            pView = transform.parent.GetComponentInChildren<PhotonView>();

            if (pView.IsMine)
                return;
            
            if (destroyGameObject)
            {
                Destroy(gameObject);
                return;
            }
            
            foreach (var component in destroyList)
            {
                Destroy(component);
            }
        }
    }
}
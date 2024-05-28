using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Server
{
    public class OvrCamManager : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(nameof(FindMyPlayer));
        }

        private IEnumerator FindMyPlayer()
        {
            while (true)
            {
                var playerShooters = FindObjectsOfType<global::PlayerShooter>();

                foreach (var shooter in playerShooters)
                {
                    if (shooter.photonView.IsMine)
                    {
                        gameObject.transform.parent = shooter.gameObject.transform;
                        // gameObject.transform.localPosition = Vector3.zero;
                        gameObject.transform.localScale = Vector3.one;

                        yield break;
                    }
                }

                yield return null;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

namespace Server
{
    public class OvrCamManager : MonoBehaviour
    {
        [SerializeField] private Transform headTransform;
        [SerializeField] private Transform leftHandTransform;
        [SerializeField] private Transform rightHandTransform;
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
                        // gameObject.transform.localPosition = Vector3.zero;
                        // gameObject.transform.localScale = Vector3.one;

                        var shooterVRIK = shooter.gameObject.GetComponent<VRIK>();
                        shooterVRIK.solver.leftArm.target = leftHandTransform;
                        shooterVRIK.solver.rightArm.target = rightHandTransform;
                        shooterVRIK.solver.spine.headTarget = headTransform;
                        
                        yield break;
                    }
                }

                yield return null;
            }
        }
    }
}
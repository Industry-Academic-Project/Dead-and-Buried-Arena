using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform shootingPositionL;
    public Transform shootingPositionR;// 발사 위치

    public ParticleSystem MuzzleFlashL;
    public ParticleSystem MuzzleFlashR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) // 오른쪽 버튼
        {
            //총알 발사 입력
            Instantiate(bulletPrefab, shootingPositionR.position, shootingPositionR.rotation);
            MuzzleFlashR.Play();
        }
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) // 오른쪽 버튼
        {
            //총알 발사 입력
            Instantiate(bulletPrefab, shootingPositionL.position, shootingPositionL.rotation);
            MuzzleFlashL.Play();
        }
    }
}

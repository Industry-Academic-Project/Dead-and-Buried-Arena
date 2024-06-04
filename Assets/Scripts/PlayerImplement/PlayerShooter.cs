using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform shootingPositionL;
    public Transform shootingPositionR;// 발사 위치

    public ParticleSystem MuzzleFlashL;
    public ParticleSystem MuzzleFlashR;
    
    public int maxBulletCountL;
    public int maxBulletCountR;
    private int _bulletCountL;
    private int _bulletCountR;
    public bool isReloadL;
    public bool isReloadR;

    public Slider bulletCountSilderL;
    public Slider bulletCountSilderR;

    public TMPro.TextMeshProUGUI bulletCountTextL;
    public TMPro.TextMeshProUGUI bulletCountTextR;

    public Image gunImageL;
    public Image gunImageR;

    public Sprite gunSprite;
    public Sprite reloadSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        _bulletCountL = maxBulletCountL;
        _bulletCountR = maxBulletCountR;
        
        isReloadL = false;
        isReloadR = false;

        bulletCountSilderL.maxValue = maxBulletCountL;
        bulletCountSilderR.maxValue = maxBulletCountR;

        gunImageL.sprite = gunSprite;
        gunImageR.sprite = gunSprite;
    }

    // Update is called once per frame
    void Update()
    {
        bulletCountSilderL.value = _bulletCountL;
        bulletCountSilderR.value = _bulletCountR;
        bulletCountTextL.text = $"{_bulletCountL}/{maxBulletCountL}";
        bulletCountTextR.text = $"{_bulletCountR}/{maxBulletCountR}";
        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && _bulletCountR > 0 && isReloadR == false) // 오른쪽 버튼
        {
            //총알 발사 입력
            Instantiate(bulletPrefab, shootingPositionR.position, shootingPositionR.rotation);
            MuzzleFlashR.Play();
            _bulletCountR--;
        }
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)&& _bulletCountL> 0 && isReloadL == false) // 왼쪽 버튼
        {
            //총알 발사 입력
            Instantiate(bulletPrefab, shootingPositionL.position, shootingPositionL.rotation);
            MuzzleFlashL.Play();
            _bulletCountL--;
        }

        if (OVRInput.GetDown(OVRInput.Button.Two) && isReloadR == false)
        {
            StartCoroutine(ReloadR());
        }
        if (OVRInput.GetDown(OVRInput.Button.Four) && isReloadL == false)
        {
            StartCoroutine(ReloadL());
        }
    }

    public IEnumerator ReloadR()
    {
        gunImageR.sprite = reloadSprite;
        isReloadR = true;
        yield return new WaitForSeconds(1.5f);
        _bulletCountR = maxBulletCountR;
        isReloadR = false;
        gunImageR.sprite = gunSprite;
    }
    public IEnumerator ReloadL()
    {
        gunImageL.sprite = reloadSprite;
        isReloadL = true;
        yield return new WaitForSeconds(1.5f);
        _bulletCountL = maxBulletCountL;
        isReloadL = false;
        gunImageL.sprite = gunSprite;
    }
}

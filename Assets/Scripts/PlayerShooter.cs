using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShooter : MonoBehaviourPun
{
    public GameObject bulletPrefab;

    public Transform rFirePosition;
    public Transform lFirePosition;

    [SerializeField] private float maxHealth = 100f;
    private float currHealth;

    public GameObject onDeathCanvas;

    // Start is called before the first frame update
    private void Start()
    {
        if (!photonView.IsMine) return;

        currHealth = maxHealth;
        
        
        // StartCoroutine(DelayedFire());
    }

    // Update is called once per frame
    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            photonView.RPC("FireRPC", RpcTarget.AllBuffered, lFirePosition.position, lFirePosition.eulerAngles);
        }
        
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            photonView.RPC("FireRPC", RpcTarget.AllBuffered, rFirePosition.position, rFirePosition.eulerAngles);
        }
    }

    [PunRPC]
    public void FireRPC(Vector3 firePos, Vector3 fireRot)
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firePos, Quaternion.Euler(fireRot));
        bulletInstance.GetComponent<Bullet>().BulletColor = photonView.IsMine ? Color.green : Color.red;
    }

    [PunRPC]
    public void GetDamageRPC(float damage)
    {
        currHealth -= damage;
        if (currHealth <= 0)
        {
            // dead
            Debug.Log("Dead!");
            StartCoroutine(ShowDead());
            currHealth = maxHealth;
        }
    }

    private IEnumerator ShowDead()
    {
        if (onDeathCanvas == null)
        {
            onDeathCanvas = FindObjectOfType<Canvas>(true).gameObject;
        }
        onDeathCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        onDeathCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
            return;

        if (other.transform.CompareTag("Bullet") && other.transform.GetComponent<Bullet>().BulletColor == Color.red)
        {
            // damage
            photonView.RPC(nameof(GetDamageRPC), RpcTarget.AllBuffered, transform.GetComponent<Bullet>().bulletDamage);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20;
    public float bulletDamage = 25f;

    public Color BulletColor
    {
        get
        {
            if (_isColorSet == false)
            {
                _isColorSet = true;
                _bulletRenderer = GetComponentInChildren<MeshRenderer>();
            }

            return _bulletRenderer.material.color;
        }
        set => _bulletRenderer.material.color = value;
    }
    private MeshRenderer _bulletRenderer;
    private bool _isColorSet = false;
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayerUITest : MonoBehaviour
{
    public Slider bulletSlider;

    private float maxValue;
    private float bulletValue;

    private void OnEnable()
    {
        bulletSlider.maxValue = maxValue;
        bulletValue = maxValue;
    }

    private void Update()
    {
        bulletSlider.value = bulletValue;
    }
}

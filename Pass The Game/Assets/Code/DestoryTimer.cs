using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTimer : MonoBehaviour
{
    public float timer;
    private void OnEnable()
    {
        Destroy(gameObject,timer);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public ParticleSystem pr;

    public void OnPoint()
    {
        pr.Play();
    }
}

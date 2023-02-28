using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGenerator : MonoBehaviour
{

    public GameObject DeadEffect;
    AudioSource deadsound;

    private void Start()
    {
        deadsound = GetComponent<AudioSource>();
    }

    public void GenerateDeadIcon(Vector3 deadpos)
    {
        Instantiate(DeadEffect, deadpos, Quaternion.identity);
        deadsound.Play();
        Destroy(DeadEffect,3f);
    }
}
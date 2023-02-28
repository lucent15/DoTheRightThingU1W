using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "BaseStatus")]

public class BaseStatus : ScriptableObject
{
    [SerializeField]
    public string type;
    [SerializeField]
    public float hp;
    [SerializeField]
    public float atk;
    [SerializeField]
    public float mpvespeed;
    [SerializeField]
    public float atkrate;
}

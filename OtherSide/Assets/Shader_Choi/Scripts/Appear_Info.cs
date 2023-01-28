using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum AppearVec
{
    X, Y, Z
}

public class Appear_Info : MonoBehaviour
{
    public AppearVec appearVec;
    public int Vec;
    public float Distance;
    public float Time;
}

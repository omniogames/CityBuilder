using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ShakeCameraData
{
    public float Intensity;
    public float Time;
    
    public ShakeCameraData(float intensity, float time)
    {
        Intensity = intensity;
        Time = time;
    }
}

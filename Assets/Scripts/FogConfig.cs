using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogConfig : MonoBehaviour
{
    public float FogDensity = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = FogDensity;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.fogDensity = FogDensity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Color fogColor;
    public float fogDensity;
    // Start is called before the first frame update
    void Start()
    {
        fogColor = RenderSettings.fogColor;
        fogDensity = RenderSettings.fogDensity;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boxophobic.StyledGUI;

public class LightManager : MonoBehaviour
{        
    public Color fogColor;
    public float fogDensity;      
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetBool("LightBlend"))
        {
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
        }        
    }

    public void LightBlend()
    {
        anim.SetBool("LightBlend", true);
    }
}



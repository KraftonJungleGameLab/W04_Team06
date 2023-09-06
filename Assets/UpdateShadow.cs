using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLB;

public class UpdateShadow : MonoBehaviour
{
    #region PublicVariables

    #endregion

    #region PrivateVariables
    private VolumetricShadowHD volumetricShadow;
#endregion

#region PublicMethods

#endregion

#region PrivateMethods
    void Start()
    {
        volumetricShadow = GetComponent<VolumetricShadowHD>();
    }
    void Update()
    {
        volumetricShadow.ProcessOcclusionManually();
    }
#endregion
}

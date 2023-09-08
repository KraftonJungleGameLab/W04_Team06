using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsManager : MonoBehaviour
{
    #region PublicVariables

    #endregion

    #region PrivateVariables
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();
    public List<Transform> WayPoints => wayPoints;
#endregion

#region PublicMethods

#endregion

#region PrivateMethods
    void Start()
    {
        
    }
    void Update()
    {
        
    }
#endregion
}

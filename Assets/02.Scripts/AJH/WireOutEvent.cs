using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WireOutEvent : MonoBehaviour
{
    #region PublicVariables

    #endregion

    #region PrivateVariables
    private PlayableDirector playableDirector;
#endregion

#region PublicMethods

#endregion

#region PrivateMethods
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        playableDirector.Play();
    }
    #endregion
}

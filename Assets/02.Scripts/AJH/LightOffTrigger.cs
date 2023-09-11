using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOffTrigger : MonoBehaviour
{
    #region PublicVariables

    #endregion

    #region PrivateVariables
    [SerializeField] List<GameObject> lights = new List<GameObject>();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wire"))
        {
            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].SetActive(false);
            }
        }
    }

    #endregion
}

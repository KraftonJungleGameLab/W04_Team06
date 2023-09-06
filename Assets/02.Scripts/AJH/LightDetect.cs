using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetect : MonoBehaviour
{
    #region PublicVariables
    public Player player;
    #endregion

    #region PrivateVariables
    private List<Renderer> objectsBetween = new List<Renderer>();
    [SerializeField] private GameObject character;
    private Vector3 lightPosition;
    private bool isInLight = false;
#endregion

#region PublicMethods

#endregion

#region PrivateMethods
    void Start()
    {
       player = GetComponent<Player>();
       lightPosition  = this.gameObject.transform.position;
    }
    void Update()
    {
        //CheckObjectsBetween(character.transform.position, lightPosition.position);
        //if (objectsBetween.Count == 0)
        //{
        //    isInLight = true;
        //}
        //else
        //{
        //    isInLight = false;
        //}
        //Debug.Log(isInLight);
        if (isInLight)
        {
            StartCoroutine(iePlayerDamage());
        }
    }
    private IEnumerator iePlayerDamage()
    {
        player.curHp -= 1;
        yield return new WaitForSeconds(1f);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            CheckObjectsBetween(character.transform.position, lightPosition);
            if(objectsBetween.Count == 0){
                isInLight = true;
            }
            else
            {
                isInLight = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isInLight = false;
        }
    }
    private void CheckObjectsBetween(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 rayDirection = endPosition - startPosition;
        Ray ray = new Ray(startPosition, rayDirection);
        float distance = Vector3.Distance(startPosition, endPosition); // 두 지점 사이의 거리 계산
        int layerMask = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ground");
        RaycastHit[] hits = Physics.RaycastAll(ray, distance, ~layerMask);
        Debug.DrawLine(startPosition, endPosition, Color.red);
        Debug.Log("CheckObjects");
        foreach (RaycastHit hit in hits)
        {
            Renderer hitRenderer = hit.collider.GetComponent<Renderer>();
            if (hitRenderer != null)
            {
                objectsBetween.Add(hitRenderer);
            }
        }
    }
    #endregion
}

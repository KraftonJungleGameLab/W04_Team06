using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetect : MonoBehaviour
{
    #region PublicVariables
    public Player player;
    public float damage = 100.0f;
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
        player = GameManager.Instance.player;
       
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
        lightPosition = this.gameObject.transform.position;
        if (isInLight)
        {
            player.curHp -= damage * Time.deltaTime;
            Debug.Log(player.curHp);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if(CheckObjectsBetween(character.transform.position, lightPosition))
            {
                isInLight = true;
                player.isRecoveryOn = false;
                CancelInvoke("PlayerRecoveryOn");
            }
            else
            {
                isInLight = false;
                Invoke("PlayerRecoveryOn", 3.0f);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isInLight = false;
            Invoke("PlayerRecoveryOn", 3.0f);
        }
    }
    private bool CheckObjectsBetween(Vector3 startPosition, Vector3 endPosition)
    {
        startPosition.y += 1.0f;
        Vector3 rayDirection = endPosition - startPosition;
        Ray ray = new Ray(startPosition, rayDirection);
        float distance = Vector3.Distance(startPosition, endPosition); // 두 지점 사이의 거리 계산
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit[] hits = Physics.RaycastAll(ray, distance, layerMask);
        Debug.DrawLine(startPosition, endPosition, Color.red);
        Debug.Log("CheckObjects");

        if (hits.Length == 0)
        {
            return true;
        }

        return false;
    }

    private void PlayerRecoveryOn()
    {
        player.isRecoveryOn = true;
    }
    #endregion
}

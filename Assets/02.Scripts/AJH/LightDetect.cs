using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private DOTweenPath dOTweenPath;
    private Tweener doLookAtPlayer;
    private Quaternion curRotation;
#endregion

    #region PublicMethods

    #endregion


    #region PrivateMethods
    void Start()
    {
        curRotation = transform.rotation;
        player = GameManager.Instance.player;
        dOTweenPath = GetComponent<DOTweenPath>();
    }
    void Update()
    {
        lightPosition = this.gameObject.transform.position;
        if (isInLight)
        {
            player.DamageHealth(damage * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if(CheckObjectsBetween(character.transform.position, lightPosition))
            {
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
    private bool CheckObjectsBetween(Vector3 startPosition, Vector3 endPosition)
    {
        startPosition.y += 1.0f;
        Vector3 rayDirection = endPosition - startPosition;
        Ray ray = new Ray(startPosition, rayDirection);
        float distance = Vector3.Distance(startPosition, endPosition); // 두 지점 사이의 거리 계산
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit[] hits = Physics.RaycastAll(ray, distance, layerMask);
        Debug.DrawLine(startPosition, endPosition, Color.red);

        if (hits.Length == 0)
        {
            return true;
        }
        return false;
    }
    #endregion
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetectFollow : MonoBehaviour
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
    private Coroutine CallDOTWeenPlayCoroutine;
    private bool isCallDOTWeenPlayCoroutineOn;
    #endregion

    #region PublicMethods

    #endregion


    #region PrivateMethods
    void Start()
    {
        curRotation = transform.rotation;
        player = GameManager.Instance.player;
        dOTweenPath = GetComponent<DOTweenPath>();
        isCallDOTWeenPlayCoroutineOn = false;
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

            if (CheckObjectsBetween(character.transform.position, lightPosition))
            {
                isInLight = true;
                StopCallDOTWeenPlayCoroutine();
                if (doLookAtPlayer == null || !doLookAtPlayer.IsPlaying())
                {
                    doLookAtPlayer = transform.DOLookAt(character.transform.position, 0.5f);
                }
                dOTweenPath.DOPause();
            }
            else
            {
                StayCallDOTWeenPlayCoroutine();
                isInLight = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInLight = false;
            StartCallDOTWeenPlayCoroutine();
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

    private IEnumerator CallDOTWeenPlay()
    {
        isCallDOTWeenPlayCoroutineOn = true;
        yield return new WaitForSeconds(3.0f);
        transform.DORotateQuaternion(curRotation, 0.5f);
        dOTweenPath.DOPlay();
        isCallDOTWeenPlayCoroutineOn = false;
    }

    private void StayCallDOTWeenPlayCoroutine()
    {
        if(isInLight)
        {
            StartCallDOTWeenPlayCoroutine();
        }
    }

    private void StartCallDOTWeenPlayCoroutine()
    {
        StopCallDOTWeenPlayCoroutine();
        CallDOTWeenPlayCoroutine = StartCoroutine(CallDOTWeenPlay());
    }

    private void StopCallDOTWeenPlayCoroutine()
    {
        if (isCallDOTWeenPlayCoroutineOn)
            StopCoroutine(CallDOTWeenPlayCoroutine);
    }
    #endregion
}

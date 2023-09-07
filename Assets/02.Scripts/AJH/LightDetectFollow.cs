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
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (CheckObjectsBetween(character.transform.position, lightPosition))
            {
                isInLight = true;
                player.isRecoveryOn = false;
                CancelInvoke("PlayerRecoveryOn");
                StopCoroutine("CallDOTWeenPlay");
                if (doLookAtPlayer == null || !doLookAtPlayer.IsPlaying())
                {
                    doLookAtPlayer = transform.DOLookAt(character.transform.position, 0.5f);
                }
                dOTweenPath.DOPause();
                //CallDOTWeenPauseCoroutine = StartCoroutine(CallDOTWeenPause(character.transform.position));
                //StopAndStartCallDOTWeenPauseCoroutine();
                //if (CallDOTWeenPlayCoroutine != null)
                //    StopCoroutine(CallDOTWeenPlayCoroutine);
            }
            else
            {
                isInLight = false;
                Invoke("PlayerRecoveryOn", 3.0f);
                StartCoroutine("CallDOTWeenPlay");
                //if (CallDOTWeenPlayCoroutine.IsUnityNull())
                //{
                //    Debug.Log(CallDOTWeenPlayCoroutine);
                //    if(CallDOTWeenPauseCoroutine != null)
                //        StopCoroutine(CallDOTWeenPauseCoroutine);
                //    CallDOTWeenPlayCoroutine = StartCoroutine(CallDOTWeenPlay());
                //}
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInLight = false;
            Invoke("PlayerRecoveryOn", 3.0f);
            StartCoroutine("CallDOTWeenPlay");
            //if (CallDOTWeenPauseCoroutine != null)
            //    StopCoroutine(CallDOTWeenPauseCoroutine);
            //StopAndStartCallDOTWeenPlayCoroutine();
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

    private IEnumerator CallDOTWeenPlay()
    {
        yield return new WaitForSeconds(3.0f);
        transform.DORotateQuaternion(curRotation, 0.5f);
        dOTweenPath.DOPlay();
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    #region PublicVariables
    public string childObjectName = "ChasingEnemy";
    #endregion

    #region PrivateVariables

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
        if (other.gameObject.CompareTag("Player"))
        {
            Transform[] childObjects = transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in childObjects)
            {
                if (child.name == childObjectName)
                {
                    Animator animator = child.GetComponent<Animator>();
                    if (animator != null)
                    {
                        // isChasing 파라미터를 true로 설정
                        animator.SetBool("isChasing", true);
                    }
                    else
                    {
                        Debug.LogWarning("자식 오브젝트에 Animator 컴포넌트가 없습니다.");
                    }
                }
            }
        }
    }
    #endregion
}

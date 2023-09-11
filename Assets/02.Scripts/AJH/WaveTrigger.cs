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
                        // isChasing �Ķ���͸� true�� ����
                        animator.SetBool("isChasing", true);
                    }
                    else
                    {
                        Debug.LogWarning("�ڽ� ������Ʈ�� Animator ������Ʈ�� �����ϴ�.");
                    }
                }
            }
        }
    }
    #endregion
}

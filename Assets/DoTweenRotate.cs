using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenRotate : MonoBehaviour
{
    #region PublicVariables

    #endregion

    #region PrivateVariables
    [SerializeField] private float rotationDuration = 2f;
    [SerializeField] private float yRotation = -40f;
    private Quaternion initialRotation;
    #endregion

    #region PublicMethods

    #endregion

    #region PrivateMethods
    void Awake()
    {
        initialRotation = transform.rotation;
    }

    void Start()
    {
        RotateObject();
    }

    void Update()
    {

    }

    private void RotateObject()
    {
        // ��ǥ ȸ���� (y ���� ����)
        Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y + yRotation, initialRotation.eulerAngles.z);

        // ȸ�� �ִϸ��̼� ����
        transform.DORotate(targetRotation.eulerAngles, rotationDuration)
            .SetLoops(-1, LoopType.Yoyo) // ���� �ִϸ��̼��� �����ϰ�, Yoyo�� �����Ͽ� �պ��ϵ��� ��
            .SetEase(Ease.InOutQuad);
    }
    #endregion
}
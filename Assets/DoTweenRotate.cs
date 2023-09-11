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
        // 목표 회전값 (y 값을 설정)
        Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y + yRotation, initialRotation.eulerAngles.z);

        // 회전 애니메이션 설정
        transform.DORotate(targetRotation.eulerAngles, rotationDuration)
            .SetLoops(-1, LoopType.Yoyo) // 루프 애니메이션을 설정하고, Yoyo로 설정하여 왕복하도록 함
            .SetEase(Ease.InOutQuad);
    }
    #endregion
}
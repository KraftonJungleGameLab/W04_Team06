using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WayPointsManager : MonoBehaviour
{
    #region PublicVariables
    [HideInInspector] public Vector3 defaultPosition;
    [HideInInspector] public Animator animator;
    public float damage = 20.0f;
    #endregion

    #region PrivateVariables
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();
    public List<Transform> WayPoints => wayPoints;
    #endregion

    #region PublicMethods
    public void Strike(Object hitPlayerBox)
    {
        Instantiate(hitPlayerBox, transform.position + transform.forward * 1.0f, transform.rotation).GetComponent<HitPlayerBox>().Init(damage);
    }

    public void InitObject()
    {
        transform.position = defaultPosition;
        animator.SetBool("isChasing", false);
        
    }
    #endregion

    #region PrivateMethods

    public void Awake()
    {
        GameManager.Instance.InitAction += InitObject;
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        defaultPosition = transform.position;
    }
    void Update()
    {
        
    }
    #endregion
}

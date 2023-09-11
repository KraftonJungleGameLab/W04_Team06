using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayerBox : MonoBehaviour
{
    #region PublicVariables

    #endregion

    #region PrivateVariables
    private float damage = 20.0f;
    #endregion

    #region PublicMethods
    public void Init(float damage)
    {
        this.damage = damage;
        StartCoroutine(DestroyHitBox(0.2f));
    }

    public IEnumerator DestroyHitBox(float destroySeconds)
    {
        yield return new WaitForSeconds(destroySeconds);
        Destroy(this.gameObject);
    }
    #endregion

    #region PrivateMethods
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.player.DamageHealth(damage);
            Destroy(this.gameObject);
        }
    }
    #endregion
}

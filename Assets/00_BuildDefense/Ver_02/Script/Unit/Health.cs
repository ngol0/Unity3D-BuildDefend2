using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float healthPoints = 10f;

    public System.Action OnDie;

    // public void TakeDamage(GameObject instigator, float damage)
    // {
    //     healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

    //     if (IsDead())
    //     {
    //         onDie.Invoke();
    //         AwardExperience(instigator);
    //     }
    //     else
    //     {
    //         takeDamage.Invoke(damage);
    //     }
    //     UpdateState();
    // }

    public void TakeDamage(GameObject instigator, float damage)
    {
        healthPoints = Mathf.Max(healthPoints - damage, 0);

        if (IsDead())
        {
            OnDie?.Invoke();
        }
        else
        {
            //Debug.Log("invoke some damage event here");
        }
    }

    public bool IsDead()
    {
        return healthPoints <= 0;
    }
}

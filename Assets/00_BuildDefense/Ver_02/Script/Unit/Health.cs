using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float healthPoints = 10f;

    Animator animator;

    public Action OnDie;
    public Action OnClearPath;

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(GameObject instigator, float damage)
    {
        healthPoints = Mathf.Max(healthPoints - damage, 0);

        if (IsDead())
        {
            OnDie?.Invoke();
            animator.SetTrigger("onDie");
        }
    }

    public bool IsDead()
    {
        return healthPoints <= 0;
    }

    public void OnDisappear()
    {
        Destroy(gameObject);
        OnClearPath?.Invoke();
    }
}

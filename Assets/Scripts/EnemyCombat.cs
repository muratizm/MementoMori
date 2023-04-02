using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Animator animator;

    public Transform player;
    public Transform attackPoint;

    public LayerMask playerMask;

    public float attackRange = 2.0f;


    private void Awake()
    {
        animator = GetComponent<Animator>();    
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
    }

    public void Attack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerMask);

        foreach (var hitObj in hitObjects)
        {
            Debug.Log(hitObj.gameObject.name);
            hitObj.GetComponent<PlayerCombat>().TakeDamage(20);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}

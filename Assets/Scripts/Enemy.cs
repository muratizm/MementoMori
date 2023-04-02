using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    private int currentHealth;

    public bool isDead = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        animator.SetBool("isDead", true);
        animator.SetBool("isRunning", false);
        
        animator.Play(gameObject.name + "_Death");
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }

}

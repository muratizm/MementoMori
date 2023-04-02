using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public AudioSource source;
    public Animator animator;
    public AudioClip hurtsound, attacksound, diesound;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;
    public int attackDamage = 20;


    public float attackRate = 2.0f;
    private float nextAttackTime = 0.0f;

    public int maxHealth = 100;
    public int currentHealth;

    public bool isDead = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Time.time > nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        else
        {
            animator.ResetTrigger("Attack");
        }


        
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        source.clip = attacksound;
        source.Play();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyClass = enemy.GetComponent<Enemy>();
            enemyClass.TakeDamage(attackDamage);
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");
        source.clip = hurtsound;
        source.Play();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            isDead = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision object has the tag 'Trap'
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(maxHealth);
        }
    }

    private void Die()
    {
        isDead = true;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        animator.SetBool("isDead", true);
        source.clip = diesound;
        source.Play();



        GameManager gm = FindObjectOfType<GameManager>();
        gm.Invoke("DieGame", 0.75f);

        this.enabled = false;
    }
}

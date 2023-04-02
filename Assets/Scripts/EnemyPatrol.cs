using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Enemy enemy;

    public Transform player;
    public Transform currentPoint;

    public GameObject pointA, pointB;

    private Rigidbody2D rb;

    private Animator animator;

    public float speed = 1.0f;
    public float desiredDistanceToTarget = .5f;

    public float followRange = 4.0f, attackRange = 1.0f;
    public bool inFollowRange, inAttackRange;

    private float animationRate = 20.0f;
    private float nextAnimationTime = 0.0f;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        currentPoint = pointB.transform;
        animator.SetBool("isRunning", true);
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > followRange)
        {
            inFollowRange = false;
            inAttackRange = false;
        }
        else if(distanceToPlayer <= followRange && distanceToPlayer > attackRange)
        {
            inFollowRange = true;
            inAttackRange = false;
        }
        else if(distanceToPlayer <= attackRange)
        {
            inFollowRange =false;
            inAttackRange = true;
        }


        if (!enemy.isDead && !inAttackRange && !inFollowRange)
        {
            PatrolBetweenTwoPoints();
        }
        else if(!enemy.isDead && !inAttackRange && inFollowRange)
        {
            // follow player
            FollowEnemy();
        }
        else if (!enemy.isDead && inAttackRange)
        {
            // attack player
            if (player.GetComponent<PlayerCombat>().enabled)
            {
                
                    animator.SetTrigger("Attack");
                
                
                
            }
            
        }
    }

    private void FollowEnemy()
    {
        currentPoint = null;

        Vector2 point = player.position - transform.position;
        if (point.x < 0.0f)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        else if(point.x > 0.0f)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
        rb.velocity = point;
    }

    private void PatrolBetweenTwoPoints()
    {
        if (currentPoint == null)
        {
            float distanceToPointB = Vector2.Distance(transform.position, pointB.transform.position);
            float distanceToPointA = Vector2.Distance(transform.position, pointA.transform.position);

            float max = Mathf.Max(distanceToPointA, distanceToPointB);

            if (max == distanceToPointB)
            {
                currentPoint = pointB.transform;
            }
            else
            {
                currentPoint = pointA.transform;
            }
        }
        

        Vector2 point = currentPoint.position - transform.position;
        rb.velocity = point;
        if (currentPoint == pointB.transform)
        {
            //rb.velocity = new Vector2(-speed, transform.position.y);
            transform.localScale = new Vector3(3, 3, 3);
        }
        else
        {
            //rb.velocity = new Vector2(speed, transform.position.y);
            transform.localScale = new Vector3(-3, 3, 3);
        }


        if (Vector2.Distance(transform.position, currentPoint.position) < desiredDistanceToTarget && currentPoint == pointB.transform)
        {
            FlipSprite();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < desiredDistanceToTarget && currentPoint == pointA.transform)
        {
            FlipSprite();
            currentPoint = pointB.transform;
        }
    }

    private void FlipSprite()
    {
        
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, .5f);
        Gizmos.DrawWireSphere(pointB.transform.position, .5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}

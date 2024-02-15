using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HellPortalEnemy : Enemy
{
    [SerializeField]
    public int damageToEnemy = 1;

    public float detectionRange = 2f;
    private Transform target;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (!IsAlive() || isInHitstun)
        {
            return;
        }

        if (target == null || Vector2.Distance(transform.position, target.position) > detectionRange)
        {
            Debug.Log("FindingEnemey");
            FindNewTarget();
        }

        // Move towards the target if we have one
        if (target != null)
        {
            Debug.Log("Chasing" + target.gameObject.name);
            Move();
        }
    }

    protected override void Move()
    {
        rb.velocity = movementSpeed * (target.transform.position - transform.position).normalized;
    }

    public override void TakeDamage(int damage, Vector2 direction)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        Debug.Log("Direction: " + direction.x + "," + direction.y);
        StartCoroutine(DoHitStun());
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * 200, ForceMode2D.Impulse);
    }

    protected override void Attack() {}
    protected override void Die() {
        col2D.enabled = false;
        enemyController.OnEnemyDeath(this);
    }

    void FindNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] allTargets = new GameObject[enemies.Length + players.Length];

        // Combine the arrays
        enemies.CopyTo(allTargets, 0);
        players.CopyTo(allTargets, enemies.Length);

        float closestDistance = Mathf.Infinity;
        GameObject closestTarget = null;

        foreach (GameObject potentialTarget in allTargets)
        {
            if (potentialTarget == gameObject) continue; // Skip if it's this enemy itself

            float distance = Vector2.Distance(transform.position, potentialTarget.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = potentialTarget;
            }
        }

        if (closestTarget != null && closestDistance <= detectionRange)
        {
            target = closestTarget.transform;
        }
        else
        {
            target = null;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 knockbackDirection = collision.transform.position - transform.position;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damageToEnemy, knockbackDirection);
        }
    }
}

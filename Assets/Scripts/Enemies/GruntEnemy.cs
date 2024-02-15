using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic enemy, basically follows the player and does a melee attack
public class GruntEnemy : Enemy {
    [SerializeField] GameObject healthPickupPrefab;
    GameObject score;

    private void Start()
    {
        score = GameObject.FindGameObjectWithTag("Score");
    }

    private void Update() {
        if (!IsAlive() || isInHitstun || isSpawning) {
            return;
        }

        // Checks if close enough to player
        float distanceToPlayer = (player.transform.position - transform.position).sqrMagnitude;

        if (distanceToPlayer < attackingDistance) {
            // Attack player
            Attack();
        } else {
            // Move towards player
            Move();
        }
    }

    protected override void Move() {
        rb.velocity = movementSpeed * (player.transform.position - transform.position).normalized;
    }

    public override void TakeDamage(int damage, Vector2 direction)
    {
        AkSoundEngine.PostEvent("PlayerHit", this.gameObject);

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

    protected override void Attack() { }

    protected override void Die() {
        // drop health
        if(Random.Range(0, 20) == 5)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }

        // update score
        score.GetComponent<Score>().SetScore(10);

        // die
        col2D.enabled = false;
        enemyController.OnEnemyDeath(this);
    }
}

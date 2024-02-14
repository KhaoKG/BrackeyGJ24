using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic enemy, basically follows the player and does a melee attack
public class GruntEnemy : Enemy {
    [SerializeField]
    float attackingDistance = 0.5f;

    [SerializeField] GameObject healthPickupPrefab;

    private void Start() {
        // TODO Avoid find object
        player = FindObjectOfType<Player>();
    }

    private void Update() {
        if (!IsAlive() || isInHitstun) {
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
            Instantiate(healthPickupPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    IEnumerator DoHitStun()
    {
        isInHitstun = true;
        Debug.Log("in hitstun");
        yield return new WaitForSeconds(0.25f);
        Debug.Log("out of hitstun");
        isInHitstun = false;
        rb.velocity = Vector2.zero;
    }
}

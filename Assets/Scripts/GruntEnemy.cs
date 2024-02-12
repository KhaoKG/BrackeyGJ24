using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic enemy, basically follows the player and does a melee attack
public class GruntEnemy : Enemy {
    [SerializeField]
    float attackingDistance = 0.5f;

    private void Start() {
        // TODO Avoid find object
        player = FindObjectOfType<Player>();
    }

    private void Update() {
        if (!IsAlive()) {
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

    protected override void Attack() { }
    protected override void Die() {
        Destroy(gameObject);
    }
}

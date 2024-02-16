using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic enemy, basically follows the player and does a melee attack
public class RangerEnemy : Enemy {
    [Header("Ranger specific")]
    [SerializeField] GameObject keyPickupPrefab;

    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    float projectileSpeed = 2f;
    [SerializeField]
    float projectileFiringDuration = 1f;

    [SerializeField]
    float maxAttackingDistance;

    [SerializeField]
    bool movingAway = false;

    private void Update() {
        if (!IsAlive() || isInHitstun || isSpawning || isAttacking) {
            return;
        }

        // Checks if close enough to player
        float distanceToPlayer = (player.transform.position - transform.position).sqrMagnitude;

        if (distanceToPlayer < attackingDistance) {
            // Move away, tries to attack only at mid-range
            movingAway = true;
            Move();
        } else if (distanceToPlayer < maxAttackingDistance) {
            // Attack player
            Attack();
        } else {
            // Move towards player
            movingAway = false;
            Move();
        }
    }

    protected override void Move() {
        if (movingAway) {
            Vector3 outbound = transform.position - player.transform.position;
            // Add a small rotation towards the center of the stage
            Vector3[] possibleDirections = new Vector3[] { 
                new Vector3(-outbound.y, outbound.x),
                new Vector3(outbound.y, -outbound.x), };
            outbound += (possibleDirections[0].sqrMagnitude < possibleDirections[1].sqrMagnitude ?
                possibleDirections[0] : possibleDirections[1]);

            rb.velocity = movementSpeed * outbound.normalized;
        } else {
            rb.velocity = movementSpeed * (player.transform.position - transform.position).normalized;
        }
    }

    public override void TakeDamage(int damage, Vector2 direction)
    {
        AkSoundEngine.PostEvent("playerHit", this.gameObject);

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

    protected override void Attack() {
        // Stop moving to attack
        rb.velocity = Vector2.zero;

        StartCoroutine(PrepareProjectile());
    }

    IEnumerator PrepareProjectile() {
        isAttacking = true;

        yield return new WaitForSeconds(projectileFiringDuration);

        FireProjectile();
        isAttacking = false;
    }

    void FireProjectile() {
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;

        // Generate projectile and configure attributes
        EnemyProjectile projectile = Instantiate(projectilePrefab, 
            transform.position + direction * 0.2f, Quaternion.identity).GetComponent<EnemyProjectile>();
        projectile.Damage = damage;
        projectile.Speed = direction * projectileSpeed;
    }

    protected override void Die() {
        // drop Key
        if(Random.Range(0, 20) > -1)
        {
            Instantiate(keyPickupPrefab, transform.position, Quaternion.identity);
            AkSoundEngine.PostEvent("keyDropped", this.gameObject);
        }

        // die
        col2D.enabled = false;
        enemyController.OnEnemyDeath(this);
    }

    private void OnValidate() {
        if (maxAttackingDistance <= attackingDistance) {
            Debug.LogError("Max attacking distance has to be higher than attacking distance!");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door Ability")
        {
            // Get knockback direction
            Vector2 knockbackDirection = transform.position - collision.transform.position;
            TakeDamage(collision.gameObject.GetComponent<DoorDamage>().doorDamage, knockbackDirection.normalized);
        }
    }
}

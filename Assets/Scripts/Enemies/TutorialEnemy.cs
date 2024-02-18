using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic enemy, basically follows the player and does a melee attack
public class TutorialEnemy : Enemy {

    private void Update() {
        if (!IsAlive() || isInHitstun || isSpawning) {
            return;
        }
        Move();
    }

    public override void TakeDamage(int damage, Vector2 direction)
    {
        AkSoundEngine.PostEvent("playerHit", this.gameObject);

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        //Debug.Log("Direction: " + direction.x + "," + direction.y);
        StartCoroutine(DoHitStun());
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * 200, ForceMode2D.Impulse);
    }

    protected override void Attack() { }

    protected override void Move()
    {
        rb.velocity = Vector2.zero;
    }

    protected override void Die() {

        // die
        col2D.enabled = false;
        //enemyController.OnEnemyDeath(this);
        Destroy(gameObject);
    }
}

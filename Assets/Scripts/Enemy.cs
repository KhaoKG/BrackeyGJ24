using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;

    [SerializeField]
    protected float movementSpeed;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected Rigidbody2D rb;

    // Every enemy keeps track of where the player is
    protected Player player;

    protected bool isInHitstun;

    protected abstract void Move();
    protected abstract void Attack();
    public virtual void TakeDamage(int damage, Vector2 direction) {
        health -= damage;

        if (health <= 0) {
            Die();
        }

        rb.velocity = Vector2.zero;
        rb.AddForce(direction * 250);
    }
    protected abstract void Die();

    protected bool IsAlive() {
        return health > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Attack")
        {
            Vector2 knockbackDirection = transform.position - collision.transform.position;
            TakeDamage(collision.gameObject.GetComponent<PlayerPunch>().punchDamage, knockbackDirection.normalized);
        }
    }
}

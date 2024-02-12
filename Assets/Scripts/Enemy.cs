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

    protected abstract void Move();
    protected abstract void Attack();
    public virtual void TakeDamage(int damage) {
        health -= damage;

        if (health <= 0) {
            Die();
        }
    }
    protected abstract void Die();

    protected bool IsAlive() {
        return health > 0;
    }
}

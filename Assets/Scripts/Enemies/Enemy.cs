using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Common attributes")]
    [SerializeField]
    protected int health;

    [SerializeField]
    protected float movementSpeed;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected float attackingDistance = 0.5f;

    [Header("Components")]
    [SerializeField]
    protected Rigidbody2D rb;

    [SerializeField]
    protected Collider2D col2D;

    protected EnemyController enemyController;

    // Every enemy keeps track of where the player is
    protected Player player;

    [Header("Hitstun")]
    [SerializeField]
    protected bool isInHitstun;
    [SerializeField]
    protected float hitstunDuration;

    protected bool isSpawning;

    protected bool isAttacking = false;

    public EnemyController EnemyController { get => enemyController; set => enemyController = value; }
    public Player Player { get => player; set => player = value; }

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

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
    }

    private void OnEnable() {
        isSpawning = true;
        col2D.enabled = false;
        gameObject.AddComponent<EnemySpawnEffect>();
    }

    public void OnSpawnReady() {
        isSpawning = false;
        col2D.enabled = true;
    }

    protected bool IsAlive() {
        return health > 0;
    }

    protected IEnumerator DoHitStun() {
        isInHitstun = true;
        Debug.Log("in hitstun");
        yield return new WaitForSeconds(hitstunDuration);
        Debug.Log("out of hitstun");
        isInHitstun = false;
        rb.velocity = Vector2.zero;
    }
}

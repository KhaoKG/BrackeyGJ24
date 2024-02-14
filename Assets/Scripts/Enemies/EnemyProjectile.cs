using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    int damage;

    [SerializeField]
    Vector2 speed;

    Rigidbody2D rb;

    public int Damage { get => damage; set => damage = value; }
    public Vector2 Speed { get => speed; set => speed = value; }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        rb.velocity = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage, (collision.transform.position - transform.position).normalized);

            // Disappear after hit
            Disappear();
        } else if (collision.CompareTag("MapLimits")) {
            Disappear();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
    }

    void Disappear() {
        Destroy(gameObject);
    }
}

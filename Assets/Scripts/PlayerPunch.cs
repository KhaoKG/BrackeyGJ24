using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    int punchDamage = 1;

    CameraShake shakeEffect;

    private void Start() {
        shakeEffect = player.ShakeEffect;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;

            // Add an influence from the player slam direction
            knockbackDirection += ((Vector2)(transform.position - player.transform.position)).normalized;

            collision.gameObject.GetComponent<Enemy>().TakeDamage(punchDamage, knockbackDirection.normalized);

            // Shake camera
            shakeEffect.Shake(4f, 0.3f);
        }
    }
}

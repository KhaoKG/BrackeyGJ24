using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    public int punchDamage = 1;

    private void OnTriggerEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy")) {
            Vector2 knockbackDirection = collision.transform.position - transform.position;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(punchDamage, knockbackDirection);
        }
    }
}

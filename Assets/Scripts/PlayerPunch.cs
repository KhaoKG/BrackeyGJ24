using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [SerializeField]
    Player player;

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(player.PunchDamage);
        }
    }
}

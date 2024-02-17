using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBottomDoor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    float transparentAlpha = 0.4f;

    void Start()
    {
        // Get the SpriteRenderer component attached to this wall
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the object colliding with the wall is the player
        if (collision.CompareTag("Player")) {
            // Lower the alpha value to, for example, 0.5 to make the wall semi-transparent
            Color color = spriteRenderer.color;
            color.a = transparentAlpha;
            spriteRenderer.color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        // Check if the object leaving the wall's collider is the player
        if (collision.CompareTag("Player")) {
            // Reset the alpha value to 1 to make the wall fully opaque
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }
}

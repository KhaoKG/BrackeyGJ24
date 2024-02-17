using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBottomDoor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component attached to this wall
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object colliding with the wall is the player
        if (collision.gameObject.tag == "Player")
        {
            // Lower the alpha value to, for example, 0.5 to make the wall semi-transparent
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the object leaving the wall's collider is the player
        if (collision.gameObject.tag == "Player")
        {
            // Reset the alpha value to 1 to make the wall fully opaque
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }
}

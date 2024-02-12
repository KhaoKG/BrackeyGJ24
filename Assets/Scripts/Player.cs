using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    int health = 10;

    [Header("Movement")]
    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField]
    Rigidbody2D rb;
    Vector2 moveDirection;
    [SerializeField]
    bool isInHitstun = false;

    private void Update() {
        if (!IsAlive()) {
            return;
        }

        if (!isInHitstun) {
            Run();
            FlipSprite();
        }

        rb.AddForce(moveDirection * moveSpeed);
    }

    void OnFire(InputValue value) {

    }

    void OnMove(InputValue value) {
        moveDirection = value.Get<Vector2>();
    }

    void Run() {
        rb.velocity = moveSpeed * moveDirection;
    }

    void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    public bool IsAlive() {
        return health > 0;
    }
}

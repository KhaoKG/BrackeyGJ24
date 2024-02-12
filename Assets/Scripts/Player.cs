using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    int health = 10;

    [SerializeField]
    int punchDamage = 1;

    [SerializeField]
    Animator animator;

    [Header("Door related")]
    [SerializeField]
    bool isHoldingDoor = true;
    //[SerializeField]
    //GameObject doorHeld = null;
    //[SerializeField]
    //bool isNearDoor = false;
    //[SerializeField]
    //GameObject doorNearby = null;

    [Header("Attack")]
    [SerializeField]
    GameObject attackParent; // this is the parent of the attack hitbox. This object is rotated towards the mouse so the attackalways happens in the direction of the mouse


    [Header("Movement")]
    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField]
    Rigidbody2D rb;
    Vector2 moveDirection;
    [SerializeField]
    bool isInHitstun = false;

    public int PunchDamage { get => punchDamage; set => punchDamage = value; }

    private void Update() {
        if (!IsAlive()) {
            return;
        }

        if (!isInHitstun) {
            Run();
            //FlipSprite(); commented out until we fix it so it flips just the sprite and not the whole character
        }

        rb.AddForce(moveDirection * moveSpeed);
    }

    private void FixedUpdate()
    {
        
    }

    void OnAttack(InputValue value) {
        if (isHoldingDoor) {
            
        }
        else {
            
        }
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

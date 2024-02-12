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
    bool isHoldingDoor = false;
    [SerializeField]
    GameObject doorHeld = null;
    [SerializeField]
    bool isNearDoor = false;
    [SerializeField]
    GameObject doorNearby = null;

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
            FlipSprite();
        }

        rb.AddForce(moveDirection * moveSpeed);
    }

    void OnAttack(InputValue value) {
        if (isHoldingDoor) {
            doorHeld.transform.RotateAround(transform.position, Vector3.forward, -90f * transform.localScale.x);
            StartCoroutine(RotateDoorBack());
        }
        else {
            // TODO Remove test punch attack
            GameObject punch = transform.GetChild(0).gameObject;

            // If punch not active
            if (!punch.activeSelf) {
                punch.SetActive(true);
                StartCoroutine(DisappearPunch());
            }
        }
    }

    // TODO Remove test "attack end"
    IEnumerator DisappearPunch() {
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // TODO Remove test "door attack end"
    IEnumerator RotateDoorBack() {
        yield return new WaitForSeconds(0.1f);
        doorHeld.transform.RotateAround(transform.position, Vector3.forward, 90f * transform.localScale.x);
    }

    void OnGrabDoor(InputValue value) {
        // Grab door if nearby
        if (isNearDoor) {
            doorHeld = doorNearby;
            doorHeld.transform.parent = transform;
            doorHeld.transform.localPosition = Vector3.up * 1.2f;

            // Prepare door as weapon
            doorHeld.tag = "Player";
            doorHeld.layer = LayerMask.NameToLayer("PlayerAttack");
            doorHeld.GetComponent<BoxCollider2D>().isTrigger = false;

            isHoldingDoor = true;
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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Door")) {
            isNearDoor = true;
            doorNearby = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Door")) {
            isNearDoor = false;
            doorNearby = null;
        }
    }
}

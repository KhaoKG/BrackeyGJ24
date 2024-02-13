using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    int maxHealth = 10;
    int health;
    [SerializeField]
    Image healthBar;

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

    [Header("Combat")]
    [SerializeField]
    GameObject attackParent; // this is the parent of the attack hitbox. This object is rotated towards the mouse so the attackalways happens in the direction of the mouse
    [SerializeField] float knockbackForce;
    [SerializeField] float knockbackDuration;


    [Header("Movement")]
    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField]
    Rigidbody2D rb;
    Vector2 moveDirection;
    [SerializeField]
    bool isInHitstun = false;
    SpriteRenderer sr;

    //public int PunchDamage { get => punchDamage; set => punchDamage = value; }

    private void Start()
    {
        health = maxHealth;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (!IsAlive()) {
            return;
        }

        if (!isInHitstun) {
            Run();
            //FlipSprite(); commented out until we fix it so it flips just the sprite and not the whole character
        }
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

    void TakeDamage(int damage, Vector2 direction)
    {
        // update health
        health -= damage;

        Debug.Log("Took Damage, health is now " + health);

        // update UI
        healthBar.fillAmount = ((float)health / maxHealth);

        // knockback
        StartCoroutine(DoHitStun());
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce);
    }

    IEnumerator DoHitStun()
    {
        isInHitstun = true;
        float knockbackDurationFraction = knockbackDuration / 6;

        for(int i=0; i<3; i++)
        {
            sr.color = Color.clear;
            yield return new WaitForSeconds(knockbackDurationFraction);
            sr.color = Color.white;
            yield return new WaitForSeconds(knockbackDurationFraction);
        }

        isInHitstun = false;
        rb.velocity = Vector2.zero;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && !isInHitstun)
        {
            // Get knockback direction
            Vector2 knockbackDirection = transform.position - collision.transform.position;
            TakeDamage(1, knockbackDirection.normalized);
        }
    }
}

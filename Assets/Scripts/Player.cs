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
    [SerializeField]
    SpriteRenderer spriteRenderer;

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
    [SerializeField] CameraShake shakeEffect;

    [Header("Movement")]
    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField]
    Rigidbody2D rb;
    Vector2 moveDirection;
    [SerializeField]
    bool isInHitstun = false;
    float initialSpeed = 1f;

    [Header("Dash")]
    [SerializeField]
    float dashSpeed;
    [SerializeField] 
    float dashLength = .5f;
    [SerializeField]
    float dashCooldown = 1f;
    private float dashCounter, dashCoolCounter;

    public CameraShake ShakeEffect { get => shakeEffect; set => shakeEffect = value; }

    //public int PunchDamage { get => punchDamage; set => punchDamage = value; }

    private void Start()
    {
        health = maxHealth;
        initialSpeed = moveSpeed;
    }

    private void Update() {
        if (!IsAlive()) {
            return;
        }

        if (!isInHitstun) {
            Run();
            FlipSprite(); 
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter < 0)
            {
                moveSpeed = initialSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    void OnAttack(InputValue value) {
        animator.SetTrigger("Attack");
    }

    void OnMove(InputValue value) {
        moveDirection = value.Get<Vector2>();
    }

    void OnDash(InputValue value)
    {
        if (dashCoolCounter <= 0 && dashCounter <= 0)
        {
            moveSpeed = dashSpeed;
            dashCounter = dashLength;
        }
    }

    void Run() {
        rb.velocity = moveSpeed * moveDirection;

        animator.SetBool("IsMoving", rb.velocity != Vector2.zero);
    }

    void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed) {
            //transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            spriteRenderer.flipX = rb.velocity.x >= 0;
        }
    }

    public bool IsAlive() {
        return health > 0;
    }

    public void TakeDamage(int damage, Vector2 direction)
    {

        // play sound
        AkSoundEngine.PostEvent("playerHurt", this.gameObject);

        // update health
        health -= damage;

        // update UI
        healthBar.fillAmount = ((float)health / maxHealth);

        // Shake camera
        shakeEffect.Shake(damage*2, 0.5f);

        // knockback
        StartCoroutine(DoHitStun());
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

    void HealDamage(int healAmount)
    {
        // play sound
        AkSoundEngine.PostEvent("PlayerHealthUp", this.gameObject);

        // update health
        health += healAmount;

        // check for max health
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        // update UI
        healthBar.fillAmount = ((float)health / maxHealth);
    }

    public void EnableInput() {
        GetComponent<PlayerInput>().ActivateInput();
    }

    public void DisableInput() {
        GetComponent<PlayerInput>().DeactivateInput();
    }

    IEnumerator DoHitStun()
    {
        isInHitstun = true;
        float knockbackDurationFraction = knockbackDuration / 6;

        for(int i=0; i<3; i++)
        {
            spriteRenderer.color = Color.clear;
            yield return new WaitForSeconds(knockbackDurationFraction);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(knockbackDurationFraction);
        }

        isInHitstun = false;
        rb.velocity = Vector2.zero;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag is "Enemy" or "PortalEnemy" or "Door Ability" && !isInHitstun)
        {
            // Get knockback direction
            Vector2 knockbackDirection = transform.position - collision.transform.position;
            TakeDamage(1, knockbackDirection.normalized);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Health"))
        {
            HealDamage(1);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Door Ability")
        {
            // Get knockback direction
            Vector2 knockbackDirection = transform.position - collision.transform.position;
            TakeDamage(1, knockbackDirection.normalized);
        }
    }
}

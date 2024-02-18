using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Health")]
    public GameStateSO gameState;

    [SerializeField] public int maxHealth;
    public int health;
    
    [SerializeField]
    Image healthBar;

    [Header("Components")]
    [SerializeField]
    Animator animator;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    SpriteBlinkEffect blinkEffect;
    [SerializeField]
    GameStateManager gameStateManager;

    [Header("Combat")]
    [SerializeField]
    GameObject attackParent; // this is the parent of the attack hitbox. This object is rotated towards the mouse so the attackalways happens in the direction of the mouse
    [SerializeField] float knockbackForce;
    [SerializeField] float knockbackDuration;
    [SerializeField] CameraShake shakeEffect;
    public int doorDamage = 1;

    [Header("Movement")]
    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField]
    Rigidbody2D rb;
    Vector2 moveDirection;
    [SerializeField]
    bool isInHitstun = false;
    float initialSpeed = 1f;
    public bool facingLeft = true;
    bool flipping = false;

    [Header("Dash")]
    [SerializeField]
    float dashSpeed;
    [SerializeField] 
    float dashLength = .5f;
    [SerializeField]
    float dashCooldown = 1f;
    private float dashCounter, dashCoolCounter;

    float iframes = 0f;

    public CameraShake ShakeEffect { get => shakeEffect; set => shakeEffect = value; }

    //public int PunchDamage { get => punchDamage; set => punchDamage = value; }

    private void Start()
    {
        maxHealth = gameState.maxHealth;
        health = maxHealth;
        initialSpeed = moveSpeed;
    }

    private void Update() {
        if (!IsAlive()) {
            return;
        }

        if (!isInHitstun) {
            Run();
            //FlipSprite(); 
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
        if (gameStateManager.IsPaused() || !IsAlive()) {
            return;
        }

        animator.SetTrigger("Attack");
    }

    void OnMove(InputValue value) {
        if (gameStateManager.IsPaused() || !IsAlive()) {
            return;
        }

        moveDirection = value.Get<Vector2>();
    }

    void OnDash(InputValue value) {
        if (gameStateManager.IsPaused() || !IsAlive()) {
            return;
        }

        if (dashCoolCounter <= 0 && dashCounter <= 0)
        {
            moveSpeed = dashSpeed;
            dashCounter = dashLength;
        }
    }

    void OnPause(InputValue value) {
        gameStateManager.ProcessPause();
    }

    void Run() {
        rb.velocity = moveSpeed * moveDirection;

        animator.SetBool("IsMoving", rb.velocity != Vector2.zero);
    }

    public void FlipSprite() {
        //bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (flipping) return;

        StartCoroutine(DoFlipSprite(spriteRenderer.transform));

        //if ((rb.velocity.x > 0.1f && facingLeft) || (rb.velocity.x < -0.1f && !facingLeft)) {
            //transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            //spriteRenderer.flipX = rb.velocity.x >= 0;
            //StartCoroutine(DoFlipSprite(spriteRenderer.transform));
        //}
    }

    IEnumerator DoFlipSprite(Transform sprite)
    {
        flipping = true; // semaphore

        // calc the direction of flip
        float currentScale = sprite.localScale.x;
        float goalScale = currentScale * -1;
        float increment = 0f;
        if (currentScale > 0) increment = -0.2f;
        else if (currentScale < 0) increment = 0.2f;

        // do the flip
        for (int i = 0; i < 10; i++)
        {
            sprite.localScale = new Vector3(sprite.localScale.x + increment, 1, 1);
            yield return new WaitForSeconds(0.02f);
        }

        // update direction
        facingLeft = !facingLeft;

        flipping = false;
    }

    private void FixedUpdate()
    {
        if(iframes >= 0)
        {
            iframes--;
        }
    }

    public bool IsAlive() {
        return health > 0;
    }

    public void TakeDamage(int damage, Vector2 direction)
    {
        // set hurt animation
        animator.SetTrigger("Hurt");

        // check i-frames
        if (iframes > 0)
        {
            return;
        }

        // set i-frames
        iframes = 60f;

        // play sound
        AkSoundEngine.PostEvent("playerHurt", this.gameObject);

        // update health
        health -= damage;

        // update UI
        healthBar.fillAmount = ((float)health / maxHealth);

        if (IsAlive()) {
            // Still alive after hit

            // Shake camera
            shakeEffect.Shake(damage * 2, 0.5f);

            // knockback
            StartCoroutine(SufferHitStun());
            rb.velocity = Vector2.zero;
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        } else {
            Die();

            // Prepare game over
            gameStateManager.GameOver();
        }
    }

    void Die() {
        // TODO Death animation
        spriteRenderer.color = Color.clear;

        // Disable script
        enabled = false;

        // Disable collider and velocity
        GetComponent<Collider2D>().enabled = false;
        rb.velocity = Vector2.zero;
    }

    void HealDamage(int healAmount)
    {
        // play sound
        AkSoundEngine.PostEvent("PlayerEatCrayon", this.gameObject);

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
        GetComponentInChildren<PlayerMelee>().enabled = true;
    }

    public void DisableInput() {
        GetComponent<PlayerInput>().DeactivateInput();
        GetComponentInChildren<PlayerMelee>().enabled = false;
    }

    IEnumerator SufferHitStun()
    {
        isInHitstun = true;

        blinkEffect.StartBlinking();

        yield return new WaitForSeconds(knockbackDuration);

        blinkEffect.StopBlinking();

        isInHitstun = false;
        rb.velocity = Vector2.zero;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy")) {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.IsAlive()) {
                // Get knockback direction
                Vector2 knockbackDirection = transform.position - collision.transform.position;
                //Debug.Log("Collided with enemy");
                TakeDamage(enemy.Damage, knockbackDirection.normalized);
            }

        }
        else if(collision.gameObject.tag is "PortalEnemy" or "Door Ability" && !isInHitstun)
        {
            // Get knockback direction
            Vector2 knockbackDirection = transform.position - collision.transform.position;
            //Debug.Log("Collided with enemy");
            TakeDamage(collision.transform.parent.GetComponent<DoorDamage>().doorDamage, knockbackDirection.normalized);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Health"))
        {
            HealDamage(1);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Door Ability"))
        {
            // Get knockback direction
            Vector2 knockbackDirection = transform.position - collision.transform.position;
            TakeDamage(collision.transform.parent.GetComponent<DoorDamage>().doorDamage, knockbackDirection.normalized);
        }
        else if (collision.CompareTag("Key Pickup"))
        {
            if(AbilityController.Instance.availableAbilitiesForRound.Count >= 4)
            {
                return;
            }
            else
            {
                AbilityController.Instance.AddAbilityForRound(collision.gameObject.GetComponent<KeyPickup>().keyType);
                AkSoundEngine.PostEvent("playerKeyGain", this.gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}

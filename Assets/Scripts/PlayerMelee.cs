using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    bool inAttack = false; // true if attack animation is currently playing
    [SerializeField]
    Animator attackAnimator; // animator sitting on the actual attack object
    [SerializeField]
    float attackCooldown = 0.1f; // how long before you can melee again
    [SerializeField]
    GameObject attackHitbox;

    // animation variables
    [SerializeField] Animator animator; // player animator
    [SerializeField] Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // CHECK FOR ATTACK BUTTON PRESSED
        if(Input.GetMouseButtonDown(0) && !inAttack) // can't attack if already attacking
        {
            StartCoroutine(DoAttack());
        }

        if (inAttack)
            return;

        // ROTATE ATTACK TOWARD MOUSE
        // Get screen position of the object
        Vector2 screenPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Get screen position of mouse
        Vector2 mousePosition = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        // maths
        float angle = AngleBetweenTwoPoints(screenPosition, mousePosition);

        // rotate
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));


        // USE THE CORRECT ANIMATION
        // flip walk
        if((player.facingLeft && mousePosition.x > screenPosition.x) || (!player.facingLeft && mousePosition.x < screenPosition.x))
        {
            Debug.Log("mouse: " + mousePosition.x);
            Debug.Log("player: " + player.transform.position.x);
            player.FlipSprite();
        }

        // oh boy here we go
        float offset = mousePosition.y - screenPosition.y;
        if(offset > 20f)
        {
            animator.SetTrigger("Top");
        }
        else if(offset < 20f && offset > 5f)
        {
            animator.SetTrigger("TopMiddle");
        }
        else if (offset < 5f && offset > -5f)
        {
            animator.SetTrigger("Middle");
        }
        else if (offset < -5f && offset > -20f)
        {
            animator.SetTrigger("BottomMiddle");
        }
        else if (offset > 20f)
        {
            animator.SetTrigger("Bottom");
        }

    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    IEnumerator DoAttack()
    {
        AkSoundEngine.PostEvent("playerSwing", this.gameObject); // play sound
        attackHitbox.SetActive(true);
        inAttack = true;
        attackAnimator.SetTrigger("attack");

        yield return new WaitForSeconds(attackCooldown);

        inAttack = false;
        attackHitbox.SetActive(false);
    }
}

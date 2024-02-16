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

    Vector2 screenPosition;
    Vector2 mousePosition;

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
        screenPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Get screen position of mouse
        mousePosition = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        // maths
        float angle = AngleBetweenTwoPoints(screenPosition, mousePosition);

        // rotate
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));


        // USE THE CORRECT ANIMATION
        // flip walk
        if((player.facingLeft && mousePosition.x > screenPosition.x) || (!player.facingLeft && mousePosition.x < screenPosition.x))
        {
            player.FlipSprite();
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

        // oh boy here we go
        float offset = mousePosition.y - screenPosition.y;
        if (offset > 0.03f)
        {
            animator.SetTrigger("Top");
        }
        else if (offset < 0.03f && offset > 0.025f)
        {
            animator.SetTrigger("TopMiddle");
        }
        else if (offset < 0.025f && offset > -0.025f)
        {
            animator.SetTrigger("Middle");
        }
        else if (offset < -0.025f && offset > -0.03f)
        {
            animator.SetTrigger("BottomMiddle");
        }
        else if (offset < -0.03f)
        {
            animator.SetTrigger("Bottom");
        }
        else
        {
            Debug.Log("error: could not find attack direction");
            Debug.Log("offset: " + offset);
        }

        yield return new WaitForSeconds(attackCooldown);

        inAttack = false;
        attackHitbox.SetActive(false);
    }
}

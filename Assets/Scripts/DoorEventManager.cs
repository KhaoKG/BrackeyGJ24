using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DoorEventManager : MonoBehaviour
{
    Vector3 homePosition; // the position the door goes to during a door event
    SpriteRenderer sr;

    [Header("Cooldown")]
    float doorEventTimer = 1.0f; // the time for a door event
    float maxDoorEventTimer = 1.0f; // upper limit of the timer
    [SerializeField] Image CooldownIndicator;
    bool onCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        homePosition = transform.position; // save this position for when the door returns here
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false; // start as invisible
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && !onCooldown)
        {
            // Trigger Door Event
            onCooldown = true;
            sr.enabled = true;
            sr.enabled = false;
        }
        else if (onCooldown)
        {
            // Update Timer
            doorEventTimer -= Time.deltaTime; // tick the timer
            CooldownIndicator.fillAmount = doorEventTimer; // update the UI

            // Check for Cooldown Completion
            if (doorEventTimer <= 0)
            {
                // timer ended, reset timer
                doorEventTimer = maxDoorEventTimer;
                CooldownIndicator.fillAmount = maxDoorEventTimer;
                onCooldown = false;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DoorEventManager : MonoBehaviour
{
    Vector3 homePosition; // the position the door goes to during a door event
    public int DoorId; // 1 left 2 top 3 right
    private Camera mainCamera;

    public bool isUsingAbility = false;
    private bool isInCameraView = false; //Only allow ability use if you can see the door

    public static event Action<GameObject> ActivateDoor;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        homePosition = transform.position; // save this position for when the door returns here
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null)
        {
            // Convert the object's position to viewport space using the main camera
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

            // Check if the object is within the viewport
            if (viewportPosition.x > 0 && viewportPosition is { x: < 1, y: > 0 } and { y: < 1, z: > 0 })
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    if (isUsingAbility) return;
                    ActivateDoor?.Invoke(gameObject);
                }
            }
            else
            {
                //Debug.Log($"The object {DoorId} is outside the Cinemachine camera's view.");
            }
        }
    }
}

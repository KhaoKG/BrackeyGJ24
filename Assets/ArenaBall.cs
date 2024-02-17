using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBall : MonoBehaviour
{
    Vector3 targetLocation;
    Vector3 startingLocation;

    [SerializeField]
    float speed = 3f;
    float distanceElapsed = 0f;
    [SerializeField]
    float xMagnitude = 20f;
    [SerializeField]
    float yMagnitude = 14f;

    private void Start() {
        // Choose random location
        ChooseTargetLocation();
    }

    // Update is called once per frame
    void Update()
    {
        distanceElapsed = Mathf.MoveTowards(distanceElapsed, 1f, speed * Time.deltaTime);

        transform.position = CalculateBezierCurve(distanceElapsed, 
            startingLocation, startingLocation + new Vector3(1, 1, 0),
            targetLocation - new Vector3(1, 1, 0), targetLocation);

        if (distanceElapsed == 1f) {
            ChooseTargetLocation();
        }
    }

    void ChooseTargetLocation() {
        startingLocation = transform.position;

        float posX = Random.Range(-xMagnitude, xMagnitude);
        float posY;
        // Calculate y position depending on x chosen
        if (Mathf.Abs(posX) < 0.6f * xMagnitude) {
            posY = Random.Range(.6f, 1f) * (Random.Range(0, 2) < 1? yMagnitude : -yMagnitude);
        } else {
            posY = Random.Range(-yMagnitude, yMagnitude);
        }
        targetLocation = new Vector3(posX, posY);

        // Restart distance
        distanceElapsed = 0f;
    }

    Vector3 CalculateBezierCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) {
        float t2 = t * t;
        float t3 = t2 * t;

        return p0 * (-t3 + 3 * t2 - 3 * t + 1) + p1 * (3 * t3 - 6 * t2 + 3 * t) + p2 * (-3 * t3 + 3 * t2) + p3 * t3;
    }
}

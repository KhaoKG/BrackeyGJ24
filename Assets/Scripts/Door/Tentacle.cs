using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    private void Start() {
        // Check which direction should it swing
        // Default is from the right side
        if (transform.position.x == 0) {
            if (transform.position.y > 0) {
                // From top
                transform.parent.Rotate(Vector3.forward * 90f);
            } else {
                // From bottom
                transform.parent.Rotate(Vector3.forward * 270f);
            }
        } else if (transform.position.x < 0) {
            // From left
            transform.parent.Rotate(Vector3.forward * 180f);
        }

    }

    public void Disappear() {
        transform.parent.GetComponent<ParticleSystem>().Stop();
    }
}

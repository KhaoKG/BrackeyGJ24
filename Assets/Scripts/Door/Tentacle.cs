using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    PolygonCollider2D col2D;

    private void Start() {
        AkSoundEngine.PostEvent("doorTentacleWhoosh", this.gameObject);

    }

    // Check which direction should it swing
    public void RotateAccordingToDoor(int doorId) {
        // Considering clockwise with 0 = bottom and 3 = right
        transform.parent.Rotate(Vector3.forward * -90f * doorId);
    }

    public void UpdatePhysicsCollider() {
        List<Vector2> path = new List<Vector2>();
        spriteRenderer.sprite.GetPhysicsShape(0, path);
        col2D.SetPath(0, path.ToArray());
    }

    public void Disappear() {
        AkSoundEngine.PostEvent("doorTentacleSlam", this.gameObject);
        transform.parent.GetComponent<ParticleSystem>().Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    PolygonCollider2D col2D;
    TentacleAbility ability;

    public TentacleAbility Ability { get => ability; set => ability = value; }

    private void Start() {
        //AkSoundEngine.PostEvent("doorTentacleSwing", this.gameObject);

    }

    private void FixedUpdate() {
        UpdatePhysicsCollider();
    }

    // Check which direction should it swing
    public void RotateAccordingToDoor(int doorId) {
        // Considering clockwise with 0 = bottom and 3 = right
        transform.parent.Rotate(Vector3.forward * -90f * doorId);
    }

    void UpdatePhysicsCollider() {
        List<Vector2> path = new List<Vector2>();
        spriteRenderer.sprite.GetPhysicsShape(0, path);
        col2D.SetPath(0, path.ToArray());
    }

    public void Slam() {
        AkSoundEngine.PostEvent("doorTentacleSlam", this.gameObject);
    }

    public void Swing()
    {
        AkSoundEngine.PostEvent("doorTentacleSwing", this.gameObject);
    }

    public void Disappear() {
        transform.parent.GetComponent<ParticleSystem>().Stop();
        spriteRenderer.color = Color.clear;
    }

    private void OnDestroy() {
        // Deactivate ability
        ability.Deactivate(ability.Door);
    }
}

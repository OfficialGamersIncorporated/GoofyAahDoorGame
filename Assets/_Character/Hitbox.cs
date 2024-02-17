using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

    Collider2D hitTrigger;

    public float Damage = 1;
    public float PhysicsImpulse = 20;
    public bool Continuous = false;
    public bool HitEnemies = false;
    public bool HitAllies = true;

    void ProcessCollision(Collider2D other) {
        if(other.CompareTag("Enemy") && !HitEnemies) return;
        if(other.CompareTag("Player") && !HitAllies) return;

        Rigidbody2D otherRB = other.GetComponent<Rigidbody2D>();
        Vector3 toOtherVector = other.transform.position - transform.position;
        if(otherRB)
            otherRB.AddForce(toOtherVector.normalized * PhysicsImpulse, ForceMode2D.Impulse);

        // damage should be done after the physics impulse so gibs can inherit their parents velocity after being pushed back.
        Health healthComp = other.GetComponent<Health>();
        if(Damage != 0 && healthComp && healthComp.transform != transform.parent)
            healthComp.ChangeHealth(-Damage);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(!Continuous) return;

        ProcessCollision(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(!Continuous) return;

        ProcessCollision(collision.collider);
    }
    private void OnEnable() {
        if(Continuous) return;

        hitTrigger = GetComponent<Collider2D>();

        List<Collider2D> overlappingColliders = new List<Collider2D>();
        hitTrigger.Overlap(overlappingColliders);

        foreach(Collider2D other in overlappingColliders) {
            ProcessCollision(other);
        }
    }
}

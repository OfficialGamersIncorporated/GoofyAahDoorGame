using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

    Collider2D hitTrigger;

    public float Damage = 1;
    public float PhysicsImpulse = 20;
    public bool HitEnemies = false;
    public bool HitAllies = true;

    private void OnEnable() {
        hitTrigger = GetComponent<Collider2D>();

        List<Collider2D> overlappingColliders = new List<Collider2D>();
        hitTrigger.Overlap(overlappingColliders);

        foreach(Collider2D other in overlappingColliders) {
            if(other.CompareTag("Enemy") && !HitEnemies) return;
            if(other.CompareTag("Player") && !HitAllies) return;

            Health healthComp = other.GetComponent<Health>();
            if(healthComp && healthComp.transform != transform.parent)
                healthComp.ChangeHealth(-Damage);

            Rigidbody2D otherRB = other.GetComponent<Rigidbody2D>();
            Vector3 toOtherVector = other.transform.position - transform.position;
            if(otherRB)
                otherRB.AddForce(toOtherVector.normalized * PhysicsImpulse, ForceMode2D.Impulse);
        }
    }
}

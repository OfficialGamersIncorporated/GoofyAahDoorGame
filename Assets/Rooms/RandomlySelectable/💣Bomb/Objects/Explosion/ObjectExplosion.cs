using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExplosion : MonoBehaviour {

    Collider2D hurtTrigger;
    public float Damage = 2;
    public float PhysicsImpulse = 20;

    void Start() {
        hurtTrigger = GetComponent<Collider2D>();

        List<Collider2D> overlappingColliders = new List<Collider2D>();
        hurtTrigger.Overlap(overlappingColliders);

        foreach(Collider2D other in overlappingColliders) {
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

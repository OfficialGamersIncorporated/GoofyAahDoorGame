using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour {

    Animator animator;
    public Collider2D PrimaryFireAttackTrigger;
    public Collider2D SecondaryFireAttackTrigger;
    public Rigidbody PrimaryFireProjectilePrefab;
    public Rigidbody SecondaryFireProjectilePrefab;

    public float PrimaryAttackTriggerDamage = 1;
    public float PrimaryAttackPhysicsImpulse = 20;
    public float SecondaryAttackTriggerDamage = 1;
    public float SecondaryAttackPhysicsImpulse = 20;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void Update() {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 toMouse = mouseWorldPos - (Vector2)transform.position;
        //transform.up = toMouse.normalized;
        transform.LookAt(mouseWorldPos, new Vector3(0, 0, 1));
        transform.rotation *= Quaternion.Euler(-90,0,180);
    }

    public virtual void PrimaryFired() {
        if (animator)
            animator.SetTrigger("PrimaryAttack");

        if(PrimaryFireAttackTrigger) {
            List<Collider2D> overlappingColliders = new List<Collider2D>();
            PrimaryFireAttackTrigger.Overlap(overlappingColliders);

            foreach(Collider2D other in overlappingColliders) {
                Health healthComp = other.GetComponent<Health>();
                if(healthComp && healthComp.transform != transform.parent)
                    healthComp.ChangeHealth(-PrimaryAttackTriggerDamage);

                Rigidbody2D otherRB = other.GetComponent<Rigidbody2D>();
                Vector3 toOtherVector = other.transform.position - transform.position;
                if(otherRB)
                    otherRB.AddForce(toOtherVector.normalized * PrimaryAttackPhysicsImpulse, ForceMode2D.Impulse);
            }
        }
    }
    public virtual void PrimaryReleased() {

    }
    public virtual void SecondaryFired() {
        if (animator)
            animator.SetTrigger("SecondaryAttack");

        if(SecondaryFireAttackTrigger) {
            List<Collider2D> overlappingColliders = new List<Collider2D>();
            SecondaryFireAttackTrigger.Overlap(overlappingColliders);

            foreach(Collider2D other in overlappingColliders) {
                Health healthComp = other.GetComponent<Health>();
                if(healthComp && healthComp.transform != transform.parent)
                    healthComp.ChangeHealth(-SecondaryAttackTriggerDamage);

                Rigidbody2D otherRB = other.GetComponent<Rigidbody2D>();
                Vector3 toOtherVector = other.transform.position - transform.position;
                if(otherRB)
                    otherRB.AddForce(toOtherVector.normalized * SecondaryAttackPhysicsImpulse, ForceMode2D.Impulse);
            }
        }
    }
    public virtual void SecondaryReleased() {

    }
}

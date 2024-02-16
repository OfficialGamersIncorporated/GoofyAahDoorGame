using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour {

    Animator animator;
    Animator parentAnimator;
    AudioSource swingSound;

    public float PrimaryFirerate = 32;
    public float PrimaryFireProjectileSpeed = 10;
    public Collider2D PrimaryFireAttackTrigger;
    public Rigidbody2D PrimaryFireProjectilePrefab;

    [Space]
    public float SecondaryFirerate = 32;
    public float SecondaryFireProjectileSpeed = 10;
    public Collider2D SecondaryFireAttackTrigger;
    public Rigidbody2D SecondaryFireProjectilePrefab;

    public Vector2 lookVector;
    float lastPrimaryFired;
    float lastSecondaryFired;

    private void Start() {
        animator = GetComponent<Animator>();
        parentAnimator = transform.parent.GetComponentInParent<Animator>();
        swingSound = GetComponent<AudioSource>();
    }
    private void Update() {
        //Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //lookVector = (mouseWorldPos - (Vector2)transform.position).normalized;
        //transform.up = toMouse.normalized;
        //transform.LookAt(mouseWorldPos, new Vector3(0, 0, 1));

        transform.LookAt(transform.position + (Vector3)lookVector, new Vector3(0, 0, 1));
        transform.rotation *= Quaternion.Euler(-90,0,180);
    }

    public virtual void PrimaryFired() {
        if(Time.time - lastPrimaryFired < 1 / PrimaryFirerate) return;
        lastPrimaryFired = Time.time;

        if (animator)
            animator.SetTrigger("PrimaryAttack");
        if (parentAnimator)
            parentAnimator.SetTrigger("PrimaryAttack");

        if(swingSound)
            swingSound.Play();

        if(PrimaryFireAttackTrigger)
            PrimaryFireAttackTrigger.gameObject.SetActive(true);

        if(PrimaryFireProjectilePrefab) {
            Room currentRoom = DungeonManager.Singleton.CurrentRoom;
            Transform projectileParent = null;
            if(currentRoom) projectileParent = currentRoom.transform;
            Vector3 projectilePosition = transform.position + (Vector3)lookVector * .5f; // spawn slightly infront of the player instead of inside the player.

            Rigidbody2D projectile = Instantiate<Rigidbody2D>(PrimaryFireProjectilePrefab, projectilePosition, new Quaternion(), projectileParent);
            projectile.velocity = lookVector * PrimaryFireProjectileSpeed;
        }
    }

    public virtual void PrimaryReleased() {
        if(PrimaryFireAttackTrigger)
            PrimaryFireAttackTrigger.gameObject.SetActive(false);
    }
    public virtual void SecondaryFired() {
        if(Time.time - lastSecondaryFired < 1 / SecondaryFirerate) return;
        lastSecondaryFired = Time.time;

        if(animator)
            animator.SetTrigger("SecondaryAttack");
        if(parentAnimator)
            parentAnimator.SetTrigger("SecondaryAttack");

        if(SecondaryFireAttackTrigger)
            SecondaryFireAttackTrigger.gameObject.SetActive(true);
        
    }
    public virtual void SecondaryReleased() {
        if(SecondaryFireAttackTrigger)
            SecondaryFireAttackTrigger.gameObject.SetActive(false);
    }
}

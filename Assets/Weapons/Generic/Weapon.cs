using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour {

    Animator animator;
    AudioSource swingSound;
    public Collider2D PrimaryFireAttackTrigger;
    public Collider2D SecondaryFireAttackTrigger;
    public Rigidbody PrimaryFireProjectilePrefab;
    public Rigidbody SecondaryFireProjectilePrefab;

    private void Start() {
        animator = GetComponent<Animator>();
        swingSound = GetComponent<AudioSource>();
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

        if(swingSound)
            swingSound.Play();

        if(PrimaryFireAttackTrigger)
            PrimaryFireAttackTrigger.gameObject.SetActive(true);
    }
    public virtual void PrimaryReleased() {
        if(PrimaryFireAttackTrigger)
            PrimaryFireAttackTrigger.gameObject.SetActive(false);
    }
    public virtual void SecondaryFired() {
        if (animator)
            animator.SetTrigger("SecondaryAttack");

        if(SecondaryFireAttackTrigger)
            SecondaryFireAttackTrigger.gameObject.SetActive(true);
        
    }
    public virtual void SecondaryReleased() {
        if(SecondaryFireAttackTrigger)
            SecondaryFireAttackTrigger.gameObject.SetActive(false);
    }
}

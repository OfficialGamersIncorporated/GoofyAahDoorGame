using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour {

    Animator animator;

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
        animator.SetTrigger("PrimaryAttack");
    }
    public virtual void PrimaryReleased() {

    }
    public virtual void SecondaryFired() {
        animator.SetTrigger("SecondaryAttack");
    }
    public virtual void SecondaryReleased() {

    }
}

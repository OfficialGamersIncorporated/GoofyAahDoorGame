using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour {

    new Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    public Weapon HeldWeapon;

    public Vector2 MoveDirection;
    public Vector2 LookDirection;
    public float MaxWalkSpeed = 5;
    public float Acceleration = 30;
    public float Deceleration = 60;

    public enum TargetFaceModeType { Flip, Rotate };
    public TargetFaceModeType TargetFaceMode = TargetFaceModeType.Flip;

    public void EquipWeapon(Weapon weaponPrefab) {
        if (HeldWeapon)
            Destroy(HeldWeapon.gameObject);

        Weapon newWeapon = Instantiate<Weapon>(weaponPrefab, transform.position, new Quaternion(), transform);
        HeldWeapon = newWeapon;
    }

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if(!spriteRenderer) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void FixedUpdate() {
        float rateOfChange = Acceleration;
        if(MoveDirection.magnitude <= 0.1 || (rigidbody.velocity.magnitude > .5 && Vector3.Angle(MoveDirection.normalized, MoveDirection.normalized) < 0) )
            rateOfChange = Deceleration;

        rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, MoveDirection * MaxWalkSpeed, rateOfChange * Time.fixedDeltaTime);

        if(TargetFaceMode == TargetFaceModeType.Flip) {
            //float flipMultiplier = -1;
            //if(LookDirection.x < 0) flipMultiplier = 1;
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * flipMultiplier, transform.localScale.y, transform.localScale.z);

            spriteRenderer.flipX = LookDirection.x > 0;
        } else {
            transform.LookAt(transform.position + (Vector3)LookDirection, Vector3.forward);
            transform.rotation *= Quaternion.Euler(-90,0,0);
        }

        if(HeldWeapon)
            HeldWeapon.lookVector = LookDirection;
    }
}

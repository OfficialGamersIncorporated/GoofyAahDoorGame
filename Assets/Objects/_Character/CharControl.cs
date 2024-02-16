using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour {

    new Rigidbody2D rigidbody;
    public Weapon HeldWeapon;

    public Vector2 MoveDirection;
    public Vector2 LookDirection;
    public float MaxWalkSpeed = 5;
    public float Acceleration = 30;
    public float Deceleration = 60;

    public enum TargetFaceModeType { Flip, Rotate };
    public TargetFaceModeType TargetFaceMode = TargetFaceModeType.Flip;


    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        float rateOfChange = Acceleration;
        if(MoveDirection.magnitude <= 0.1 || (rigidbody.velocity.magnitude > .5 && Vector3.Angle(MoveDirection.normalized, MoveDirection.normalized) < 0) )
            rateOfChange = Deceleration;

        rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, MoveDirection * MaxWalkSpeed, rateOfChange * Time.fixedDeltaTime);

        if(TargetFaceMode == TargetFaceModeType.Flip) {
            float flipMultiplier = -1;
            if(LookDirection.x < 0) flipMultiplier = 1;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * flipMultiplier, transform.localScale.y, transform.localScale.z);
        } else {
            transform.LookAt(transform.position + (Vector3)LookDirection, Vector3.forward);
            transform.rotation *= Quaternion.Euler(-90,0,0);
        }
    }
}

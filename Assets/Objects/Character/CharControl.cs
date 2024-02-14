using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour {

    new Rigidbody2D rigidbody;

    public Vector2 MoveDirection;
    public float MaxWalkSpeed = 5;
    public float Acceleration = 30;
    public float Deceleration = 60;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        float rateOfChange = Acceleration;
        if(MoveDirection.magnitude <= 0.1 || (rigidbody.velocity.magnitude > .5 && Vector3.Angle(MoveDirection.normalized, MoveDirection.normalized) < 0) )
            rateOfChange = Deceleration;

        rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, MoveDirection * MaxWalkSpeed, rateOfChange * Time.fixedDeltaTime);
    }
}

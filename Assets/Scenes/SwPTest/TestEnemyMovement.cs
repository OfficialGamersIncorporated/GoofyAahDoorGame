using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;
    CharControl charControl;

    GameObject target;

    public float acceleration = 30;
    public float decceleration = 60;
    public float maxSpeed = 5;
    public float rotateSpeed = 10;
    public float RotationOffset = -90f;

    //float targetAngle;
    //float dotProduct;
    Vector2 lookVector;
    Vector2 targetDirection;

    //Vector2 currentVelocity;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        charControl = GetComponent<CharControl>();

        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        if(!target) target = GameObject.FindGameObjectWithTag("Player");
        if(!target) return;

        targetDirection = (target.transform.position - transform.position);
        lookVector = (target.transform.position - transform.position).normalized;
        float targetAngle = (Mathf.Atan2(lookVector.y, lookVector.x) * Mathf.Rad2Deg) - RotationOffset;

        //dotProduct = Vector2.Dot(transform.up, lookVector);
        //currentVelocity = rb.velocity;

        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(targetAngle, Vector3.forward), Time.deltaTime * rotateSpeed);
    }

    private void FixedUpdate() {

        // fly towards target
        //if (rb.velocity.magnitude < maxSpeed && dotProduct > 0.9f)
        //{
        //    rb.AddForce(acceleration * transform.up, ForceMode2D.Force);
        //}
        //else
        //{
        //    rb.AddForce(-rb.velocity.normalized * decceleration, ForceMode2D.Force);
        //}

        // Move Directly to target
        if(charControl)
            charControl.MoveDirection = Vector2.ClampMagnitude(targetDirection, 1);
        else
            rb.velocity = targetDirection.normalized * maxSpeed;

        // Predict target position
        // Vector3.ProjectOnPlane

        //movementVector = Vector2.MoveTowards(movementVector, (target.transform.position - gameObject.transform.position) * moveSpeed, rateOfChange * Time.fixedDeltaTime);
        //rb.velocity = movementVector;
    }
}

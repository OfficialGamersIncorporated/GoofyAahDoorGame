using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;

    public GameObject target;

    public float acceleration;
    public float decceleration;
    public float maxSpeed;
    public float rotateSpeed;

    public float targetAngle;
    public float dotProduct;
    public Vector2 lookVector;
    public Vector2 targetDirection;

    public Vector2 currentVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        targetDirection = (target.transform.position - transform.position);
        lookVector = (target.transform.position - transform.position).normalized;
        targetAngle = (Mathf.Atan2(lookVector.y, lookVector.x) * Mathf.Rad2Deg) - 90f;

        dotProduct = Vector2.Dot(transform.up, lookVector);
        currentVelocity = rb.velocity;

        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(targetAngle, Vector3.forward), Time.deltaTime * rotateSpeed);
    }

    private void FixedUpdate()
    {

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
        rb.velocity = targetDirection.normalized * maxSpeed;

        // Predict target position
        // Vector3.ProjectOnPlane

        //movementVector = Vector2.MoveTowards(movementVector, (target.transform.position - gameObject.transform.position) * moveSpeed, rateOfChange * Time.fixedDeltaTime);
        //rb.velocity = movementVector;
    }
}

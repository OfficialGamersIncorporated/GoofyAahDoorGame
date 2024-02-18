using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BullCharge : MonoBehaviour
{
    Collider2D col;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    Vector2 targetVector;
    Vector2 chargeVector;

    bool canCharge = false;
    bool currentlyCharging = false;
    bool recovering = false;

    GameObject target;

    public float MaxSpeed = 50f;
    public float Acceleration = 500f;
    public float RecoveryTime = 2f;
    public float WaitTime = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        target = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(WaitToCharge());
    }

    private void Update()
    {
        if (!target) target = GameObject.FindGameObjectWithTag("Player");
        if (!target)
        {
            canCharge = false;
            return;
        }
        else if (!currentlyCharging && !recovering)
        {
            canCharge = true;
        }

        targetVector = (target.transform.position - transform.position);

        if (canCharge)
        {
            chargeVector = targetVector;
            // Charge noise
            canCharge = false;
            currentlyCharging = true;
            recovering = false;
        }

        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }

        else if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }

    }


    private void FixedUpdate()
    {
        if (currentlyCharging && rb.velocity.magnitude < MaxSpeed)
        {
            rb.AddForce(chargeVector.normalized * Acceleration, ForceMode2D.Force);
        }

        if (recovering)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.fixedDeltaTime / RecoveryTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentlyCharging = false;
        recovering = true;
        StartCoroutine(Recovery());
    }

    IEnumerator Recovery()
    {
        yield return new WaitForSeconds(RecoveryTime);
        rb.velocity = Vector2.zero;
        recovering = false;
    }

    IEnumerator WaitToCharge()
    {
        yield return new WaitForSeconds(WaitTime);
        canCharge = true;
    }

    // Face target
    // Make sound
    // Start charge
    // Charge until hitting target or wall

    //Collider2D col;
    //Rigidbody2D rb;

    //GameObject target;

    //Vector2 targetDirection;
    //Vector2 chargeVector;
    //Vector2 lookVector;

    //public float RotationOffset = -90;
    //public float MaxSpeed = 10f;
    //public float Acceleration = 2f;
    //public float RecoveryTime = 1f;

    //bool charging = false;
    //bool canCharge = true;
    //bool recovering = false;

    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    col = GetComponent<Collider2D>();

    //    target = GameObject.FindGameObjectWithTag("Player");
    //}

    //private void Update()
    //{
    //    if (!target) target = GameObject.FindGameObjectWithTag("Player");
    //    if (!target)
    //    {
    //        canCharge = false;
    //        return;
    //    }
    //    else if (!charging && !recovering)
    //    {
    //        canCharge = true;
    //    }

    //    targetDirection = (target.transform.position - transform.position);
    //    lookVector = (target.transform.position - transform.position).normalized;
    //    float targetAngle = (Mathf.Atan2(lookVector.y, lookVector.x) * Mathf.Rad2Deg) - RotationOffset;
    //}

    //private void FixedUpdate()
    //{
    //    if (canCharge && target != null)
    //    {
    //        StartCharge();
    //    }


    //    if (charging)
    //    {
    //        if (rb.velocity.magnitude < MaxSpeed && target != null)
    //        {
    //            rb.AddForce(Vector2.ClampMagnitude(chargeVector, Acceleration), ForceMode2D.Force);
    //        }

    //    }



    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    charging = false;
    //    StartCoroutine(Recover());
    //}

    //void StartCharge()
    //{
    //    recovering = false;
    //    charging = true;
    //    canCharge = false;
    //    chargeVector = targetDirection;
    //}

    //IEnumerator Recover()
    //{
    //    recovering = true;
    //    yield return new WaitForSeconds(RecoveryTime);
    //    canCharge = true;
    //}

}

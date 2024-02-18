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

    Animator anim;

    public float MaxSpeed = 50f;
    public float Acceleration = 500f;
    public float RecoveryTime = 2f;
    public float WaitTime = 1f;

    float freezeTimer = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

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

            canCharge = false;
            currentlyCharging = true;
            recovering = false;
            anim.SetBool("Running", true);

            //Freeze check
            if (rb.velocity.magnitude < 0.1f)
            {
                freezeTimer -= Time.deltaTime;
                if (freezeTimer < 0)
                {
                    chargeVector = targetVector;
                    freezeTimer = 2f;
                }
            }
        }

        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }

        else if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }

        anim.SetFloat("VelocityMag", rb.velocity.magnitude);

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
        anim.SetBool("Running", false);
        yield return new WaitForSeconds(RecoveryTime);
        rb.velocity = Vector2.zero;
        recovering = false;
    }

    IEnumerator WaitToCharge()
    {
        yield return new WaitForSeconds(WaitTime);
        canCharge = true;
    }

}

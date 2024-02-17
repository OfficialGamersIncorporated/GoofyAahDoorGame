using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    public float HealthCurrent = 3;
    public float HealthMax = 3;
    public bool DestroyOnDeath = true;
    [Tooltip("Will spawn one of each. Put multiple of the same prefab to spawn more than one of it.")]
    public List<Rigidbody2D> GibsPrefabs;
    public float GibsMaxEjectionForce = 10;
    public float GibsSpawnDistance = 1.5f;

    public UnityEvent Died;
    public UnityEvent HealthChanged;

    new Rigidbody2D rigidbody;

    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ChangeHealth(float healthDelta) {
        HealthCurrent += healthDelta;
        if(HealthCurrent <= 0) Die();
        HealthChanged.Invoke();
        HealthCurrent = Mathf.Clamp(HealthCurrent, 0, HealthMax);
    }
    void Die() {
        foreach(Rigidbody2D gibPrefab in GibsPrefabs) {
            Rigidbody2D gib = Instantiate<Rigidbody2D>(gibPrefab, transform.parent);
            Vector2 spawnBump = Vector3.ProjectOnPlane(Random.onUnitSphere, Vector3.forward);
            gib.transform.position = transform.position + (Vector3)spawnBump.normalized * GibsSpawnDistance;
            gib.velocity = spawnBump * GibsMaxEjectionForce;
            if(rigidbody)
                gib.velocity += rigidbody.velocity;
        }
        Died.Invoke();
        //TODO death animations?
        if (DestroyOnDeath)
            Destroy(gameObject);
    }
}

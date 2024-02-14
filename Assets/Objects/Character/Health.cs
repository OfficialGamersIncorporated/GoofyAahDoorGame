using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    public float HealthCurrent = 3;
    public float HealthMax = 3;
    public bool DestroyOnDeath = true;

    public UnityEvent Died;

    public void ChangeHealth(float healthDelta) {
        HealthCurrent += healthDelta;
        if(HealthCurrent <= 0) Die();
        HealthCurrent = Mathf.Clamp(HealthCurrent, 0, HealthMax);
    }
    void Die() {
        Died.Invoke();
        //TODO death animations?
        if (DestroyOnDeath)
            Destroy(gameObject);
    }
}

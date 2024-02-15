using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public float Healing = 1;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player") && collision.GetComponent<PlayerInput>()) {
            collision.GetComponent<Health>().ChangeHealth(Healing);
            Destroy(gameObject);
        }
    }

}

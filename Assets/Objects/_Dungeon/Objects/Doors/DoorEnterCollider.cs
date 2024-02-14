using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnterCollider : MonoBehaviour {

    Door door;

    private void Start() {
        door = GetComponentInParent<Door>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject == PlayerInput.Singleton.gameObject) {
            door.DoorEntered.Invoke();
        }
    }
}

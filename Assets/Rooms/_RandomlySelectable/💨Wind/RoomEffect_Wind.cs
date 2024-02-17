using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEffect_Wind : MonoBehaviour {

    public Vector2 WindVector;

    private void OnTriggerStay2D(Collider2D collision) {
        collision.attachedRigidbody.AddForce(WindVector);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEffect_Ice : RoomEffect {

    CharControl playerCharControl;

    public float PlayerAcceleration = 10;
    public float RigidbodyDrag = 0;
    public float RigidbodyAngularDrag = 0.05f;

    float playerDefaultAccell;
    float playerDefaultDecell;

    new private void Start() {
        base.Start();

        playerCharControl = PlayerInput.Singleton.GetComponent<CharControl>();

        playerDefaultAccell = playerCharControl.Acceleration;
        playerDefaultDecell = playerCharControl.Deceleration;

        playerCharControl.Deceleration = 0;
        playerCharControl.Acceleration = PlayerAcceleration;

        foreach(Rigidbody2D body in GameObject.FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None)) {
            body.drag = RigidbodyDrag;
            body.angularDrag = RigidbodyAngularDrag;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        collision.attachedRigidbody.drag = RigidbodyDrag;
        collision.attachedRigidbody.angularDrag = RigidbodyAngularDrag;
    }
    private void OnDestroy() {
        playerCharControl.Acceleration = playerDefaultAccell;
        playerCharControl.Deceleration = playerDefaultDecell;
    }

}

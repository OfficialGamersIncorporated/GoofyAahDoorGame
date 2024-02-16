using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic_Ice : RoomLogic_EliminateUnits {

    CharControl playerCharControl;

    public float PlayerAcceleration = 10;

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
            body.drag = 0;
            body.angularDrag = 0.05f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        collision.attachedRigidbody.drag = 0;
        collision.attachedRigidbody.angularDrag = 0.05f;
    }
    private void OnDestroy() {
        playerCharControl.Acceleration = playerDefaultAccell;
        playerCharControl.Deceleration = playerDefaultDecell;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public static PlayerInput Singleton;
    CharControl charControl;

    private void Start() {
        Singleton = this;
        charControl = GetComponent<CharControl>();
    }

    private void Update() {
        charControl.MoveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}

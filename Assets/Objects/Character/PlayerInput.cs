using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    CharControl charControl;

    private void Start() {
        charControl = GetComponent<CharControl>();
    }

    private void Update() {
        charControl.MoveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}

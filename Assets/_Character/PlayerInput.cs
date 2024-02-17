using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public static PlayerInput Singleton;
    CharControl charControl;

    private void Awake() {
        Singleton = this;
        charControl = GetComponent<CharControl>();
    }

    private void Update() {
        charControl.MoveDirection = Vector2.ClampMagnitude(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1);

        Weapon heldWeapon = charControl.HeldWeapon;
        if(heldWeapon) {

            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            charControl.LookDirection = (mouseWorldPos - (Vector2)transform.position).normalized;

            if(Input.GetButtonDown("Fire1"))
                heldWeapon.PrimaryFired();
            if(Input.GetButtonUp("Fire1"))
                heldWeapon.PrimaryReleased();
            if(Input.GetButtonDown("Fire2"))
                heldWeapon.SecondaryFired();
            if(Input.GetButtonUp("Fire2"))
                heldWeapon.SecondaryReleased();
        }
    }
}

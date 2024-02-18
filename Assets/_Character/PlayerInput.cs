using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView; // where the fuck did this come from?
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public static PlayerInput Singleton;
    CharControl charControl;

    bool useController = false;
    Vector2 lastControllerLook;

    private void Awake() {
        Singleton = this;
        charControl = GetComponent<CharControl>();
    }

    private void Update() {
        charControl.MoveDirection = Vector2.ClampMagnitude(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1);

        Weapon heldWeapon = charControl.HeldWeapon;
        if(heldWeapon) {

            Vector2 controllerLook = new Vector2(Input.GetAxisRaw("LookHorizontal"), Input.GetAxisRaw("LookVertical"));
            print(controllerLook);
            if(controllerLook.magnitude > 0.1f) {
                useController = true;
                lastControllerLook = controllerLook;
            }
            if(Input.mousePositionDelta.magnitude > 0) useController = false;

            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(useController) {
                if(controllerLook.magnitude > 0)
                    charControl.LookDirection = controllerLook.normalized;
                else
                    charControl.LookDirection = lastControllerLook.normalized;
            } else
                charControl.LookDirection = (controllerLook + (mouseWorldPos - (Vector2)transform.position)).normalized;

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

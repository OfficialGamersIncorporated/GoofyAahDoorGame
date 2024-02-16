using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// room logic scripts should be attached to the same object as the room.cs component and inherit from this script.
public class RoomLogic : MonoBehaviour {

    protected Room room;

    protected void Start() {
        room = GetComponent<Room>();
    }
    protected void OpenExitDoor() {
        room.DoorExit.Open();
    }
    private IEnumerator _OpenExitWithDelay(float delay) {
        yield return new WaitForSeconds(delay);
        OpenExitDoor();
    }
    protected void OpenExitWithDelay(float delay) {
        StartCoroutine(_OpenExitWithDelay(delay));
    }

}

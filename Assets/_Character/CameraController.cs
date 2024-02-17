using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Transform target;
    public float Distance = 10;

    void Start() {
        target = PlayerInput.Singleton.transform;
    }
    void LateUpdate() {
        if(!DungeonManager.Singleton.CurrentRoom) return; // only the camera controller should ever encounter this edge case.
        if(DungeonManager.Singleton.CurrentRoom.CameraFollowsPlayer && target )//&& target.gameObject.activeSelf)
            transform.position = target.position + Vector3.back * Distance;
        else
            transform.position = Vector3.zero + Vector3.back * Distance;
    }
}

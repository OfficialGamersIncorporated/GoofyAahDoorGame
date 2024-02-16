using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic_Blank : RoomLogic {

    public Transform ExitBox;
    public List<ParticleSystem> ParticleEmitters;
    public float distanceRequired = 35;
    public float exitOpenDistance = 5;

    private void Update() {
        Transform player = PlayerInput.Singleton.transform;
        if(!ExitBox.gameObject.activeSelf) {
            Vector3 enteranceToPlayer = player.position - room.DoorEnterance.transform.position;

            if(enteranceToPlayer.magnitude >= distanceRequired) {
                ExitBox.position = player.position;
                ExitBox.gameObject.SetActive(true);
                foreach(ParticleSystem emitter in ParticleEmitters) {
                    var emission = emitter.emission; // why does this need to be two lines?!
                    emission.enabled = false;
                }
            }
        } else {
            Vector3 exitToPlayer = player.position - room.DoorExit.transform.position;

            if (exitToPlayer.magnitude <= exitOpenDistance) {
                OpenExitDoor();
                this.enabled = false;
            }
        }
    }

}

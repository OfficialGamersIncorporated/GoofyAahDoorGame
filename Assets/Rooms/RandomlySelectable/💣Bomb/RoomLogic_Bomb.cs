using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic_Bomb : RoomLogic {

    public Transform Button;
    public ObjectBomb BombPrefab;
    public float BombLaunchForce = 5;
    public float BombFuseTime = 1;

    public void ButtonPushed() {
        ObjectBomb bomb = Instantiate<ObjectBomb>(BombPrefab, transform);
        Rigidbody2D bombRB = bomb.GetComponent<Rigidbody2D>();
        bomb.FuseTime = BombFuseTime;
        bomb.transform.position = Button.transform.position;
        bombRB.velocity = ((Vector2)Random.onUnitSphere).normalized * BombLaunchForce;
    }

    new private void Start() {
        base.Start();
        OpenExitWithDelay(5);
    }

}

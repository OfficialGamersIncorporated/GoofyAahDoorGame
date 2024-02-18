using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public GameObject Prefab;
    public float DelayBetweenSpawns = 5;

    float lastSpawnTick;

    public void Spawn() {
        GameObject newInstance = Instantiate<GameObject>(Prefab, transform.position, Quaternion.identity, transform.parent);
    }
    private void Update() {
        if(Time.time - lastSpawnTick < DelayBetweenSpawns) return;

        lastSpawnTick = Time.time;
        Spawn();
    }

}

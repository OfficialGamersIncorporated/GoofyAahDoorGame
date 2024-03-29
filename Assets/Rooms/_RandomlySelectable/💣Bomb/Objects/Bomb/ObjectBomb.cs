using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBomb : MonoBehaviour {

    public GameObject Explosion;
    public float FuseTime = 3;

    IEnumerator Start() {
        yield return new WaitForEndOfFrame(); // give the spawner enough time to set FuseTime
        yield return new WaitForSeconds(FuseTime);
        Explosion.SetActive(true);
        Explosion.transform.SetParent(null);
        Destroy(gameObject);
    }
}

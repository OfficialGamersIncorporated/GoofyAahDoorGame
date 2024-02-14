using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExplosion : MonoBehaviour {

    void Start() {
        
    }

    public void AnimationComplete() {
        Destroy(gameObject);
    }
}

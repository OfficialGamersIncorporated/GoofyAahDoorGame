using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyAI : MonoBehaviour {

    GameObject target;
    CharControl charControl;

    [Range(-1, 1)]
    public float TowardsTargetLinear = 1;
    public float TowardsTargetSinFrequency = 0;
    [Range(0, 1)]
    public float TowardsTargetSinAmplitude = 0;

    [Range(-1, 1)]
    public float OrbitTargetLinear = 0;
    public float OrbitTargetSinFrequency = 0;
    [Range(0, 1)]
    public float OrbitTargetSinAmplitude = 0;

    void Start() {
        charControl = GetComponent<CharControl>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update() {
        if(!target) target = GameObject.FindGameObjectWithTag("Player");
        if(!target) return;

        Vector2 towardsTargetVector = target.transform.position - transform.position;
        Vector2 orbitTargetVector = Vector3.Cross(towardsTargetVector, Vector3.forward);

        Vector2 towardsTargetContribution = towardsTargetVector * (TowardsTargetLinear + Mathf.Sin(Time.time * TowardsTargetSinFrequency) * TowardsTargetSinAmplitude);
        Vector2 orbitTargetContribution = orbitTargetVector * (OrbitTargetLinear + Mathf.Cos(Time.time * OrbitTargetSinFrequency) * OrbitTargetSinAmplitude);

        charControl.MoveDirection = Vector2.ClampMagnitude(towardsTargetContribution + orbitTargetContribution, 1);
    }
}

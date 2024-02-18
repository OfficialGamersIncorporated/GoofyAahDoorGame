using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyAI : MonoBehaviour {

    GameObject target;
    CharControl charControl;
    new Rigidbody2D rigidbody;

    public enum LookTargetType { Target, MoveDirection };
    public LookTargetType LookTarget = LookTargetType.Target;
    public enum TargetingType { Ally, Enemy }
    public TargetingType TargetingMode = TargetingType.Ally;

    [Space]
    [Range(-1, 1)]
    public float TowardsTargetLinear = 1;
    public float TowardsTargetSinFrequency = 0;
    [Range(0, 1)]
    public float TowardsTargetSinAmplitude = 0;

    [Space]
    [Range(-1, 1)]
    public float OrbitTargetLinear = 0;
    public float OrbitTargetSinFrequency = 0;
    [Range(0, 1)]
    public float OrbitTargetSinAmplitude = 0;

    float tickBorn;

    GameObject GetTarget() {
        GameObject[] possibleTargets;
        if(TargetingMode == TargetingType.Ally)
            possibleTargets = GameObject.FindGameObjectsWithTag("Player");
        else
            possibleTargets = GameObject.FindGameObjectsWithTag("Enemy");

        float closestDistance = Mathf.Infinity;
        GameObject closestTarget = null;
        foreach(GameObject target in possibleTargets) {
            float distance = (target.transform.position - transform.position).magnitude;
            if(distance > closestDistance) continue;
            closestDistance = distance;
            closestTarget = target;
        }
        return closestTarget;
    }
    void Start() {
        charControl = GetComponent<CharControl>();
        rigidbody = GetComponent<Rigidbody2D>();
        target = GetTarget(); //GameObject.FindGameObjectWithTag("Player");
        tickBorn = Time.time;
    }
    void Update() {
        float lifetime = Time.time - tickBorn;

        if(LookTarget == LookTargetType.MoveDirection) {
            if(rigidbody)
                if(rigidbody.velocity.magnitude > .1f)
                    charControl.LookDirection = rigidbody.velocity.normalized;
                else
                if(charControl.MoveDirection.magnitude > .1f)
                    charControl.LookDirection = charControl.MoveDirection.normalized;
        }

        if(!target) target = GetTarget(); //GameObject.FindGameObjectWithTag("Player");
        if(!target) {
            charControl.MoveDirection = Vector2.zero;
            return;
        }

        Vector2 towardsTargetVector = target.transform.position - transform.position;
        Vector2 orbitTargetVector = Vector3.Cross(towardsTargetVector, Vector3.forward);

        Vector2 towardsTargetContribution = towardsTargetVector * (TowardsTargetLinear + Mathf.Sin(lifetime * TowardsTargetSinFrequency) * TowardsTargetSinAmplitude);
        Vector2 orbitTargetContribution = orbitTargetVector * (OrbitTargetLinear + Mathf.Cos(lifetime * OrbitTargetSinFrequency) * OrbitTargetSinAmplitude);

        charControl.MoveDirection = Vector2.ClampMagnitude(towardsTargetContribution + orbitTargetContribution, 1);

        if(LookTarget == LookTargetType.Target)
            charControl.LookDirection = towardsTargetVector.normalized;

        if(charControl.HeldWeapon) {
            charControl.HeldWeapon.PrimaryFired();
            charControl.HeldWeapon.PrimaryReleased();
            charControl.HeldWeapon.SecondaryFired();
            charControl.HeldWeapon.SecondaryReleased();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic_EliminateAllUnits : RoomLogic_EliminateUnits {
    new void Update() {
        Health[] targets = FindObjectsByType<Health>(FindObjectsSortMode.None);
        for(int i = 0; i < targets.Length; i++) {
            Health target = targets[i];
            if(target.CompareTag("Player"))
                targets[i] = null;
        }
        Units = new List<Health>(targets);

        // this should be run last because it checks if everything is
        // dead in the list and we want to make sure everything is in the list first.
        base.Update();
    }
}

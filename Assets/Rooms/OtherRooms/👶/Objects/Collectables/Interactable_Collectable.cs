using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Collectable : Interactable {

    public enum BossCollectableType { BabyBottle, KeyRing, StuffedBear };
    public BossCollectableType Collectable;

    public override void Interact(CharControl character) {
        base.Interact(character);

        DungeonManager dungeonManager = DungeonManager.Singleton;

        dungeonManager.GiveBossCollectable(Collectable);

        Destroy(gameObject);
    }

}

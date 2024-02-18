using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIBossCollectablesDisplay : MonoBehaviour {

    DungeonManager dungeonManager;
    public Image BabyBottle;
    public Image KeyRing;
    public Image StuffedBear;

    private void Start() {
        dungeonManager = DungeonManager.Singleton;
    }

    public void RefreshDisplay() {
        BabyBottle.gameObject.SetActive(dungeonManager.CollectableBottle);
        KeyRing.gameObject.SetActive(dungeonManager.CollectableKeyRing);
        StuffedBear.gameObject.SetActive(dungeonManager.CollectableStuffedBear);
    }
}

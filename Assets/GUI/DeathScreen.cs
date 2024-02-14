using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour {

    public GameObject Screen;

    public void RestartDungeon() {
        DungeonManager.Singleton.RestartDungeon();
    }
    public void PlayerDied() {
        Screen.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreen : MonoBehaviour {

    public GameObject Screen;
    public TextMeshProUGUI DeathMessageObject;

    public void RestartDungeon() {
        DungeonManager.Singleton.RestartDungeon();
    }
    public void PlayerDied() {
        List<string> deathMessages = DungeonManager.Singleton.CurrentRoom.DeathScreenMessages;
        if(deathMessages.Count > 0)
            DeathMessageObject.text = deathMessages[Random.Range(0, deathMessages.Count)];
        else
            DeathMessageObject.text = "";
        Screen.SetActive(true);
    }
}

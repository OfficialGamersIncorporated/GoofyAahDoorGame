using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room_DoorChooser : Room {

    //public List<EmojiDoor> Doors = new List<EmojiDoor>();
    public List<Door> Doors = new List<Door>();
    public List<TextMeshPro> DoorTexts = new List<TextMeshPro>();

    IEnumerator Start() {
        foreach(Door door in Doors) {
            door.Close();
        }
        yield return new WaitForSeconds(2);
        foreach(Door door in Doors) {
            door.Open();
        }
    }
    void Update() {

    }
}

// stupid shitty unity bug stops me from using this even though it would be cleaner.
//[System.Serializable]
//public class EmojiDoor {
//    public Door Door;
//    public TextMeshPro TextObject;
//}
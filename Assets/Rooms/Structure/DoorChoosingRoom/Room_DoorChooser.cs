using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room_DoorChooser : Room {

    //public List<EmojiDoor> Doors = new List<EmojiDoor>();
    public List<Door> Doors = new List<Door>();
    public List<TextMeshPro> DoorTexts = new List<TextMeshPro>();
    public List<Room> DoorRooms = new List<Room>();
    

    IEnumerator Start() {
        DoorRooms = DungeonManager.Singleton.GetNewRooms(Doors.Count);
        for(int i = 0; i < DoorTexts.Count; i++) {
            DoorTexts[i].text = DoorRooms[i].name;
        }
        foreach(Door door in Doors) {
            door.Close();
        }
        yield return new WaitForSeconds(2);
        foreach(Door door in Doors) {
            door.Open();

            void DoorEntered() {
                // test code
                DungeonManager.Singleton.GoToRoom(DungeonManager.Singleton.SelectableRooms.Rooms[0]);
            }
            door.DoorEntered.AddListener(DoorEntered);
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
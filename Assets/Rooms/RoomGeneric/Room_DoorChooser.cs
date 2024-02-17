using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room_DoorChooser : Room {

    //public List<EmojiDoor> Doors = new List<EmojiDoor>();
    public List<Door> Doors = new List<Door>();
    //public List<TextMeshPro> DoorTexts = new List<TextMeshPro>();
    //public List<Room> DoorRooms = new List<Room>();
    

    IEnumerator Start() {
        List<Room> DoorRooms = DungeonManager.Singleton.GetNewRooms(Doors.Count);
        for(int i = 0; i < Doors.Count; i++) {
            Doors[i].SetDestinationRoom(DoorRooms[i]);
        }
        //for(int i = 0; i < DoorTexts.Count; i++) {
        //    DoorTexts[i].text = DoorRooms[i].name;
        //}
        //foreach(Door door in Doors) {
        //    door.Close();
        //}
        yield return new WaitForSeconds(1);
        for(int i = 0; i < Doors.Count; i++) {
            int doorIndex = i;
            Door door = Doors[i];
            door.Open();

            //void DoorEntered() {
            //    // test code
            //    DungeonManager.Singleton.GoToRoom(DoorRooms[doorIndex]);
            //}
            //door.DoorEntered.AddListener(DoorEntered);
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
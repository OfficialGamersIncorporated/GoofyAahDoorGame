using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CreateAssetMenu(fileName = "SelectableRooms", menuName = "ScriptableObjects/SelectableRoomsAsset", order = 1)]
public class SelectableRoomsAsset : ScriptableObject {

    public List<Room> Rooms = new List<Room>();

}

[CustomEditor(typeof(SelectableRoomsAsset))]
public class SelectableRoomsAssetEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        SelectableRoomsAsset selectedObject = (SelectableRoomsAsset)target;

        if(GUILayout.Button("Grab from Assets/Rooms/RandomlySelectable")) {
            selectedObject.Rooms = new List<Room>();

            string path = "Assets/Rooms/RandomlySelectable"; // Application.dataPath
            //Debug.Log(path);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            foreach(DirectoryInfo subDirectory in directoryInfo.GetDirectories()) {
                Debug.Log(subDirectory.FullName);
                foreach(FileInfo file in subDirectory.GetFiles("*.prefab")) {
                    Room foundRoom = AssetDatabase.LoadAssetAtPath<Room>(path + "/" + subDirectory.Name + "/" + file.Name);
                    //Debug.Log(path + "/" + subDirectory.Name);
                    //Debug.Log(foundRoom);
                    if(foundRoom) selectedObject.Rooms.Add(foundRoom);
                }

                //Debug.Log(subDirectory.Name);
                //Room foundRoom = AssetDatabase.LoadAssetAtPath<Room>(path + "/" +subDirectory.Name);
                //Debug.Log(path + "/" + subDirectory.Name);
                //Debug.Log(foundRoom);
                //if(foundRoom) selectedObject.Rooms.Add(foundRoom);
            }
            Undo.RecordObject(selectedObject, "auto grab rooms");
            EditorUtility.SetDirty(selectedObject);
        }
    }
}
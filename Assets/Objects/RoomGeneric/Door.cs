using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Door : MonoBehaviour {

    public GameObject ClosedState;
    public GameObject OpenState;

    public void Open() {
        ClosedState.SetActive(false);
        OpenState.SetActive(true);
    }
    public void Close() {
        OpenState.SetActive(false);
        ClosedState.SetActive(true);
    }


}

[CustomEditor(typeof(Door))]
public class DoorEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        Door selectedObject = (Door)target;

        if(GUILayout.Button("Open Door")) {
            selectedObject.Open();
        }
        if(GUILayout.Button("Close Door")) {
            selectedObject.Close();
        }
    }
}
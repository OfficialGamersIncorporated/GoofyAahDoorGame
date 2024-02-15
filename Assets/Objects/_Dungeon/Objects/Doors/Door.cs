using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class Door : MonoBehaviour {

    AudioSource OpenSound;
    public GameObject ClosedState;
    public GameObject OpenState;
    public GameObject SpawnPoint;
    public UnityEvent DoorEntered;

    private void Start() {
        OpenSound = GetComponent<AudioSource>();
    }
    public void Open() {
        if (OpenSound)
            OpenSound.Play();
        ClosedState.SetActive(false);
        OpenState.SetActive(true);
    }
    public void Close() {
        if(OpenSound)
            OpenSound.Play();
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
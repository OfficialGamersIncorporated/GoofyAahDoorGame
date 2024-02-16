using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using TMPro;

public class Door : MonoBehaviour {

    [Tooltip("Optional. Don't set from code. Use Door.SetDestinationRoom()")]
    public Room DestinationRoom;
    public UnityEvent DoorEntered;

    [Space]
    public TextMeshPro EmojiTextObject;
    public GameObject ClosedState;
    public GameObject OpenState;
    public GameObject SpawnPoint;
    public Vector2 FacingNormal;

    AudioSource OpenSound;

    private void Start() {
        OpenSound = GetComponent<AudioSource>();
        SetDestinationRoom(DestinationRoom);
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
    public void SetDestinationRoom(Room destination) {
        DestinationRoom = destination;
        if (destination)
            EmojiTextObject.text = destination.name;
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
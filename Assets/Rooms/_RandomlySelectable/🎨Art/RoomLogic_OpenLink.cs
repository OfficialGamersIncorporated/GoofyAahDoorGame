using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic_OpenLink : RoomLogic {

    public string URL;


    new void Start() {
        base.Start();
        OpenExitWithDelay(2);
    }

    public void OpenLink() {
        Application.OpenURL(URL);
    }

}

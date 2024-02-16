using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public virtual void Interact(CharControl character) {

    }
    public virtual void PlayerInteract() {
        Interact(PlayerInput.Singleton.GetComponent<CharControl>());
    }

}

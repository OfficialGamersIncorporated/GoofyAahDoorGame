using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScreenInitiator : MonoBehaviour {

    public void GoToCredits() {
        SceneManager.LoadScene("Credits");
    }
    public void GoToMainMenu() {
        SceneManager.LoadScene("TitleScreen");
    }

}

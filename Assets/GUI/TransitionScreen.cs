using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour {

    public float TransitionTime = 1;

    public static TransitionScreen Singleton;
    Image overlay;

    private void Awake() {
        Singleton = this;
        overlay = GetComponent<Image>();
    }

    public IEnumerator Show() {
        float startTime = Time.time;
        while(Time.time - startTime < TransitionTime) {
            float alpha = (Time.time - startTime) / TransitionTime;

            overlay.color = new Color(1, 1, 1, alpha);
            yield return new WaitForEndOfFrame();
        }
        overlay.color = new Color(1, 1, 1, 1);
        //yield return null;
    }
    public IEnumerator Hide() {
        float startTime = Time.time;
        while(Time.time - startTime < TransitionTime) {
            float alpha = (Time.time - startTime) / TransitionTime;

            overlay.color = new Color(1, 1, 1, 1-alpha);
            yield return new WaitForEndOfFrame();
        }
        overlay.color = new Color(1, 1, 1, 0);
        //yield return null;
    }
}

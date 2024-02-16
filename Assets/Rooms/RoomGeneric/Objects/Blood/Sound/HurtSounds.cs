using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtSounds : MonoBehaviour {

    Health healthObject;
    public List<AudioSource> Sounds;

    private void OnEnable() {
        healthObject = GetComponentInParent<Health>();
        healthObject.Died.AddListener(Died);
        healthObject.HealthChanged.AddListener(Damaged);
    }
    private void OnDisable() {
        healthObject.Died.RemoveListener(Died);
        healthObject.HealthChanged.RemoveListener(Damaged);
    }

    void Damaged() {
        AudioSource sound = Sounds[Random.Range(0, Sounds.Count)];
        sound.Play();
    }
    IEnumerator _Died() {
        transform.parent = null;
        AudioSource sound = Sounds[Random.Range(0, Sounds.Count)];
        sound.Play();
        yield return new WaitForSecondsRealtime(sound.clip.length);
        Destroy(gameObject);
    }
    void Died() {
        StartCoroutine(_Died());
    }
}

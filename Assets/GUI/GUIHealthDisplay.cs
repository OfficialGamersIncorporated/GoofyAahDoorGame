using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIHealthDisplay : MonoBehaviour {

    Health player;
    public List<Image> Hearts;

    void Start() {
        player = PlayerInput.Singleton.GetComponent<Health>();

        if(player.HealthMax > Hearts.Count)
            Debug.LogError("Player's max health is greater than the number of heart elements in their hud. Please add more.");

        player.HealthChanged.AddListener(RefreshHealth);
    }
    //private void OnEnable() {
        
    //}
    //private void OnDisable() {
    //    player.HealthChanged.RemoveListener(RefreshHealth);
    //}

    void RefreshHealth() {
        int health = Mathf.CeilToInt(player.HealthCurrent);
        for(int i = 0; i < Hearts.Count; i++) {
            Hearts[i].gameObject.SetActive(i < health);
        }
    }
}

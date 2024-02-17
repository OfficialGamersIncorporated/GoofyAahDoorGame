using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    public List<GameObject> LootTable;
    public Hitbox OpenPusher;

    public void Open() {
        OpenPusher.gameObject.SetActive(true);
        GameObject loot = Instantiate<GameObject>(LootTable[Random.Range(0, LootTable.Count)], transform.position, Quaternion.identity, transform.parent);

        Destroy(gameObject);
    }
}

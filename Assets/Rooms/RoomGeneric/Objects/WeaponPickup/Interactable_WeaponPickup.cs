using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_WeaponPickup : Interactable {

    [Tooltip("Don't set this property directly from code. Use SetWeaponPrefab() instead.")]
    public Weapon WeaponPrefab;
    public SpriteRenderer weaponPreview;

    public void SetWeaponPrefab(Weapon newPrefab) {
        WeaponPrefab = newPrefab;

        if(WeaponPrefab)
            weaponPreview.sprite = WeaponPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
        else
            weaponPreview.sprite = null;
    }

    private void Start() {
        SetWeaponPrefab(WeaponPrefab);
    }

    public override void Interact(CharControl character) {
        base.Interact(character);

        character.EquipWeapon(WeaponPrefab);

        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipRegion : MonoBehaviour
{
    public Equipable defaultEquip;
    public string region_id;
    GameObject currentEquip;

    public void spawnEquipable(Equipable equipable) {
        if(equipable == null) {
            return;
        }
        if(currentEquip != null) {
            Destroy(currentEquip);
            currentEquip = null;
        }
        if(equipable.region_id != region_id) {
            return;
        }

        GameObject.Instantiate(
            equipable.gameObject,
            transform.position + equipable.offset,
            Quaternion.Euler(transform.rotation.eulerAngles + equipable.rotate_offset),
            transform
        );
    }
}

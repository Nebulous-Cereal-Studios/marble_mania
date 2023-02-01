using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] List<EquipRegion> regions = new List<EquipRegion>();

    [SerializeField] List<Equipable> Cosmetics;

    Dictionary<string, Equipable> equiped_cosmetics = new Dictionary<string, Equipable>();

    void repopulateDict() {
        equiped_cosmetics.Clear();
        foreach(var cosmetic in Cosmetics) {
            if(equiped_cosmetics.ContainsKey(cosmetic.region_id)) {
                continue;
            }
            equiped_cosmetics.Add(cosmetic.region_id, cosmetic);
        }
    }

    private void Start() {
        repopulateDict();

        foreach(var region in regions) {
            Equipable val = null;
            if(!equiped_cosmetics.TryGetValue(region.region_id, out val)) {
                region.spawnEquipable(region.defaultEquip);
                continue;
            }

            region.spawnEquipable(val);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//i should probably give this more contorl over multi-enemies, but its probably not that significant of a difference
public class MultiEnemyEncounter : Encounter
{

    protected List<HostileEntity> encounterEnemies;
    public string encounterName;

    public virtual void setEnemies(List<HostileEntity> encounterEnemies) {
        this.encounterEnemies = encounterEnemies;
    }

    public override string getEncounterName() {
        return this.encounterName;
    }

    public override string getEncounterStats() {
        string stats = "";
        foreach(HostileEntity e in this.encounterEnemies) {
            stats += "Health: " + e.health + "/" + e.maxHealth + "\nDamage: " + e.damage + "\nDefense: " + e.defense + "\n";
        }
        return stats;
    }
}

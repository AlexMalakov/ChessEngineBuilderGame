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

    public override List<string[]> getEncounterStats() {
        List<string[]> stats = new List<string[]>();
        foreach(HostileEntity e in this.encounterEnemies) {
            stats.Add(new string[]{e.name, e.health + "/" + e.maxHealth, e.damage + "", e.defense + ""});
            // stats += "Health: " + e.health + "/" + e.maxHealth + "\nDamage: " + e.damage + "\nDefense: " + e.defense + "\n";
        }
        return stats;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//maybe could be buffed cuz this sucks against a lot of enemies
public class BattlePreperationsKingStrategy : KingStance
{
    public override void onEncounterStart() {
        new PrimedStatusEffect().onAttatch(this.game.getEnemy());
        foreach(Minion m in this.game.getEnemy().minions) {
            new PrimedStatusEffect().onAttatch(m);
        }
    }
    public override void onStrategyEnabled() {}
    public override void onStrategyDisabled() {}

    public override string getRewardName() {
        return "Strategy: Battle Preperation";
    }
    public override string getRewardDescription() {
        return "At the start of combat, apply primed to all enemies and minions.";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "strategy";
    }
}

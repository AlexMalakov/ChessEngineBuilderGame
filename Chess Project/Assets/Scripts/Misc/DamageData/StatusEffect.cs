using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    public Entity target;
    public string statusName;

    public StatusEffect(string statusName) {
        this.statusName = statusName;
    }

    public virtual void onAttatch(Entity target) {
        this.target = target;
        target.addStatusEffect(this);
    }

    public virtual void onRemove() {
        target.removeStatusEffect(this);
    }

    public virtual int affectOutgoingDamage(int damage) {
        return damage;
    }

    public virtual int affectOutgoingDefense(int defense) {
        return defense;
    }

    public virtual int affectIncomingDamage(int incomingDamage) {
        return incomingDamage;
    }

    //returns true if the movement will be succesful, and false if it fails ig?
    public virtual bool affectMoveAttempt(Square destination) {
        return true;
    }


    public virtual void onRoundEnd() {

    }
}

//  - ### Positive
// 	 - guard n: blocks against the next n damage, then lose effect
// 	 - overguard: the next time this entity is attatcked, the attack deals 0 damage, then lose effect
// 	 - energized n: the next attack deals n extra damage, then lose effect
// 	 - blessed: the next attack is guaranteed to crit

//  - ### Negative
// 	 - poison n: deals n unblockable damage per turn, then drop the number of stacks by (1 or 50%) OR not stackable, deal 1 unblockable damage every turn
// 	 - burn n : deals n damage at the start of the next damage phase. Adding m burn before the next damage phase causes the effect to deal m + n damage, then n burn stacks are removed
// 	 - primed: the next time primed is applied, deal 3 damage
// 	 - Hinder: 30% (+- luck) change the next movement fails
// 	 - frozen: cannot perform actions for a turn
// 	 - weaken: all damage dealt during the next round is reduced by 1
// 	 - ensnared: take 1 damage every time an action or move is preformed
//   - shatter: reduces the effectiveness of block
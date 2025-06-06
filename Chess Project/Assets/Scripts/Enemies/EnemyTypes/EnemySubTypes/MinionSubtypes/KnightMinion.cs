using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMinion : MultiMinion
{
    public override void initActions() {
        this.actionQueue = new List<HostileEntityAction>();
        this.actionLoop = new List<HostileEntityAction>();

        KnightAttackAction attack = new KnightAttackAction(this);
        KnightCondHopAction condAttack = new KnightCondHopAction(this, false);
        KnightCondHopAction condGuard = new KnightCondHopAction(this, true);
        KnightGuardAction guard = new KnightGuardAction(this);
        KnightHopAction hop = new KnightHopAction(this);

        RandomAction all = new RandomAction(this, new List<HostileEntityAction>());
        ComboAction hophop = new ComboAction(this, new List<HostileEntityAction>(){hop, hop});
        ComboAction hopGuard = new ComboAction(this, new List<HostileEntityAction>(){condGuard, guard});
        ComboAction hopAttack = new ComboAction(this, new List<HostileEntityAction>(){condAttack, attack});
        ComboAction attackAttackHop = new ComboAction(this, new List<HostileEntityAction>(){attack, attack, hop});
        ComboAction attackHopAttack = new ComboAction(this, new List<HostileEntityAction>(){attack, hopAttack});
        ComboAction guardHopAttack = new ComboAction(this, new List<HostileEntityAction>(){guard, hopAttack});
        ComboAction attackHopGuard = new ComboAction(this, new List<HostileEntityAction>(){attack, hopGuard});
        ComboAction allAll = new ComboAction(this, new List<HostileEntityAction>(){all, all});
        all.actions = new List<HostileEntityAction>(){hophop, hopGuard, hopAttack, attackAttackHop, attackHopAttack, guardHopAttack, attackHopGuard, allAll};
        this.actionLoop.Add(all);
    }
}

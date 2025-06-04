using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoostType {
    heal, damage, defense, status
}

public abstract class BoostAction : HostileEntityAction
{
    public abstract List<HostileEntity> boostTargets();
    public string[] args;
    public BoostType type;

    public BoostAction(HostileEntity opponent, BoostType type, string[] args) : base(opponent) {
        this.args = args;
        this.type = type;
    }


    public override IEnumerator act() {
        foreach(HostileEntity boostTarget in this.boostTargets()) {
            switch(this.type) {
                case BoostType.heal:
                    boostTarget.health += int.Parse(args[0]);
                    break;
                case BoostType.damage:
                    boostTarget.damage += int.Parse(args[0]);
                    break;
                case BoostType.defense:
                    boostTarget.defense += int.Parse(args[0]);
                    break;
                case BoostType.status:
                    //TODO status factory
                    if(args.Length > 1) {
                        //StatusEffect s = statusFactory.createStatus(args[0]);
                    } else {
                        //StatusEffect s = statusFactory.createStackableStatus(args[0], int.Parse(args[1]));
                    }
                    break;

                    //s.attatch(boostTarget);

            }
        }
        yield return null;
    }
}

public class BoostSelf : BoostAction {
    public BoostSelf(HostileEntity opponent, BoostType type, string[] args) : base(opponent, type, args) {}

    public override List<HostileEntity> boostTargets() {
        List<HostileEntity> targets = new List<HostileEntity>();
        targets.Add(this.opponent);
        return targets;
    }
}

public class BoostOthers : BoostAction {
    public BoostOthers(HostileEntity opponent, BoostType type, string[] args) : base(opponent, type, args) {}

    public override List<HostileEntity> boostTargets() {
        List<HostileEntity> targets = new List<HostileEntity>();

        if(this.opponent is Enemy) {
            foreach(Minion m in ((Enemy)this.opponent).minions) {
                targets.Add(m);
            }
        } else {
            targets.Add(((Minion)this.opponent).enemy);
            foreach(Minion m in ((Minion)this.opponent).enemy.minions) {
                if(m == this.opponent) {
                    continue;
                }
                targets.Add(m);
            }
        }
        return targets;
    }
}

public class BoostAll : BoostAction {
    public BoostAll(HostileEntity opponent, BoostType type, string[] args) : base(opponent, type, args) {}

    public override List<HostileEntity> boostTargets() {
        List<HostileEntity> targets = new List<HostileEntity>();

        if(this.opponent is Enemy) {
            targets.Add(this.opponent);
            foreach(Minion m in ((Enemy)this.opponent).minions) {
                targets.Add(m);
            }
        } else {
            targets.Add(((Minion)this.opponent).enemy);
            foreach(Minion m in ((Minion)this.opponent).enemy.minions) {
                targets.Add(m);
            }
        }
        return targets;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : HostileEntity
{
    protected int minionsFinished; protected bool enemyFinished;

    [Header ("Minions")]
    public List<GameObject> minionsToInstantiate;
    public List<int> minionCounts;
    public List<int> minionStartingXs;
    public List<int> minionStartingYs;
    public List<GameObject> minionWaveToInstantiate;
    public List<Minion> minions;
    [Header ("Costmetic")]
    public Sprite enemySprite;

    public virtual void onEncounterStart(){
        this.initActions();
    }

    public void Awake() {
        int counter = 0;
        for(int mIndex = 0; mIndex < minionsToInstantiate.Count; mIndex++) {
            for(int mAmount = 0; mAmount < this.minionCounts[mIndex]; mAmount++) {
                Minion minion = Instantiate(minionsToInstantiate[mIndex]).GetComponent<Minion>();
                minions.Add(minion);
                minion.startingX = minionStartingXs[counter];
                minion.startingY = minionStartingYs[counter];
                minion.gameObject.SetActive(false);
                counter++;
            }
        }
    }

    public override IEnumerator takeTurn() {
        enemyFinished = false;
        minionsFinished = 0;

        yield return base.takeTurn();

        foreach(Minion m in this.minions) {
            yield return m.takeTurn();
        }
    }

    //revives all the dead minions
    public virtual void summonMinions() {
        foreach(Minion m in this.minions) {
            if(!m.alive) {
                m.gameObject.SetActive(true);
                this.game.getBoard().placeEntity(m);
                m.onSummon(this);
            }
        }

        this.summonMinionWave();
    }

    public virtual void summonMinionWave() {
        int counter = 0;
        for(int i = 0; i < minionsToInstantiate.Count; i++) {
            counter += minionCounts[i];
        }
        for(int mIndex = 0; mIndex < minionWaveToInstantiate.Count; mIndex++) {
            for(int mCount = minionsToInstantiate.Count; mCount < minionCounts.Count; mCount++) {
                if(this.game.getBoard().getSquareAt(minionStartingXs[counter], minionStartingYs[counter])) {
                    counter++;
                    continue;
                }

                Minion m = Instantiate(minionWaveToInstantiate[mIndex]).GetComponent<Minion>();
                m.waveMinion = true;
                minions.Add(m);
                m.startingX = minionStartingXs[counter];
                m.startingY = minionStartingYs[counter];
                m.gameObject.SetActive(true);
                this.game.getBoard().placeEntity(m);
                m.onSummon(this);
                counter++;
            }
        }
    }

    public override void onTurnOver() {
        enemyFinished = true;
        if(minionsFinished == minions.Count) {
            this.game.startPlayerTurn();
        }
    }
    
    public virtual void onMinionFinished() {
        minionsFinished++;
        if(enemyFinished && minionsFinished == minions.Count) {
            this.game.startPlayerTurn();
        }
    }

    public override void returnDamage() {
        game.getBoard().returnDamage(this.position,this.defense);
        foreach(Minion m in this.minions) {
            m.returnDamage();
        }
    }

    public override void onDeath() {
        foreach(Minion m in this.minions) {
            m.onEnemyDeath();
        }
        this.game.getEncounter().onEnemyDefeat();
        Destroy(this.gameObject);
    }

    //revives all dead minions :)
    public void reviveMinions() {
        foreach(Minion m in this.minions) {
            if(!m.alive) {
                m.onSummon(this);
            }
        }
    }

    public override EntityType getEntityType() {
        return EntityType.Enemy;
    }
}

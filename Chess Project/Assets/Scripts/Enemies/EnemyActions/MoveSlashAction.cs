using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//bishops action where it dashes and then stabs
public class MoveSlashAction : EnemyAction
{
    private Square bestBounced; //this is bad but i dont want to learn how tuples work so womp womp lol

    public int MoveSlashDamage;
    public override IEnumerator act() {
        bestBounced = null;
        Square destination = findLethalSquare();

        if(bestBounced != null) {
            yield return this.enemy.slide(bestBounced);
        }
        yield return this.enemy.slide(destination);

        if(this.enemy.game.getBoard().getSquareAt(destination.x+1, destination.y+1)!= null && this.enemy.game.getBoard().getSquareAt(destination.x+1, destination.y+1).hasChessPiece()) {
            this.enemy.game.getBoard().getSquareAt(destination.x+1, destination.y+1).entity.takeDamage(MoveSlashDamage);
        }  
        if(this.enemy.game.getBoard().getSquareAt(destination.x+1, destination.y-1)!= null && this.enemy.game.getBoard().getSquareAt(destination.x+1, destination.y-1).hasChessPiece()) {
            this.enemy.game.getBoard().getSquareAt(destination.x+1, destination.y-1).entity.takeDamage(MoveSlashDamage);
        } 
        if(this.enemy.game.getBoard().getSquareAt(destination.x-1, destination.y+1)!= null && this.enemy.game.getBoard().getSquareAt(destination.x-1, destination.y+1).hasChessPiece()) {
            this.enemy.game.getBoard().getSquareAt(destination.x-1, destination.y+1).entity.takeDamage(MoveSlashDamage);
        } 
        if(this.enemy.game.getBoard().getSquareAt(destination.x-1, destination.y-1)!= null && this.enemy.game.getBoard().getSquareAt(destination.x-1, destination.y-1).hasChessPiece()) {
            this.enemy.game.getBoard().getSquareAt(destination.x-1, destination.y-1).entity.takeDamage(MoveSlashDamage);
        } 
    }

    private Square findLethalSquare() {
        Square bestDestination = enemy.position;
        int bestStabs = stabCount(enemy.position);
        int bestKills = killCount(enemy.position);

        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{-1,-1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,1});

        foreach(int[] offset in offsets) {
            Square bounce = null;
            int x = enemy.position.x; int y = enemy.position.y;
            while(true) {
                Square s = this.enemy.game.getBoard().getSquareAt(x + offset[0], y + offset[1]);
                if(s == null && bounce == null) {
                    bounce = this.enemy.game.getBoard().getSquareAt(x,y);
                    if(x + offset[0] >= this.enemy.game.getBoard().len || x + offset[0] < 0) {
                        offset[0] = -offset[0];
                    }
                    if(y + offset[1] >= this.enemy.game.getBoard().height || y + offset[1] < 0) {
                        offset[1] = -offset[1];
                    }
                    continue;
                } else if(s == null) {
                    break;
                }

                if(s.entity != null) {
                    break;
                }

                if(bestKills < killCount(s) || (bestKills == killCount(s) && bestStabs < stabCount(s)) || bestKills == killCount(s) && bestStabs == stabCount(s) && Random.Range(0, 2) == 0) {
                    bestDestination = s;
                    bestStabs = stabCount(s);
                    bestKills = killCount(s);
                    bestBounced = bounce;
                }

                x += offset[0]; y += offset[1];
            }
        }

        return bestDestination;
    }

    private int stabCount(Square position) {
        int count = 0;
        if(this.enemy.game.getBoard().getSquareAt(position.x+1, position.y+1).canDamageSquare(MoveSlashDamage)) {
            count++;
        }
        if(this.enemy.game.getBoard().getSquareAt(position.x+1, position.y-1).canDamageSquare(MoveSlashDamage)) {
            count++;
        }
        if(this.enemy.game.getBoard().getSquareAt(position.x-1, position.y+1).canDamageSquare(MoveSlashDamage)) {
            count++;
        }
        if(this.enemy.game.getBoard().getSquareAt(position.x-1, position.y-1).canDamageSquare(MoveSlashDamage)) {
            count++;
        }
        return count;
    }

    private int killCount(Square position) {
        int count = 0;
        if(this.enemy.game.getBoard().getSquareAt(position.x+1, position.y+1).canKillSquare(MoveSlashDamage)) {
            count++;
        }
        if(this.enemy.game.getBoard().getSquareAt(position.x+1, position.y-1).canKillSquare(MoveSlashDamage)) {
            count++;
        }
        if(this.enemy.game.getBoard().getSquareAt(position.x-1, position.y+1).canKillSquare(MoveSlashDamage)) {
            count++;
        }
        if(this.enemy.game.getBoard().getSquareAt(position.x-1, position.y-1).canKillSquare(MoveSlashDamage)) {
            count++;
        }
        return count;
    }
}

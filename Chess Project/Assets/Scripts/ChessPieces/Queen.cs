using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{

    public GameObject queenProjectile;

    public override List<Square> getPossibleMoves(bool attacking) {
        List<Square> possibleMoves = new List<Square>();

        if(this.pieceUpgrades.ContainsKey(PieceMethods.getMoves)) {
            foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.getMoves]) {
                possibleMoves.AddRange(upgrade.changePossibleMoves(this, false, attacking));
            }
        }

        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{-1,-1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,1});
        offsets.Add(new int[]{0,-1});
        offsets.Add(new int[]{0,1});
        offsets.Add(new int[]{-1,0});
        offsets.Add(new int[]{1,0});

        foreach (int[] offset in offsets) {
            int distance = 1;
            Square s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            while(s != null) {
                if(s.entity != null) {
                    if(s.entity.getEntityType() == EntityType.Piece) {
                        break;
                    }
                    possibleMoves.Add(s);
                    break;
                }
                possibleMoves.Add(s);
                distance++;
                s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            }
        }
        return possibleMoves;
    }

    public override List<Square> getDefensiveMoves() {
        List<Square> possibleMoves = new List<Square>();

        if(this.pieceUpgrades.ContainsKey(PieceMethods.getMoves)) {
            foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.getMoves]) {
                possibleMoves.AddRange(upgrade.changePossibleMoves(this, true, false));
            }
        }

        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{-1,-1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,1});
        offsets.Add(new int[]{0,-1});
        offsets.Add(new int[]{0,1});
        offsets.Add(new int[]{-1,0});
        offsets.Add(new int[]{1,0});

        foreach (int[] offset in offsets) {
            int distance = 1;
            Square s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            while(s != null) {
                possibleMoves.Add(s);        
                if(s.entity != null) {
                    break;
                }
                distance++;
                s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            }
        }
        return possibleMoves;
    }

    public override IEnumerator attackAnimation(Square opponentSq) {
        Vector3 direction = (opponentSq.transform.position - this.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        GameObject firing = Instantiate(this.queenProjectile, transform.position, rotation);
        float elapsed = 0f;
        while(elapsed < this.game.playerAttackDuration) {
            firing.transform.position = Vector3.Lerp(transform.position, opponentSq.transform.position, elapsed/this.game.playerAttackDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(firing);
    }

    public override PieceType getPieceType() {
        return PieceType.Queen;
    }
}

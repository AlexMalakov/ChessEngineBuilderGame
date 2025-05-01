using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Game game;
    private Square[,] squaresOnBoard;
    public int len; public int height;

    public GameObject lightSquare;
    public GameObject darkSquare;
    public Square clickedSquare;
    public float popUpNumberWaitTime;


    public float movePieceTime;

    public virtual void generateBoard() {
        this.squaresOnBoard = new Square[this.len, this.height];
        for(int i = 0; i < this.len; i++) {
            for(int j = 0; j < this.height; j++) {
                Vector3 offset = new Vector3(this.transform.position.x + i*.75f,this.transform.position.y + j*.75f,0);
                GameObject square;
                if((i+j)%2==0) {
                    square = Instantiate(this.darkSquare, offset, Quaternion.identity);
                } else {
                    square = Instantiate(this.lightSquare, offset, Quaternion.identity);
                }
                Square fromSquare = square.GetComponent<Square>();
                fromSquare.init(i, j, this, ""+(char)('a'+i) + (char)(j));
                this.squaresOnBoard[i, j] = fromSquare;
            }
        }
    }

    public virtual void placePieces(List<ChessPiece> pieces) {
        foreach(ChessPiece p in pieces) {
            placeEntity(p);
        }
    }

    public virtual void placeEntity(Entity entity) {
        entity.position = this.squaresOnBoard[entity.startingX, entity.startingY];
        this.squaresOnBoard[entity.startingX, entity.startingY].entity = entity;
        entity.place(entity.position.transform);
    }


    public void onSquarePress(Square newClickedSquare, bool premove) {

        wipeTargetableSquares();

        if(newClickedSquare.hasChessPiece()){
            ChessPiece p = (ChessPiece)newClickedSquare.entity;
            this.wipeMoveableSquares();
            foreach(Square s in p.getPossibleMoves(false)) {
                s.setMoveable(true);
            }

            if(this.clickedSquare != null) {
                this.clickedSquare.unclickSquare();  
            }
            this.clickedSquare = newClickedSquare;
        } else if(this.clickedSquare == null) {
            this.clickedSquare = newClickedSquare;
        } else if(this.clickedSquare == newClickedSquare){
            this.clickedSquare.unclickSquare();
            this.clickedSquare=null;
            this.wipeMoveableSquares();
        }else if(newClickedSquare.isMoveable && this.clickedSquare.hasChessPiece()){ //null check as safety precaution but not really needed
            this.wipeMoveableSquares();
            this.clickedSquare.unclickSquare();

            ChessPiece p = (ChessPiece)this.clickedSquare.entity;
            
            bool moveMade = p.move(newClickedSquare);

            if(moveMade && !premove) {
                game.onPlayerMove();
                //problem check
            } else if(moveMade) {
                game.onPlayerPremove();
                game.getPlayer().premovePiece(p);
            }
            newClickedSquare.unclickSquare();
            //REMOVE A MOVE FROM THE PLAYER if true
        } else {
            this.wipeMoveableSquares();
            this.clickedSquare.unclickSquare();
            this.clickedSquare = newClickedSquare;        
        }

        //don't need this ig
        // if(this.clickedSquare != null) {
        //     foreach(ChessPiece p in this.game.getPieces()) {
        //         foreach(Square s in p.getAllMoves()) {
        //             if(s == this.clickedSquare) {
        //                 p.position.toggleTarget(true);
        //                 break;
        //             }
        //         }
        //     }
        // }
    }

    private void wipeMoveableSquares() {
        for(int i = 0; i < this.squaresOnBoard.GetLength(0); i++) {
            for(int j = 0; j < this.squaresOnBoard.GetLength(1); j++) {
                if(this.squaresOnBoard[i,j].isMoveable) {
                    this.squaresOnBoard[i,j].setMoveable(false);
                }
            }
        }
    }

    private void wipeTargetableSquares() {
        for(int i = 0; i < this.squaresOnBoard.GetLength(0); i++) {
            for(int j = 0; j < this.squaresOnBoard.GetLength(1); j++) {
                if(this.squaresOnBoard[i,j].isTargeting) {
                    this.squaresOnBoard[i,j].toggleTarget(false);
                }
            }
        }
    }


    public Square getSquareAt(int x, int y) {
        if(x >= 0 && x < this.squaresOnBoard.GetLength(0) && y >= 0 && y < this.squaresOnBoard.GetLength(1)) {
            return this.squaresOnBoard[x,y];
        }
        return null;
    }

    //calculate incoming damage to a square
    public int calculateDamage(Square target) {
        List<ChessPiece> damageDealers = new List<ChessPiece>();
        foreach(ChessPiece p in this.game.getNonPremovePieces()) {
            foreach(Square s in p.getPossibleMoves(true)) {
                if(s == target) {
                    damageDealers.Add(p);
                    break;
                }
            }
        }

        int damage = 0;
        foreach(ChessPiece p in damageDealers) {
            //display
            damage += p.getPieceDamage(target) + calculateDefense(p.position, false);
        }

        damageDealers = new List<ChessPiece>();

        foreach(ChessPiece p in this.game.getPremovePieces()) {
            foreach(Square s in p.getPossibleMoves(true)) {
                if(s == target) {
                    damageDealers.Add(p);
                    break;
                }
            }
        }

        foreach(ChessPiece p in damageDealers) {
            //display
            damage += p.getPieceDamage(target) + calculateDefense(p.position, true);
        }

        return damage; //THIS IS GOING TO CAUSE A BUG BECAUSE IENUMERATOR VERSUS INT RETURN TYPE :(
        //get all the pieces attacking a square
        //sum the defense damage bonus that they all get
    }

    public IEnumerator performDamagePhase(Enemy target) {
        List<ChessPiece> damageDealers = new List<ChessPiece>();
        foreach(ChessPiece p in this.game.getNonPremovePieces()) {
            foreach(Square s in p.getPossibleMoves(true)) {
                if(s == target.position) {
                    damageDealers.Add(p);
                    break;
                }
            }
        }

        foreach(ChessPiece p in damageDealers) {
            //display
            p.popUpAction(PopUpType.damage);
            int damage = p.getPieceDamage(target.position) + calculateDefense(p.position, false);
            enemy.takeDamage(damage);
            yield return new WaitForSeconds(this.popUpNumberWaitTime);
            unAssistAllSquares();

        }

        damageDealers = new List<ChessPiece>();

        foreach(ChessPiece p in this.game.getPremovePieces()) {
            foreach(Square s in p.getPossibleMoves(true)) {
                if(s == target.position) {
                    damageDealers.Add(p);
                    break;
                }
            }
        }

        foreach(ChessPiece p in damageDealers) {
            //display
            p.popUpAction(PopUpType.damage);
            int damage = p.getPieceDamage(target.position) + calculateDefense(p.position, true);
            enemy.takeDamage(damage);
            yield return new WaitForSeconds(this.popUpNumberWaitTime);
            unAssistAllSquares();
        }
    }

    public IEnumerator assignEffectiveDefense() {
        foreach(ChessPiece p in this.game.getNonPremovePieces()) {
            //display 
            p.popUpAction(PopUpType.defense);
            p.assignEffectiveDefense(calculateDefense(p.position, false));
            yield return new WaitForSeconds(this.popUpNumberWaitTime);
            unAssistAllSquares();
        }

        foreach(ChessPiece p in this.game.getPremovePieces()) {
            //display
            p.popUpAction(PopUpType.defense);
            p.assignEffectiveDefense(calculateDefense(p.position, true));
            yield return new WaitForSeconds(this.popUpNumberWaitTime);
            unAssistAllSquares();
        }
    }

    private void unAssistAllSquares() {
        foreach(Square s in this.squaresOnBoard) {
            s.isAssisting(false);
        }
    }

    public int calculateDefense(Square square, bool premove) {
        int defense = 0;
        List<ChessPiece> defenders = (premove) ? this.game.getPremovePieces() : this.game.getNonPremovePieces();
        foreach(ChessPiece d in defenders) {
            foreach(Square s in d.getAllMoves()) {
                if(s == square) {
                    defense += d.defense;
                    s.isAssisting(true);
                    break;
                }
            }
        }

        return defense;
    }

    public void onPieceTaken(ChessPiece p) {
        p.position.entity = null;
        this.game.player.onPieceDeath(p);
        this.game.graveyard.addToGraveyard(p);
    }

    public void returnDamage(Square target, int defense) {
        foreach(ChessPiece p in this.game.getPieces()) { //return damage to all since we are assuming premove pieces can deal damage
            foreach(Square s in p.getPossibleMoves(false)) {
                if(s == target) {
                    p.takeDamage(defense);
                    break;
                }
            }
        }
    }

    public IEnumerator slideObj(GameObject obj, Square destination) {
        float elapsed = 0f;

        while(elapsed < this.movePieceTime) {
            obj.transform.position = Vector3.Lerp(obj.transform.position, destination.transform.position, elapsed / this.movePieceTime);
            elapsed += Time.deltaTime;

            yield return null;
        }
    }
}

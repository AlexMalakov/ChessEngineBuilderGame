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
            p.place(this.squaresOnBoard[p.startingX, p.startingY].transform);
            this.squaresOnBoard[p.startingX,p.startingY].piece = p;
            p.position = this.squaresOnBoard[p.startingX, p.startingY];
        }
    }

    public virtual void placeEnemy(Enemy enemy) {
        enemy.position = this.squaresOnBoard[enemy.startingX, enemy.startingY];
        this.squaresOnBoard[enemy.startingX, enemy.startingY].enemy = enemy;
        enemy.place(enemy.position.transform);
    }


    public void onSquarePress(Square newClickedSquare, bool premove) {

        wipeTargetableSquares();

        if(newClickedSquare.piece != null){
            this.wipeMoveableSquares();
            foreach(Square s in newClickedSquare.piece.getPossibleMoves()) {
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
        }else if(newClickedSquare.isMoveable && this.clickedSquare.piece != null){ //null check as safety precaution but not really needed
            this.wipeMoveableSquares();
            this.clickedSquare.unclickSquare();


            
            bool moveMade = this.clickedSquare.piece.move(newClickedSquare);

            if(moveMade && !premove) {
                game.onPlayerMove();
                //problem check
            } else if(moveMade) {
                game.onPlayerPremove();
                game.getPlayer().premovePiece(this.clickedSquare.piece);
            }
            newClickedSquare.unclickSquare();
            //REMOVE A MOVE FROM THE PLAYER if true
        } else {
            this.wipeMoveableSquares();
            this.clickedSquare.unclickSquare();
            this.clickedSquare = newClickedSquare;        
        }

        if(this.clickedSquare != null) {
            foreach(ChessPiece p in this.game.getPieces()) {
                foreach(Square s in p.getAllMoves()) {
                    if(s == this.clickedSquare) {
                        p.position.toggleTarget(true);
                        break;
                    }
                }
            }
        }
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
            foreach(Square s in p.getPossibleMoves()) {
                if(s == target) {
                    damageDealers.Add(p);
                    break;
                }
            }
        }

        int damage = 0;
        foreach(ChessPiece p in damageDealers) {
            damage += p.getPieceDamage() + calculateDefense(p.position, false);
        }

        damageDealers = new List<ChessPiece>();

        foreach(ChessPiece p in this.game.getPremovePieces()) {
            foreach(Square s in p.getPossibleMoves()) {
                if(s == target) {
                    damageDealers.Add(p);
                    break;
                }
            }
        }

        foreach(ChessPiece p in damageDealers) {
            damage += p.getPieceDamage() + calculateDefense(p.position, true);
        }

        return damage;
        //get all the pieces attacking a square
        //sum the defense damage bonus that they all get
    }

    public void assignEffectiveDefense() {
        foreach(ChessPiece p in this.game.getNonPremovePieces()) {
            p.assignEffectiveDefense(calculateDefense(p.position, false));
        }

        foreach(ChessPiece p in this.game.getPremovePieces()) {
            p.assignEffectiveDefense(calculateDefense(p.position, true));
        }
    }

    public int calculateDefense(Square square, bool premove) {
        int defense = 0;
        List<ChessPiece> defenders = (premove) ? this.game.getPremovePieces() : this.game.getNonPremovePieces();
        foreach(ChessPiece d in defenders) {
            foreach(Square s in d.getAllMoves()) {
                if(s == square) {
                    defense += d.defense;
                    break;
                }
            }
        }

        return defense;
    }

    public void onPieceTaken(ChessPiece p) {
        p.position.piece = null;
        this.game.player.onPieceDeath(p);
        this.game.graveyard.addToGraveyard(p);
    }

    public void returnDamage(Square target, int defense) {
        foreach(ChessPiece p in this.game.getPieces()) { //return damage to all since we are assuming premove pieces can deal damage
            foreach(Square s in p.getPossibleMoves()) {
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

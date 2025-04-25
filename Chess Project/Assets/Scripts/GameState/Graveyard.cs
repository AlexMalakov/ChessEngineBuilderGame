using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    public List<ChessPiece> deadPieces;
    public Transform gravePoint;
    public float pieceSpacing;

    public void addToGraveyard(ChessPiece piece) {
        piece.transform.position = gravePoint.position + new Vector3(pieceSpacing * deadPieces.Count , 0, 0);
        deadPieces.Add(piece);
    }

    public void resurrectPieces() {
        //does nothing right now
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Payload : MonoBehaviour
{
    public Game game;
    
    public abstract void activate(Square s, bool targetPieces, bool targetEnemies, bool targetMinions, int damage);
}

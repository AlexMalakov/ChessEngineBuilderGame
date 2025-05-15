using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //dictates the effects of the projectile
    public Payload payload;
    public Game game; 
    public GameObject projectileModel;
    
    public bool targetPieces;
    public bool targetHostile;

    protected int deltaX; protected int deltaY;

    public float flySpeed = .2f;

    public virtual IEnumerator launch(Square origin, int deltaX, int deltaY, int damage) {
        this.deltaX = deltaX; this.deltaY = deltaY;
        Square s = this.game.getBoard().getSquareAt(origin.x, origin.y);
        while (true) {
            if(this.game.getBoard().getSquareAt(s.x+deltaX, s.y+deltaY) == null && releasePayload(s, true, damage)) {
                break;
            } else if(this.game.getBoard().getSquareAt(s.x+deltaX, s.y+deltaY) == null) {
                continue;
            }

            s = this.game.getBoard().getSquareAt(s.x+deltaX, s.y+deltaY);
            
            if(s.entity != null && releasePayload(s, false, damage)) {
                break;
            }
        }

        this.payload.activate(s, this.targetPieces, this.targetHostile, damage);
        yield return projectileAnimation(origin, s);
    }

    //change me to determine certain behaviors of the projectile
    public virtual bool releasePayload(Square s, bool bounds, int damage) {
        return true;
    }

    public virtual IEnumerator projectileAnimation(Square origin, Square destination) {
        Vector3 startingPos = transform.position;
        float elapsed = 0;
        while(elapsed < this.flySpeed) {
            transform.position = Vector3.Lerp(startingPos, destination.transform.position, elapsed/this.flySpeed);
            elapsed += Time.deltaTime;

            yield return null;
        }

        Destroy(this);
    }

}

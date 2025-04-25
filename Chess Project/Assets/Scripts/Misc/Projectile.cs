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
    public bool targetEnemies;
    public bool targetMinions;
    public float flySpeed = .2f;

    public virtual IEnumerator launch(Square origin, int deltaX, int deltaY, int damage) {
        int x = origin.x; int y = origin.y;
        while (true) {

            if(game.getBoard().getSquareAt(x+deltaX, y+deltaY) == null) {
                break;
            } 

            x+= deltaX; y+= deltaY;
            
            if((game.getBoard().getSquareAt(x,y).piece != null && targetPieces) || (game.getBoard().getSquareAt(x,y).enemy != null && targetEnemies)) {
                this.payload.activate(game.getBoard().getSquareAt(x,y), targetPieces, targetEnemies, targetMinions, damage);
                break;
            } //else if(game.getBoard().getSquareAt(x,y).minion != null && targetMinions){}
        }

        GameObject proj = Instantiate(projectileModel, origin.transform.position, Quaternion.identity);
        float elapsed = 0;
        while(elapsed < this.flySpeed) {
            proj.transform.position = Vector3.Lerp(proj.transform.position, game.getBoard().getSquareAt(x,y).transform.position, elapsed/this.flySpeed);
            elapsed += Time.deltaTime;

            yield return null;
        }

        Destroy(proj);
    }
}

using System;
using UnityEngine;

public abstract class IBoardEntity: ILerpable {

    [SerializeField]
    private GemType type;

    public Position position { get; private set; } = new Position(-1, -1);

    [SerializeField]
    private bool matchable = true;
    public Position displacementPosition { get; private set; }
    private Func<Position, Vector3> positionToVector;

    public bool getMatchable() {
        return matchable;
    }

    public void StartBE(Func<Position, Vector3> positionToVector) {
        this.positionToVector = positionToVector;
    }

    public void setPosition (Position position) {
        this.position = new Position(position.x, position.y);
    }

    public void setDisplacement(Position displacementPosition, float speed) {
        this.displacementPosition = displacementPosition;
        displaceLerpTo(positionToVector(displacementPosition), speed);
    }

    public void setDisplacementTimed(Position displacementPosition, float time) {
        this.displacementPosition = displacementPosition;
        displaceLerpToTimed(positionToVector(displacementPosition), time);
    }

    public void clearDisplacement(float speed) {
        displacementPosition = null;
        displaceLerpTo(new Vector3(0,0,0), speed);
    }

    public void clearDisplacementTimed(float time) {
        displacementPosition = null;
        displaceLerpToTimed(new Vector3(0, 0, 0), time);
    }


    // there might eventually be a difference here
    public void goToPosition(Position position) {
        this.position = new Position(position.x, position.y);
    }

    public GemType getType() {
        return type;
    }

    //debugging
    void OnMouseDown() {
        FindObjectOfType<Board>().removeBoardEntity(position);
        FindObjectOfType<Board>().checkFalling();

    }


}
public enum GemType { YellowGem, RedGem, BlueGem, GreenGem, PurpleGem, Blocker};

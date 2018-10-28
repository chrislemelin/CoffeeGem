using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position {

    public int x { get; }
    public int y { get; }

    public Position(int x,int y) {
        this.x = x;
        this.y = y;
    }

    public Position(Position position) {
        x = position.x;
        y = position.y;
    }

    public override bool Equals(object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType())) {
            return false;
        } else {
            Position other = (Position)obj;
            return other.x == this.x && other.y == this.y;
        }
    }

    public override int GetHashCode() {
        return new Vector2(x, y).GetHashCode();
    }

    public Position getNormalized() {
        Vector2 helper = new Vector2(x, y).normalized;
        return new Position((int)helper.x, (int)helper.y);
    }

    public Vector3 toVec() {
        return new Vector3(x, y, 0);
    }

    public static Position operator -(Position b, Position c) {
        return new Position(b.x - c.x, b.y - c.y);
    }

    public static Position operator +(Position b, Position c) {
        return new Position(b.x + c.x, b.y + c.y);
    }

    public static Position operator *(Position a, int mult) {
        return new Position(a.x * mult, a.y * mult);
    }
}

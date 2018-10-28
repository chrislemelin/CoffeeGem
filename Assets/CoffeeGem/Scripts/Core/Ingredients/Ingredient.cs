using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ingredient {

    public Dictionary<Position, IBoardEntity> boardEntities { get; protected set; } = new Dictionary<Position, IBoardEntity>();
    public IngredientDisplacement displacementType { get; protected set; } = IngredientDisplacement.Up;
    public IngredientType type { get; protected set; }

    public Position getDimensions() {
        int x = 0;
        int y = 0;
        foreach(Position position in boardEntities.Keys) {
            x = Mathf.Max(position.x, x);
            y = Mathf.Max(position.y, y);
        }
        return new Position(x+1, y+1);
    }

    public Dictionary<Position, Position> getDisplacements(Position position) {
        Dictionary<Position, Position> displacements = new Dictionary<Position, Position>();
        foreach (Position bEPosition in boardEntities.Keys) {
            displacements.Add(bEPosition + position, getDisplacement());
        }
        return displacements;
    }

    public Position getDisplacement() {
        switch (displacementType) {
            case IngredientDisplacement.Down:
                return new Position(0, -1);
            case IngredientDisplacement.Up:
                return new Position(0, 1);
            case IngredientDisplacement.Left:
                return new Position(-1, 0);
            case IngredientDisplacement.Right:
                return new Position(1, 0);
        }
        return new Position(0, 0);
    }
}

public enum IngredientDisplacement { Up, Down, Left, Right}

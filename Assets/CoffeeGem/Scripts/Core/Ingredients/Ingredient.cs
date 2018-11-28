using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient {

    public Dictionary<Position, IBoardEntity> boardEntities { get; protected set; } = new Dictionary<Position, IBoardEntity>();
    public IngredientDisplacement displacementType { get; protected set; } = IngredientDisplacement.Up;
    public IngredientType type { get; protected set; }

    public List<GemType> fallingTypes { get; protected set; } = new List<GemType>();
    public string title { get; protected set; }
    public string flavorText;
    public string description;

    public bool bonusPlacement = false;
    public bool showAdjacent;

    public Action<ISet<Position>> deleteGems;
    public Action<Dictionary<Position, GemType>> changeGems;

    public Ingredient() {
        description = "A basic ingredient";
        flavorText = "Chocolate tasting notes";
    }

    public static Ingredient copy(Ingredient other) {
        Ingredient ingredient = (Ingredient)other.GetType().GetConstructor(Type.EmptyTypes).Invoke(null);
        ingredient.boardEntities = other.boardEntities;
        ingredient.displacementType = other.displacementType;
        ingredient.type = other.type;

        ingredient.title = other.title;
        ingredient.flavorText = other.flavorText;
        ingredient.description = other.description;
        ingredient.fallingTypes = new List<GemType>(other.fallingTypes);
        ingredient.bonusPlacement = other.bonusPlacement;
        return ingredient;
    }

    public void initIngredient(Action<ISet<Position>> deleteGems, Action<Dictionary<Position, GemType>> changeGems) {
        this.deleteGems = deleteGems;
        this.changeGems = changeGems;
    }

    public virtual void boardEntitiesPushed(List<IBoardEntity> removedBoardEntities) {}

    public virtual void ingredientPlaced(Dictionary<Position, IBoardEntity> board, Position placedPosition) {}

    public Position getMinPosition() {
        int xMin = int.MaxValue;
        int yMin = int.MaxValue;
        foreach (Position position in boardEntities.Keys) {
            xMin = Mathf.Min(position.x, xMin);
            yMin = Mathf.Min(position.y, yMin);
        }
        return new Position(xMin, yMin);
    }

    public Position getMaxPosition() {
        int xMax = int.MinValue;
        int yMax = int.MinValue;
        foreach (Position position in boardEntities.Keys) {
            xMax = Mathf.Max(position.x, xMax);
            yMax = Mathf.Max(position.y, yMax);
        }
        return new Position(xMax, yMax);
    }

    public Position getDimensions() {
        Position max = getMaxPosition();
        Position min = getMinPosition();

        return new Position(Mathf.Abs(min.x - max.x) + 1, Mathf.Abs(min.y - max.y) + 1);
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

    public HashSet<Position> getAdjacentPositions() {
        HashSet<Position> returnPositions = new HashSet<Position>();
        foreach (Position position in boardEntities.Keys) {
            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    returnPositions.Add(position + new Position(x,y));
                }
            }
        }
        returnPositions.RemoveWhere((position) => boardEntities.ContainsKey(position));

        return returnPositions;
    }
}

public enum IngredientDisplacement { Up, Down, Left, Right}

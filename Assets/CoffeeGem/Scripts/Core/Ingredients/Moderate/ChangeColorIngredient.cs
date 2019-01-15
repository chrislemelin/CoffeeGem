using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorIngredient : Ingredient {

    public ChangeColorIngredient(BELibrary bELibrary) : base(bELibrary) {
        title = "Color Changer";
        description = "Changes adjacent blue gems to purple gems";
        flavorText = "Where those gems always purple?";
        showAdjacent = true;

        type = IngredientType.BlueToPurple;
        displacementType = IngredientDisplacement.Down;

        boardEntities.Add(new Position(0, 1), bELibrary.get(GemType.PurpleGem));
        boardEntities.Add(new Position(0, 0), bELibrary.get(GemType.GreenGem));
    }

    public override void ingredientPlaced(Dictionary<Position, IBoardEntity> board, Position placedPosition) {
        base.ingredientPlaced(board, placedPosition);
        HashSet<Position> adjacentPositions = getAdjacentPositions();
        Dictionary<Position, GemType> gemsToChange = new Dictionary<Position, GemType>();
        foreach (Position position in adjacentPositions) {
            Position realPosition = position + placedPosition;
            if (board.ContainsKey(realPosition) && board[realPosition].getType() == GemType.BlueGem) {
                gemsToChange.Add(realPosition, GemType.PurpleGem);
            }
        }
        changeGems(gemsToChange); 
    }

}



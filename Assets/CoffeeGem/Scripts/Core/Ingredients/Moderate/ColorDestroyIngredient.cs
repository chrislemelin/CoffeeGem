using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDestroyIngredient : Ingredient {

    public ColorDestroyIngredient(BELibrary bELibrary) : base(bELibrary) {
        title = "Yellow Destroyer";
        description = "Destroys all yellow gems";
        flavorText = "This ingredient really doesn't play well with others";

        type = IngredientType.YellowDestroy;
        displacementType = IngredientDisplacement.Left;


        boardEntities.Add(new Position(0, 1), bELibrary.get(GemType.GreenGem));
        boardEntities.Add(new Position(0, 0), bELibrary.get(GemType.BlueGem));
    }

    public override void ingredientPlaced(Dictionary<Position, IBoardEntity> board, Position placedPosition) {
        HashSet<Position> yellowGems = new HashSet<Position>();
        foreach (KeyValuePair<Position, IBoardEntity> entry in board) {
            if (entry.Value.getType() == GemType.YellowGem) {
                yellowGems.Add(entry.Key);
            }
        }

        deleteGems(yellowGems);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBlueIngredient : Ingredient {

	public BasicBlueIngredient() {
        title = "Blue Mountain";
        description = "A basic ingredient";
        flavorText = "For when your feeling blue";

        type = IngredientType.BasicBlue;
        displacementType = IngredientDisplacement.Right;


        boardEntities.Add(new Position(0, 0), BELibrary.Instance.get(GemType.BlueGem));
        boardEntities.Add(new Position(0, 1), BELibrary.Instance.get(GemType.RedGem));
        boardEntities.Add(new Position(1, 2), BELibrary.Instance.get(GemType.BlueGem));
    }

}

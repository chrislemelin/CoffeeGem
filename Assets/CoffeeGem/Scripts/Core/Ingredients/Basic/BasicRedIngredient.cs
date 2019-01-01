using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRedIngredient : Ingredient {

	public BasicRedIngredient() {
        title = "House Blend";
        description = "A basic ingredient";
        flavorText = "Coreys in the house";


        type = IngredientType.BasicRed;
        displacementType = IngredientDisplacement.Up;

        boardEntities.Add(new Position(0, 0), BELibrary.Instance.get(GemType.RedGem));
        boardEntities.Add(new Position(1, 0), BELibrary.Instance.get(GemType.YellowGem));
        boardEntities.Add(new Position(1, 1), BELibrary.Instance.get(GemType.RedGem));

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRedIngredient : Ingredient {

	public BasicRedIngredient(BELibrary bELibrary) : base(bELibrary) {
        title = "House Blend";
        description = "A basic ingredient";
        flavorText = "Coreys in the house";


        type = IngredientType.BasicRed;
        displacementType = IngredientDisplacement.Up;

        boardEntities.Add(new Position(0, 0), bELibrary.get(GemType.RedGem));
        boardEntities.Add(new Position(1, 0), bELibrary.get(GemType.YellowGem));
        boardEntities.Add(new Position(1, 1), bELibrary.get(GemType.RedGem));

    }

}

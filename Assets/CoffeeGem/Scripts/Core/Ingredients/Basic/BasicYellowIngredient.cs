using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicYellowIngredient : Ingredient {

	public BasicYellowIngredient(BELibrary bELibrary) : base(bELibrary) {
        title = "Kona";
        description = "A basic ingredient";
        flavorText = "The champagne of coffee";

        type = IngredientType.BasicYellow;
        displacementType = IngredientDisplacement.Down;


        boardEntities.Add(new Position(0, 0), bELibrary.get(GemType.YellowGem));
        boardEntities.Add(new Position(1, 1), bELibrary.get(GemType.YellowGem));
    }

}

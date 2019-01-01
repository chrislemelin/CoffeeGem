using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicYellowIngredient : Ingredient {

	public BasicYellowIngredient() {
        title = "Kona";
        description = "A basic ingredient";
        flavorText = "The champagne of coffee";

        type = IngredientType.BasicYellow;
        displacementType = IngredientDisplacement.Down;


        boardEntities.Add(new Position(0, 0), BELibrary.Instance.get(GemType.YellowGem));
        boardEntities.Add(new Position(1, 1), BELibrary.Instance.get(GemType.YellowGem));
    }

}

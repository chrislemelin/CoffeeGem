using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicYellow2Ingredient : Ingredient {

	public BasicYellow2Ingredient() {
        this.title = "Basic Yellow 2";

        type = IngredientType.BasicYellow2;
        displacementType = IngredientDisplacement.Down;


        boardEntities.Add(new Position(0, 0), BELibrary.Instance.get(GemType.YellowGem));
        boardEntities.Add(new Position(1, 1), BELibrary.Instance.get(GemType.YellowGem));

    }

}

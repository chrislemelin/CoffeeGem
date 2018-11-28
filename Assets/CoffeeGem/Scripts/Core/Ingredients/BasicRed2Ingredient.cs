using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRed2Ingredient : Ingredient {

	public BasicRed2Ingredient() {
        title = "Basic Red";

        type = IngredientType.BasicRed2;
        displacementType = IngredientDisplacement.Up;

        boardEntities.Add(new Position(0, 0), BELibrary.Instance.get(GemType.RedGem));
        boardEntities.Add(new Position(1, 0), BELibrary.Instance.get(GemType.YellowGem));
        boardEntities.Add(new Position(1, 1), BELibrary.Instance.get(GemType.RedGem));

    }

}

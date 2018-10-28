using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRedIngredient : Ingredient {

	public BasicRedIngredient() {
        type = IngredientType.BasicRed;
        displacementType = IngredientDisplacement.Up;

        boardEntities.Add(new Position(0, 0), BELibrary.Instance.get(BEType.RedGem));
        boardEntities.Add(new Position(1, 0), BELibrary.Instance.get(BEType.YellowGem));
        boardEntities.Add(new Position(1, 1), BELibrary.Instance.get(BEType.RedGem));

    }

}

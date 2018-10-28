using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicYellowIngredient : Ingredient {

	public BasicYellowIngredient() {
        type = IngredientType.BasicYellow;
        displacementType = IngredientDisplacement.Down;


        boardEntities.Add(new Position(0, 0), BELibrary.Instance.get(BEType.YellowGem));
        boardEntities.Add(new Position(1, 1), BELibrary.Instance.get(BEType.YellowGem));

    }

}

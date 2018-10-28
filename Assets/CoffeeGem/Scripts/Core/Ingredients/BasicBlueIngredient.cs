using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBlueIngredient : Ingredient {

	public BasicBlueIngredient() {
        type = IngredientType.BasicBlue;
        displacementType = IngredientDisplacement.Right;


        boardEntities.Add(new Position(0, 0), BELibrary.Instance.get(BEType.BlueGem));
        boardEntities.Add(new Position(0, 1), BELibrary.Instance.get(BEType.RedGem));
        boardEntities.Add(new Position(0, 2), BELibrary.Instance.get(BEType.BlueGem));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGreenIngredient : Ingredient {

	public BasicGreenIngredient() {
        type = IngredientType.BasicGreen;
        displacementType = IngredientDisplacement.Left;


        boardEntities.Add(new Position(0, 0), BELibrary.Instance.get(BEType.GreenGem));
        boardEntities.Add(new Position(1, -1), BELibrary.Instance.get(BEType.GreenGem));
        boardEntities.Add(new Position(1, 0), BELibrary.Instance.get(BEType.BlueGem));
        boardEntities.Add(new Position(1, 1), BELibrary.Instance.get(BEType.GreenGem));

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGreen2Ingredient : Ingredient {

	public BasicGreen2Ingredient() {
        title = "Basic Green 2";


        type = IngredientType.BasicGreen2;
        displacementType = IngredientDisplacement.Left;


        boardEntities.Add(new Position(0, 0), BELibrary.Instance.get(GemType.GreenGem));
        boardEntities.Add(new Position(1, -1), BELibrary.Instance.get(GemType.GreenGem));
        boardEntities.Add(new Position(1, 0), BELibrary.Instance.get(GemType.BlueGem));
        boardEntities.Add(new Position(1, 1), BELibrary.Instance.get(GemType.GreenGem));

    }

}

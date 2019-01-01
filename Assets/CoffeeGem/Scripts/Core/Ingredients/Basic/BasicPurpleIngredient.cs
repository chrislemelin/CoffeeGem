using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPurpleIngredient : Ingredient {

    public BasicPurpleIngredient() {
        title = "Purple Haze";
        description = "A basic ingredient";
        flavorText = "I'm tired of writing these flavor texts";

        type = IngredientType.BasicPurple;
        displacementType = IngredientDisplacement.Right;


        boardEntities.Add(new Position(1, -1), BELibrary.Instance.get(GemType.RedGem));
        boardEntities.Add(new Position(1, 1), BELibrary.Instance.get(GemType.PurpleGem));
        boardEntities.Add(new Position(0, 0), BELibrary.Instance.get(GemType.PurpleGem));

    }
}

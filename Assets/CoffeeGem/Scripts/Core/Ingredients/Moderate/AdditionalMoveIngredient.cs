using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalMoveIngredient : Ingredient {

    public AdditionalMoveIngredient(BELibrary bELibrary) : base(bELibrary) {
        title = "Mixed Blend";
        description = "Allows you to play another ingredient";
        flavorText = "This blend works wonders if you mix it with somethings";

        type = IngredientType.AdditionalMove;
        displacementType = IngredientDisplacement.Left;
        bonusPlacement = true;

        boardEntities.Add(new Position(0, 0), bELibrary.get(GemType.YellowGem));
    }
}

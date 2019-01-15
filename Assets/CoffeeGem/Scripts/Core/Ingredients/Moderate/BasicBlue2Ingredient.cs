using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBlackIngredient : Ingredient {

    public SmallBlackIngredient(BELibrary bELibrary) : base(bELibrary) {
        title = "Thick Coffee";
        description = "Contains Black Gems which cannot be matched";
        flavorText = "Might be to thick to handle";

        type = IngredientType.SmallBlack;
        displacementType = IngredientDisplacement.Up;
        fallingTypes = new List<GemType> { GemType.RedGem, GemType.RedGem };

        boardEntities.Add(new Position(0, 0), bELibrary.get(GemType.BlueGem));
        boardEntities.Add(new Position(1, 0), bELibrary.get(GemType.BlueGem));
        boardEntities.Add(new Position(1, 1), bELibrary.get(GemType.Blocker));
        boardEntities.Add(new Position(1, 2), bELibrary.get(GemType.RedGem));
        boardEntities.Add(new Position(2, 2), bELibrary.get(GemType.RedGem));

    }
}

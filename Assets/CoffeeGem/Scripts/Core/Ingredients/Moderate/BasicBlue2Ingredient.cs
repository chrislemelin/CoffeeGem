using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBlackIngredient : Ingredient {

    public SmallBlackIngredient() {
        title = "Thick Coffee";
        description = "Contains Black Gems which cannot be matched";
        flavorText = "Might be to thick to handle";

        type = IngredientType.SmallBlack;
        displacementType = IngredientDisplacement.Up;
        fallingTypes = new List<GemType> { GemType.RedGem, GemType.RedGem };

        boardEntities.Add(new Position(0, 0), BELibrary.Instance.get(GemType.BlueGem));
        boardEntities.Add(new Position(1, 0), BELibrary.Instance.get(GemType.BlueGem));
        boardEntities.Add(new Position(1, 1), BELibrary.Instance.get(GemType.Blocker));
        boardEntities.Add(new Position(1, 2), BELibrary.Instance.get(GemType.RedGem));
        boardEntities.Add(new Position(2, 2), BELibrary.Instance.get(GemType.RedGem));

    }
}

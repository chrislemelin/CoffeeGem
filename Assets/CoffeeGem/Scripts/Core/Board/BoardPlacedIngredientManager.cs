using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlacedIngredientManager : MonoBehaviour {

    protected Ingredient placedIngredient = null;

    protected List<GemType> getPlacedIngredientFallingTypes() {
        return placedIngredient != null ? placedIngredient.fallingTypes : new List<GemType>();
    }

    protected bool getPlacedIngredientBonusPlacement() {
        return placedIngredient != null ? placedIngredient.bonusPlacement : false;
    }

    protected List<IngredientMatch> getPlacedIngredientMatches(Position position) {
        return placedIngredient != null ? placedIngredient.getBonusMatches(position) : new List<IngredientMatch>();
    }

}

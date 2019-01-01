using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IIngredientUIItem {

    public IngredientType ingredientType;
    public Ingredient ingredient;


    public IIngredientUIItem(IngredientType ingredientType) {
        this.ingredientType = ingredientType;
        ingredient = IngredientLibrary.Instance.get(ingredientType);
    }
}

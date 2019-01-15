using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IIngredientInventoryItem {

    public IngredientType ingredientType;
    public Ingredient ingredient;

    public IIngredientInventoryItem(IngredientType ingredientType) {
        this.ingredientType = ingredientType;
        ingredient = LibraryManager.instance.get<IngredientLibrary>().get(ingredientType);
    }
}

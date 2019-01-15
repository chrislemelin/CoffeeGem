using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInventoryItemShop : IIngredientInventoryItem {

    public int cost;

    public IngredientInventoryItemShop(IngredientType ingredientType, int cost) : base (ingredientType) {
        this.cost = cost;
    }
}

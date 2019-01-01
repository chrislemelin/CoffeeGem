using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientUIItemShop : IIngredientUIItem {

    public int cost;

    public IngredientUIItemShop(IngredientType ingredientType, int cost) : base (ingredientType) {
        this.cost = cost;
    }
}

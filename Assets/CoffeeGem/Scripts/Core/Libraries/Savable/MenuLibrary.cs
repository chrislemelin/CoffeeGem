using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuLibrary : ILibrary {

    [SerializeField]
    private int size = 5;

    private List<IngredientType?> ingredients;

    public event Action onFull;
    public event Action onNotFull;

    public void init() {
        ingredients = new List<IngredientType?>();

        // initial pool
        ingredients.Add(IngredientType.BasicBlue);
        ingredients.Add(IngredientType.BasicGreen);
        ingredients.Add(IngredientType.BasicPurple);
        ingredients.Add(IngredientType.BasicRed);
        ingredients.Add(IngredientType.BasicYellow);
        
    }

    public void addIngredient(Ingredient ingredient, int index) {
        if (index < size) {
            ingredients.RemoveAt(index);
            ingredients.Insert(index, ingredient.type);
            checkFull();
        }     
    }

    public void removeIngredientAt(int index) {
        ingredients.RemoveAt(index);
        ingredients.Insert(index, null);
        checkFull();
    }

    public void removeIngredient(Ingredient ingredient) {
        for (int a = 0; a < ingredients.Count; a++) {
            if (ingredients[a] == ingredient.type) {
                ingredients.RemoveAt(a);
                ingredients.Insert(a, null);
                checkFull();
                return;
            }
        }
    }

    public void checkFull() {
       if (isFull()) {
            onFull?.Invoke();
        } else {
            onNotFull?.Invoke();
        }
    }

    public bool isFull() {
        foreach (IngredientType? ingredient in ingredients) {
            if (ingredient == null) {
                return false;
            }
        }
        return true;
    }

    public List<Ingredient> getIngredients() {
        IngredientLibrary ingredientLibrary = LibraryManager.instance.get<IngredientLibrary>();

        return ingredients.Select(i => i != null ? ingredientLibrary.get((IngredientType)i) : null).ToList();
    }
}

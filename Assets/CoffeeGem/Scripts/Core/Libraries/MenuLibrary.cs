using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLibrary : MonoBehaviour {

    [SerializeField]
    private int size = 5;

    public static MenuLibrary Instance;
    private List<Ingredient> ingredients;
    public event Action onFull;
    public event Action onNotFull;
    
    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        ingredients = new List<Ingredient>();
        for (int a = 0; a < size; a++) {
            ingredients.Add(null);
        }
    }

    public void addIngredient(Ingredient ingredient, int index) {
        if (index < size) {
            ingredients.RemoveAt(index);
            ingredients.Insert(index, ingredient);
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
            if (ingredients[a] == ingredient) {
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
        foreach (Ingredient ingredient in ingredients) {
            if (ingredient == null) {
                return false;
            }
        }
        return true;
    }

    public List<Ingredient> getIngredients() {
        return ingredients;
    }
}

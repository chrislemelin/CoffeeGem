using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientLibrary : MonoBehaviour {

    public static IngredientLibrary Instance;

    private List<Ingredient> ingredients = new List<Ingredient>();

    private Dictionary<IngredientType, Ingredient> ingredientsDict = new Dictionary<IngredientType, Ingredient>();

    private void Awake() {
        if (Instance != null)
            Destroy(Instance);
        else {
            makeDictionary(ingredients);
            Instance = this;
        }

    }

    public Ingredient get(IngredientType type) {
        return ingredientsDict[type];
    }

    private void makeDictionary (List<Ingredient> ingredients) {
        ingredients.Add(new BasicBlueIngredient());
        ingredients.Add(new BasicRedIngredient());
        ingredients.Add(new BasicGreenIngredient());
        ingredients.Add(new BasicYellowIngredient());
        ingredients.Add(new BasicPurpleIngredient());


        ingredients.Add(new SmallBlackIngredient());
        ingredients.Add(new ChangeColorIngredient());
        ingredients.Add(new ColorDestroyIngredient());
        ingredients.Add(new AdditionalMoveIngredient());

        foreach (Ingredient ingredient in ingredients) {
            ingredientsDict.Add(ingredient.type, ingredient);
        }

    }
}

public enum IngredientType { BasicRed, BasicBlue, BasicGreen, BasicYellow, BasicPurple,
                            SmallBlack, BlueToPurple, YellowDestroy, AdditionalMove}

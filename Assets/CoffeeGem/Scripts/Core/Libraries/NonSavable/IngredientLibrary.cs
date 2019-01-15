using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientLibrary : MonoBehaviour, ILibrary {

    //public static IngredientLibrary Instance;

    private List<Ingredient> ingredients = new List<Ingredient>();

    private Dictionary<IngredientType, Ingredient> ingredientsDict = new Dictionary<IngredientType, Ingredient>();

    //private void Awake() {
    //    if (Instance != null)
    //        Destroy(Instance);
    //    else {
    //        Instance = this;
    //    }
    //}

    public Ingredient get(IngredientType type) {
        return ingredientsDict[type];
    }

    public void makeDictionary () {
        BELibrary bELibrary = LibraryManager.instance.get<BELibrary>();

        ingredients.Add(new BasicBlueIngredient(bELibrary));
        ingredients.Add(new BasicRedIngredient(bELibrary));
        ingredients.Add(new BasicGreenIngredient(bELibrary));
        ingredients.Add(new BasicYellowIngredient(bELibrary));
        ingredients.Add(new BasicPurpleIngredient(bELibrary));

        ingredients.Add(new SmallBlackIngredient(bELibrary));
        ingredients.Add(new ChangeColorIngredient(bELibrary));
        ingredients.Add(new ColorDestroyIngredient(bELibrary));
        ingredients.Add(new AdditionalMoveIngredient(bELibrary));

        foreach (Ingredient ingredient in ingredients) {
            ingredientsDict.Add(ingredient.type, ingredient);
        }

    }

    public void init() {}
}

public enum IngredientType { BasicRed, BasicBlue, BasicGreen, BasicYellow, BasicPurple,
                            SmallBlack, BlueToPurple, YellowDestroy, AdditionalMove}

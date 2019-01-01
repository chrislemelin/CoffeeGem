using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuLibraryUI : MonoBehaviour {

    private List<IngredientHolder> ingredientsHolders;
    [SerializeField]
    IngredientLibrarySelection ingredientLibrarySelection;
    Dictionary<IngredientHolder, int> holderToIndex = new Dictionary<IngredientHolder, int>();

    private void Awake() {
        ingredientLibrarySelection.dropCallback = dropped;
        ingredientsHolders = GetComponentsInChildren<IngredientHolder>().ToList();
        List<Ingredient> ingredients = MenuLibrary.Instance.getIngredients();
        for (int a = 0; a < ingredientsHolders.Count; a++) {
            if (a < ingredients.Count) {
                ingredientsHolders[a].setIngredient(ingredients[a]);
            } else {
                ingredientsHolders[a].setIngredient(null);
            }
            holderToIndex.Add(ingredientsHolders[a], a);
        }   
    }

    private void dropped(Ingredient ingredient) {
        foreach(IngredientHolder ingredientHolder in ingredientsHolders) {
            RectTransform rectTransform = ingredientHolder.transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition)) {
                int index = holderToIndex[ingredientHolder];
                foreach (IngredientHolder ingredientHolderTemp in ingredientsHolders) {
                    if (ingredientHolderTemp.renderedIngredient == ingredient) {
                        MenuLibrary.Instance.removeIngredient(ingredient);
                        if (ingredientHolderTemp != ingredientHolder) {
                            ingredientHolderTemp.renderPreview(null);
                        }
                    }
                }

                if (ingredientHolder.renderedIngredient != null) {
                   // MenuLibrary.Instance.removeIngredient(ingredientHolder.renderedIngredient);
                }

                MenuLibrary.Instance.addIngredient(ingredient, index);
                ingredientHolder.setIngredient(ingredient);
            }
        }
    }
  
}

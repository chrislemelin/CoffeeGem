using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuLibraryUI : MonoBehaviour {

    private List<IngredientHolder> ingredientsHolders;
    Dictionary<IngredientHolder, int> holderToIndex = new Dictionary<IngredientHolder, int>();
    private MenuLibrary menuLibrary;

    private void Start() {
        menuLibrary = LibraryManager.instance.get<MenuLibrary>();
        ingredientsHolders = GetComponentsInChildren<IngredientHolder>().ToList();
        List<Ingredient> ingredients = menuLibrary.getIngredients();
        for (int a = 0; a < ingredientsHolders.Count; a++) {
            if (a < ingredients.Count) {
                ingredientsHolders[a].setIngredient(ingredients[a]);
            } else {
                ingredientsHolders[a].setIngredient(null);
            }
            holderToIndex.Add(ingredientsHolders[a], a);
        }   
    }

    public void dropped(Ingredient ingredient) {
        foreach(IngredientHolder ingredientHolder in ingredientsHolders) {
            RectTransform rectTransform = ingredientHolder.transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition)) {
                int index = holderToIndex[ingredientHolder];
                foreach (IngredientHolder ingredientHolderTemp in ingredientsHolders) {
                    if (ingredientHolderTemp.renderedIngredient == ingredient) {
                        menuLibrary.removeIngredient(ingredient);
                        if (ingredientHolderTemp != ingredientHolder) {
                            ingredientHolderTemp.renderIngredient(null);
                        }
                    }
                }

                if (ingredientHolder.renderedIngredient != null) {
                   // MenuLibrary.Instance.removeIngredient(ingredientHolder.renderedIngredient);
                }

                menuLibrary.addIngredient(ingredient, index);
                ingredientHolder.setIngredient(ingredient);
            }
        }
    }
  
}

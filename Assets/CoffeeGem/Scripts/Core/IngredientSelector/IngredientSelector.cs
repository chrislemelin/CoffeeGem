using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSelector : MonoBehaviour {

    [SerializeField]
    private int size = 4;

    [SerializeField]
    private GameObject ingredientSelector;

    private List<IngredientHolder> selectors = new List<IngredientHolder>();

    [SerializeField]
    private List<IngredientType> ingredients = new List<IngredientType>();

    [SerializeField]
    private Board board;


    public void Start() {
        for (int a = 0; a < size; a++) {
            GameObject selector = Instantiate(ingredientSelector);
            selector.transform.SetParent(transform, false);
            selector.GetComponent<IngredientHolder>().setIngredient(IngredientLibrary.Instance.get(ingredients[a]));
            selectors.Add(selector.GetComponent<IngredientHolder>());
        }
        foreach (IngredientHolder ingredientHolder in selectors) {
            ingredientHolder.gameObject.GetComponent<OnClick>().click += () => ingredientSelected(ingredientHolder);
        }
        ingredientSelected(selectors[0]);

    }

    private void ingredientSelected(IngredientHolder ingredientHolder) {
        board.setIngredient(ingredientHolder.ingredient);
        foreach (IngredientHolder holder in selectors) {
            if (holder == ingredientHolder) {
                holder.background.GetComponent<SpriteRenderer>().color = Color.red;
            } else {
                holder.background.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

    }




}

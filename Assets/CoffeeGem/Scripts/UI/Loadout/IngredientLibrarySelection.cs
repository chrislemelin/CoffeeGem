using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientLibrarySelection : MonoBehaviour {

    [SerializeField]
    GameObject dragTarget;

    [SerializeField]
    IngredientLibrarySelected ingredientLibrarySelected;

    [SerializeField]
    GameObject ingredientUIItemPF;

    [SerializeField]
    GameObject target;

    public Action<Ingredient> dropCallback;

    private HashSet<GameObject> renderedItems = new HashSet<GameObject>();

    private HashSet<IIngredientUIItem> ingredientItems = new HashSet<IIngredientUIItem>();

	// Use this for initialization
	void Start () {
        if (target == null) {
            target = gameObject;
        }
        addIngredient(new IngredientUIItemOwned(IngredientType.BasicBlue));
        addIngredient(new IngredientUIItemOwned(IngredientType.BasicGreen));
        addIngredient(new IngredientUIItemOwned(IngredientType.BasicPurple));
        addIngredient(new IngredientUIItemOwned(IngredientType.BasicRed));
        addIngredient(new IngredientUIItemOwned(IngredientType.BasicYellow));

        addIngredient(new IngredientUIItemShop(IngredientType.AdditionalMove, 500));
        addIngredient(new IngredientUIItemShop(IngredientType.SmallBlack, 1000));

        addIngredient(new IngredientUIItemShop(IngredientType.BlueToPurple, 1000));
        addIngredient(new IngredientUIItemShop(IngredientType.YellowDestroy, 1500));
    }

    public void addIngredient(IIngredientUIItem ingredientItem) {
        ingredientItems.Add(ingredientItem);
        renderIngredients();
    }
	

    public void renderIngredients() {
        foreach (GameObject gameObject in renderedItems) {
            Destroy(gameObject);
        }
        renderedItems.Clear();

        foreach (IIngredientUIItem ingredientItem in ingredientItems) {
            GameObject newItem = Instantiate(ingredientUIItemPF);
            newItem.GetComponent<Fade>().init();
            newItem.GetComponent<IngredientLibraryItemDragSpawner>().target = dragTarget;
            newItem.GetComponent<IngredientLibraryItemDragSpawner>().setDropCallback(dropCallback);

            newItem.transform.SetParent(target.transform, false);
            newItem.GetComponentInChildren<IngredientInventoryItem>().setItem(ingredientItem);
            newItem.GetComponentInChildren<OnEvent>().clickUp += () => ingredientClicked(ingredientItem, newItem);
            renderedItems.Add(newItem);
        }
    }

    private void ingredientClicked (IIngredientUIItem ingredientItem, GameObject gameObject) {
        foreach (GameObject ingredientUIItem in renderedItems) {
            if (ingredientUIItem != gameObject) {
                ingredientUIItem.GetComponentInChildren<ButtonCG>().toggleOff();
            }
        }
        ingredientLibrarySelected.setIngredientItem(ingredientItem);
    }
}

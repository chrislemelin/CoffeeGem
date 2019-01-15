using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInventorySelection : MonoBehaviour {

    [SerializeField]
    GameObject dragTarget;

    [SerializeField]
    IngredientInventorySelected ingredientLibrarySelected;

    [SerializeField]
    GameObject ingredientUIItemPF;

    [SerializeField]
    GameObject target;

    private MenuLibraryUI menuLibraryUI;
    private List<GameObject> renderedItems = new List<GameObject>();
    private List<IIngredientInventoryItem> ingredientItems = new List<IIngredientInventoryItem>();
    private IngredientInventoryLibrary ingredientInventoryLibrary;

	// Use this for initialization
	void Start () {
        if (target == null) {
            target = gameObject;
        }
        menuLibraryUI = FindObjectOfType<MenuLibraryUI>();
        ingredientInventoryLibrary = LibraryManager.instance.get<IngredientInventoryLibrary>();
        ingredientInventoryLibrary.inventoryChanged += updateIngredients;
        ingredientInventoryLibrary.triggerChange();

        renderedItems[0].GetComponentInChildren<ButtonCG>().OnMouseUp(true);
    }

    private void OnDestroy() {
        ingredientInventoryLibrary.inventoryChanged -= updateIngredients;
    }

    public void updateIngredients(List<IIngredientInventoryItem> items) {
        ingredientItems = items;
        renderIngredients();
    }
	

    public void renderIngredients() {
        int count = 0;
        if (renderedItems.Count == 0) {
            foreach (IIngredientInventoryItem ingredientItem in ingredientItems) {
                GameObject newItem = Instantiate(ingredientUIItemPF);
                newItem.GetComponent<Fade>().init();
                newItem.GetComponent<IngredientInventoryItemDragSpawner>().target = dragTarget;

                if (ingredientItem is IngredientInventoryItemOwned) {
                    newItem.GetComponent<IngredientInventoryItemDragSpawner>().setDropCallback(menuLibraryUI.dropped);
                } else {
                    newItem.GetComponent<IngredientInventoryItemDragSpawner>().enabled = false;
                }

                newItem.transform.SetParent(target.transform, false);
                newItem.GetComponentInChildren<IngredientInventoryItemUI>().setItem(ingredientItem);
                newItem.GetComponentInChildren<ButtonCG>().clicked += () => ingredientClicked(ingredientItem, newItem);
                count++;
                renderedItems.Add(newItem);
            }
        } else {
            foreach (IIngredientInventoryItem ingredientItem in ingredientItems) {
                GameObject gameObject = renderedItems[count];

                if (ingredientItem is IngredientInventoryItemOwned) {
                    gameObject.GetComponent<IngredientInventoryItemDragSpawner>().enabled = true;
                    gameObject.GetComponent<IngredientInventoryItemDragSpawner>().setDropCallback(menuLibraryUI.dropped);
                } else {
                    gameObject.GetComponent<IngredientInventoryItemDragSpawner>().enabled = false;
                }

                gameObject.GetComponentInChildren<ButtonCG>().clicked += () => ingredientClicked(ingredientItem, gameObject);
                gameObject.GetComponentInChildren<IngredientInventoryItemUI>().setItem(ingredientItem);
                count++;
            }
        }
       

        //renderedItems[selectedIndex].GetComponentInChildren<ButtonCG>().OnMouseUp(true);
    }

    private void ingredientClicked (IIngredientInventoryItem ingredientItem, GameObject gameObject) {
        foreach (GameObject ingredientUIItem in renderedItems) {
            if (ingredientUIItem != gameObject) {
                ingredientUIItem.GetComponentInChildren<ButtonCG>().toggleOff();
            }
        }
        ingredientLibrarySelected.setIngredientItem(ingredientItem);
    }
}

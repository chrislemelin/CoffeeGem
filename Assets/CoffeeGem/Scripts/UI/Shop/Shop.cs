﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public List<KeyValuePair<IngredientType, int>> shopItems = new List<KeyValuePair<IngredientType, int>>();

    [SerializeField]
    private GameObject shopItemPf;

    [SerializeField]
    private GameObject items;

	// Use this for initialization
	void Start() { 
        shopItems.Add(new KeyValuePair<IngredientType, int>(IngredientType.AdditionalMove, 250));

        shopItems.Add(new KeyValuePair<IngredientType, int>(IngredientType.SmallBlack, 500));
        shopItems.Add(new KeyValuePair<IngredientType, int>(IngredientType.BlueToPurple, 500));

        shopItems.Add(new KeyValuePair<IngredientType, int>(IngredientType.YellowDestroy, 1000));

        shopItems.ForEach(item => addShopItem(item.Key, item.Value));
	}

    private void addShopItem(IngredientType ingredientType, int cost) {
        Ingredient ingredient = LibraryManager.instance.get<IngredientLibrary>().get(ingredientType);
        GameObject shopItem = Instantiate(shopItemPf);
        shopItem.transform.SetParent(items.transform, false);
        shopItem.GetComponent<ShopItem>().setItem(ingredient, cost);
    }

}

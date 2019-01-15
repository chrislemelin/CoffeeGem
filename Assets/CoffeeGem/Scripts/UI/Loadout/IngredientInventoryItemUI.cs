using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngredientInventoryItemUI : MonoBehaviour {

    [SerializeField]
    private GameObject lockSprite;
    [SerializeField]
    private TextMeshProUGUI costText;

	public void setItem(IIngredientInventoryItem item) {
        if (item is IngredientInventoryItemShop) {
            IngredientInventoryItemShop shopItem = (IngredientInventoryItemShop)item;
            costText.text = "$"+(shopItem.cost / 100.0).ToString("N2");
        }
        else {
            lockSprite.SetActive(false);
            costText.gameObject.SetActive(false);

        }
        GetComponentInChildren<IngredientPreviewer>().renderIngredient(item.ingredient);
    }
}

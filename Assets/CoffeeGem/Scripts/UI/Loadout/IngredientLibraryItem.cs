using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngredientInventoryItem : MonoBehaviour {

    [SerializeField]
    private GameObject lockSprite;
    [SerializeField]
    private TextMeshProUGUI costText;

	public void setItem(IIngredientUIItem item) {
        if (item is IngredientUIItemShop) {
            IngredientUIItemShop shopItem = (IngredientUIItemShop)item;
            costText.text = "$"+(shopItem.cost / 100.0).ToString("N2");
        }
        else {
            lockSprite.SetActive(false);
            costText.gameObject.SetActive(false);

        }
        GetComponentInChildren<IngredientPreviewer>().renderPreview(item.ingredient);
    }
}

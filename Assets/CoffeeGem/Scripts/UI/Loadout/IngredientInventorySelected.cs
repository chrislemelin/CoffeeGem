using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngredientInventorySelected : MonoBehaviour {

    private IngredientSelectedDisplay display;
    [SerializeField]
    private ButtonCG buyButton;
    [SerializeField]
    private TextMeshProUGUI costText;
    private IngredientInventoryLibrary ingredientInventoryLibrary;
    private MoneyLibrary moneyLibrary;
    private bool started = false;

    private IIngredientInventoryItem selectedItem;

    public void Start() {
        if (!started){
            started = true;
            display = GetComponent<IngredientSelectedDisplay>();
            moneyLibrary = LibraryManager.instance.get<MoneyLibrary>();
            ingredientInventoryLibrary = LibraryManager.instance.get<IngredientInventoryLibrary>();
            buyButton.clicked += buyItem;
            moneyLibrary.moneyChanged += updateEnableBuyButton;
        }
    }

    private void OnDestroy() {
        moneyLibrary.moneyChanged -= updateEnableBuyButton;    
    }

    private void updateEnableBuyButton(int money) {
        if (selectedItem is IngredientInventoryItemShop) {
            IngredientInventoryItemShop shopItem = (IngredientInventoryItemShop)selectedItem;
            buyButton.setEnabled(money >= shopItem.cost);
        }
    }

    private void buyItem() {
        IngredientInventoryItemShop shopItem = (IngredientInventoryItemShop)selectedItem;
        moneyLibrary.removeMoney(shopItem.cost);
        ingredientInventoryLibrary.buyItem(shopItem.ingredientType);
        display.animation = false;
        setIngredientItem(new IngredientInventoryItemOwned(shopItem.ingredientType));
        display.animation = true;
    }


    public void setIngredientItem(IIngredientInventoryItem item) {
        Start();

        selectedItem = item;
        display.setIngredient(item.ingredient);
        if (item is IngredientInventoryItemShop) {
            IngredientInventoryItemShop shopItem = (IngredientInventoryItemShop)item;
            costText.text = "$" + (shopItem.cost / 100.0).ToString("0.00");
            updateEnableBuyButton(moneyLibrary.getMoney());
            buyButton.gameObject.SetActive(true);
        } else {
            costText.text = "";
            buyButton.gameObject.SetActive(false);
        }
    }


}

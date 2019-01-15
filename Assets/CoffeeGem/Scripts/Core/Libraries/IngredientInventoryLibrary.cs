using System;
using System.Collections;
using System.Collections.Generic;

public class IngredientInventoryLibrary : ILibrary {

    public event Action<List<IIngredientInventoryItem>> inventoryChanged;
    private List<IIngredientInventoryItem> inventoryItems = new List<IIngredientInventoryItem>();

    public void init() {
        inventoryItems.Add(new IngredientInventoryItemOwned(IngredientType.BasicBlue));
        inventoryItems.Add(new IngredientInventoryItemOwned(IngredientType.BasicGreen));
        inventoryItems.Add(new IngredientInventoryItemOwned(IngredientType.BasicPurple));
        inventoryItems.Add(new IngredientInventoryItemOwned(IngredientType.BasicRed));
        inventoryItems.Add(new IngredientInventoryItemOwned(IngredientType.BasicYellow));

        inventoryItems.Add(new IngredientInventoryItemShop(IngredientType.AdditionalMove, 500));
        inventoryItems.Add(new IngredientInventoryItemShop(IngredientType.SmallBlack, 1000));

        inventoryItems.Add(new IngredientInventoryItemShop(IngredientType.BlueToPurple, 1000));
        inventoryItems.Add(new IngredientInventoryItemShop(IngredientType.YellowDestroy, 1500));
    }

    public void triggerChange() {
        inventoryChanged?.Invoke(inventoryItems);
    }

    public void buyItem(IngredientType type) {
        int index = inventoryItems.FindIndex(i => i.ingredientType == type && i is IngredientInventoryItemShop);
        if ( index != -1) {
            inventoryItems.RemoveAt(index);
            inventoryItems.Insert(index, new IngredientInventoryItemOwned(type));
            triggerChange();
        }
    }
}

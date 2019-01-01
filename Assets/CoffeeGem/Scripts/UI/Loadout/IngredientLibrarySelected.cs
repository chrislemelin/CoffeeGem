using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientLibrarySelected : MonoBehaviour {

	public void setIngredientItem(IIngredientUIItem item) {
        GetComponentInChildren<IngredientSelectedDisplay>().setIngredient(item.ingredient);
    }
}

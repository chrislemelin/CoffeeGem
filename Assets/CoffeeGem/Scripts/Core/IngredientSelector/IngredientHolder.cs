using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientHolder : IngredientPreviewer {
    //[SerializeField]
    //private IngredientType defaultIngredient;
    //[SerializeField]
    //private bool hasDefaultIngredient = true;

    [SerializeField]
    public Ingredient ingredient { get; private set; }

    public GameObject background;

    private List<GameObject> boardEntityPreviews = new List<GameObject>();

    public void setIngredient(Ingredient ingredient) {
        this.ingredient = ingredient;
        renderPreview(ingredient);
    }

}

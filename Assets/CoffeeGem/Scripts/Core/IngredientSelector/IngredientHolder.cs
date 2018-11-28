using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientHolder : IngredientPreviewer {
    [SerializeField]
    private IngredientType defaultIngredient;


    [SerializeField]
    public Ingredient ingredient { get; private set; }


    public GameObject background;

    private List<GameObject> boardEntityPreviews = new List<GameObject>();

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setIngredient(Ingredient ingredient) {
        this.ingredient = ingredient;
        renderPreview(ingredient);
    }

}

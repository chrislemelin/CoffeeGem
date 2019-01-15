using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSelectedDisplay : IngredientPreviewer {

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private GameObject titlePf;

    [SerializeField]
    private GameObject descriptionPf;

    [SerializeField]
    private GameObject flavorTextPf;

    private IngredientLibrary ingredientLibrary;

    List<GameObject> texts = new List<GameObject>();

    public void Start() {
        ingredientLibrary = LibraryManager.instance.get<IngredientLibrary>();
        setIngredient(ingredientLibrary.get(IngredientType.BasicBlue));
    }

    public void setIngredient(Ingredient ingredient) {
        renderIngredient(ingredient);
        setText(ingredient);
    }

    public void setText(Ingredient ingredient) {
        texts.ForEach(i => Destroy(i));
        texts.Clear();

        setTextHelper(descriptionPf, ingredient.description);

        setTextHelper(titlePf, ingredient.title, true);

        setTextHelper(flavorTextPf, ingredient.flavorText);
        target.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();

    }

    private void setTextHelper(GameObject gameObjectPf, string text, bool scale = false) {
        // GameObject gameObject = Instantiate(gameObjectPf);
        if (ui) {
            gameObjectPf.GetComponent<TextMeshProUGUI>().SetText(text);
        } else {
            gameObjectPf.GetComponent<TextMeshPro>().SetText(text);
        }

        //gameObject.transform.SetParent(target.transform, false);
        //texts.Add(gameObject);

    }

}

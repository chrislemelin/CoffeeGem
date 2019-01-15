using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IngredientSelector : MonoBehaviour {

    [SerializeField]
    private int size = 4;

    [SerializeField]
    private GameObject ingredientSelector;

    private List<IngredientHolder> selectors = new List<IngredientHolder>();

    [SerializeField]
    IngredientSelectedDisplay selectedIngredientDisplay;

    [SerializeField]
    private List<IngredientType> ingredientOverridePool = new List<IngredientType>();

    [SerializeField]
    private List<IngredientType> ingredientsPool = new List<IngredientType>();

    [SerializeField]
    private Board board;
    
    private Dictionary<Ingredient, IngredientHolder> selectedTypeToHolder = new Dictionary<Ingredient, IngredientHolder>();
    private Ingredient selectedIngredient;
    private List<IngredientType> ingredientsNotInUse = new List<IngredientType>();
    private MenuLibrary menuLibrary;
    private IngredientLibrary ingredientLibrary;

    public void Start() {
        menuLibrary = LibraryManager.instance.get<MenuLibrary>();
        ingredientLibrary = LibraryManager.instance.get<IngredientLibrary>();
        if (ingredientOverridePool.Count > 0) {
            ingredientsPool = ingredientOverridePool;
        } else {
            ingredientsPool = menuLibrary.getIngredients().Select((ingred) => ingred.type).ToList();
        }

        foreach (IngredientType type in ingredientsPool) {
            ingredientsNotInUse.Add(type);
        }

        for (int a = 0; a < size; a++) {
            GameObject selector = Instantiate(ingredientSelector);
            selector.transform.SetParent(transform, false);
            setIngredientType(selector.GetComponent<IngredientHolder>(), getRandomIngredient());
            selectors.Add(selector.GetComponent<IngredientHolder>());
        }

        foreach (IngredientHolder ingredientHolder in selectors) {
            ingredientHolder.gameObject.GetComponent<OnEvent>().click += () => ingredientSelected(ingredientHolder);
        }
        ingredientSelected(selectors[0]);

    }

    private void setIngredientType(IngredientHolder holder, IngredientType type) {
        Ingredient ingredient = Ingredient.copy(ingredientLibrary.get(type));
        selectedTypeToHolder.Add(ingredient, holder);
        holder.setIngredient(ingredient);
        board.setIngredient(ingredient);
    }

    private IngredientType getRandomIngredient() {
        int index = Random.Range(0, ingredientsNotInUse.Count);
        IngredientType selectedType = ingredientsNotInUse[index];
        ingredientsNotInUse.Remove(selectedType);
        return selectedType;
    }

    public void ingredientUsed(Ingredient ingredient, IngredientType? ingredientType = null) {
        IngredientType newIngredient = ingredientType.HasValue ? (IngredientType)ingredientType : getRandomIngredient();
        IngredientHolder ingredientHolder = selectedTypeToHolder[ingredient];
        setIngredientType(selectedTypeToHolder[ingredient], newIngredient);
        selectedTypeToHolder.Remove(ingredient);
        ingredientsNotInUse.Add(ingredient.type);
        ingredientSelected(ingredientHolder);
    }

    private void ingredientSelected(IngredientHolder ingredientHolder) {
        board.setIngredient(ingredientHolder.ingredient);
        if (selectedIngredient != ingredientHolder.ingredient){
            selectedIngredientDisplay.setIngredient(ingredientHolder.ingredient);
        }
        selectedIngredient = ingredientHolder.ingredient;
        foreach (IngredientHolder holder in selectors) {
            if (holder == ingredientHolder) {
                holder.GetComponent<ColorOnHover>().setSelected(true);
            } else {
                holder.GetComponent<ColorOnHover>().setSelected(false);
            }
        }
    }

    public void addIngredient(IngredientType ingredientType, bool addToFirst = true) {
        ingredientsPool.Add(ingredientType);
        ingredientsNotInUse.Add(ingredientType);
        if (addToFirst) {
            ingredientUsed(selectors[0].ingredient, ingredientType);
        }
    }
}

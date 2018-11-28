using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour {

    [SerializeField]
    private IngredientPreviewer previewer;
    [SerializeField]
    private TextMeshPro textmeshPro;
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private AudioClip audioClip;
    public ButtonCG buyButton;

    private bool disabled = false;
    private Ingredient ingredient;
    private int cost;
    private IngredientSelector ingredientSelector;
    private MoneyScore moneyScore;

    public void Start() {
        ingredientSelector = FindObjectOfType<IngredientSelector>();
        moneyScore = FindObjectOfType<MoneyScore>();
        moneyScore.moneyChangedEvent += checkMoneyBalance;
        moneyScore.updateMoneyBalance();
    }

    public void checkMoneyBalance(int money) {
        buyButton.setEnabled(money > cost);
    }

    public void setItem(Ingredient ingredient, int cost) {
        this.ingredient = ingredient;
        this.cost = cost;
        previewer.renderPreview(ingredient);
        textmeshPro.SetText("$" + (cost / 100.0f).ToString("0.00"));
        button.GetComponent<OnClick>().click += () => addIngredient(ingredient);
    }

    private void addIngredient(Ingredient ingredient) {
        if (moneyScore.getScore() > cost && !disabled) {
            moneyScore.removeMoney(cost);
            ingredientSelector.addIngredient(ingredient.type);
            disabled = true;
            Destroy(gameObject);
            FindObjectOfType<SoundEffectPlayer>().PlaySoundEffect(audioClip);
        }
    }

    private void OnDestroy() {
        moneyScore.moneyChangedEvent -= checkMoneyBalance;

    }

}

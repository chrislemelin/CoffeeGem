using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientHolder : MonoBehaviour {
    [SerializeField]
    private IngredientType defaultIngredient;

    [SerializeField]
    private GameObject boardEntityPreview;

    [SerializeField]
    public Ingredient ingredient { get; private set; }

    [SerializeField]
    GameObject direction;

    [SerializeField]
    private GameObject boardEntityPreviewHolder;

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

    private void renderPreview(Ingredient ingredient) {
        foreach (KeyValuePair<Position, IBoardEntity> pair in ingredient.boardEntities) {
            GameObject preview = Instantiate(boardEntityPreview);
            copySpriteRender(pair.Value.GetComponentInChildren<SpriteRenderer>(), preview);
            preview.transform.SetParent(boardEntityPreviewHolder.transform);
            preview.transform.localPosition = pair.Key.toVec() * .42f;
        }
        switch (ingredient.displacementType) {
            case (IngredientDisplacement.Down):
                direction.transform.Rotate(0, 0, 0);
                break;

            case (IngredientDisplacement.Right):
                direction.transform.Rotate(0, 0, 90);
                break;

            case (IngredientDisplacement.Up):
                direction.transform.Rotate(0, 0, 180);
                break;

            case (IngredientDisplacement.Left):
                direction.transform.Rotate(0, 0, 270);
                break;

        }
    }

    private void copySpriteRender(SpriteRenderer renderer, GameObject gameObject) {
        gameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer copySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        copySpriteRenderer.sprite = renderer.sprite;
        copySpriteRenderer.color = renderer.color;
    }

    Component CopyComponent(Component original, GameObject destination) {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        // Copied fields can be restricted with BindingFlags
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields) {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy;
    }
}

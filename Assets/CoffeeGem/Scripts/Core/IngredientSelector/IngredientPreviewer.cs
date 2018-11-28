using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPreviewer : MonoBehaviour {

    [SerializeField]
    GameObject direction;

    [SerializeField]
    private GameObject boardEntityPreviewPf;

    [SerializeField]
    private GameObject boardEntityPreviewHolderPf;

    private GameObject boardEntityPreviewHolder;

    [SerializeField]
    private GameObject previewTarget;

    private void Awake() {
        if (previewTarget == null) {
            previewTarget = gameObject;
        }
    }

    protected List<SpriteRenderer> getPreviewSpriteRenderers(Ingredient ingredient) {
        List<SpriteRenderer> returnRenderers = new List<SpriteRenderer>();
        Position max = ingredient.getMaxPosition();
        Position min = ingredient.getMinPosition();

        for (int x = min.x; x <= max.x; x++) {
            for (int y = min.y; y <= max.y; y++) {
                Position position = new Position(x, y);
                if (ingredient.boardEntities.ContainsKey(position)) {
                    returnRenderers.Add(ingredient.boardEntities[position].GetComponentInChildren<SpriteRenderer>());
                } else {
                    returnRenderers.Add(null);
                }
            }
        }
        return returnRenderers;
    }

    protected void copySpriteRender(SpriteRenderer renderer, GameObject gameObject) {
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

    public void renderPreview(Ingredient ingredient) {
        if (boardEntityPreviewHolder != null) {
            boardEntityPreviewHolder.GetComponent<FadeOnDestroy>().Destroy(1);
            boardEntityPreviewHolder.GetComponent<ILerpable>().lerpToTimed(boardEntityPreviewHolder.transform.localPosition + new Vector3(0, -1), 1);
        }

        Position ingredientDimensions = ingredient.getDimensions();

        boardEntityPreviewHolder = Instantiate(boardEntityPreviewHolderPf);
        boardEntityPreviewHolder.transform.SetParent(previewTarget.transform, false);
        boardEntityPreviewHolder.GetComponent<GridLayoutGroup>().constraintCount = ingredientDimensions.x;
        boardEntityPreviewHolder.transform.localPosition = new Vector3(0, 1);
        

        foreach (SpriteRenderer render in getPreviewSpriteRenderers(ingredient)) {
            GameObject preview = Instantiate(boardEntityPreviewPf);
            if (render != null) {
                copySpriteRender(render, preview);
            }
            preview.transform.SetParent(boardEntityPreviewHolder.transform, false);

            preview.gameObject.GetComponent<Fade>().checkShow();
        }
        boardEntityPreviewHolder.GetComponent<ILerpable>().lerpToTimed(new Vector2(0, 0), 1);

        boardEntityPreviewHolder.GetComponent<Fade>().setShow(false, true);
        boardEntityPreviewHolder.gameObject.GetComponent<Fade>().checkShow();
        boardEntityPreviewHolder.GetComponent<Fade>().setShow(true);

        if (direction != null) {
            switch (ingredient.displacementType) {
                case (IngredientDisplacement.Down):
                    direction.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;

                case (IngredientDisplacement.Right):
                    direction.transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;

                case (IngredientDisplacement.Up):
                    direction.transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;

                case (IngredientDisplacement.Left):
                    direction.transform.rotation = Quaternion.Euler(0, 0, 270);
                    break;
            }
        }
    }
}

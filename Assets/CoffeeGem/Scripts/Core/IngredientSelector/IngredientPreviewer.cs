using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPreviewer : MonoBehaviour {

    [SerializeField]
    private IngredientType defaultIngredient;
    [SerializeField]
    private bool hasDefaultIngredient = true;

    [SerializeField]
    GameObject direction;

    [SerializeField]
    protected bool ui = false;

    [SerializeField]
    private TextMeshProUGUI textUI;

    [SerializeField]
    private GameObject boardEntityPreviewPf;

    [SerializeField]
    private GameObject boardEntityPreviewHolderPf;

    private GameObject boardEntityPreviewHolder;

    public bool animation = true;

    public Ingredient renderedIngredient { get; protected set; }

    [SerializeField]
    private GameObject previewTarget;

    private void Awake() {
        if (previewTarget == null) {
            previewTarget = gameObject;
        }
        //renderPreview(null);
    }

    protected List<SpriteRenderer> getPreviewSpriteRenderers(Ingredient ingredient) {
        renderedIngredient = ingredient;
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

    protected void copySpriteRenderToUI(SpriteRenderer renderer, GameObject gameObject) {
        gameObject.AddComponent<Image>();
        Image copyImage = gameObject.GetComponent<Image>();
        copyImage.sprite = renderer.sprite;
        copyImage.color = renderer.color;
    }

    public void renderIngredient(Ingredient ingredient) {
        if (boardEntityPreviewHolder != null) {
            if (animation) {
                boardEntityPreviewHolder.GetComponent<FadeOnDestroy>().Destroy(1);
                boardEntityPreviewHolder.GetComponent<ILerpable>().lerpToTimed(boardEntityPreviewHolder.transform.localPosition + new Vector3(0, ui ? -50 : -1), 1);
            } else {
                boardEntityPreviewHolder.GetComponent<FadeOnDestroy>().Destroy(0);
            }

        }

        if (ingredient == null) {
            direction.SetActive(false);
            if (ui) {
                textUI.text = "none";
            }
            return;
        } else {
            direction.SetActive(true);
            if (ui) {
                textUI.text = "";
            }
        }

        Position ingredientDimensions = ingredient.getDimensions();

        boardEntityPreviewHolder = Instantiate(boardEntityPreviewHolderPf);
        boardEntityPreviewHolder.transform.SetParent(previewTarget.transform, false);
        boardEntityPreviewHolder.GetComponent<GridLayoutGroup>().constraintCount = ingredientDimensions.x;
    
        

        foreach (SpriteRenderer render in getPreviewSpriteRenderers(ingredient)) {
            GameObject preview = Instantiate(boardEntityPreviewPf);
            if (render != null) {
                if (ui) {
                    copySpriteRenderToUI(render, preview);
                } else {
                    copySpriteRender(render, preview);
                }
            }
            preview.transform.SetParent(boardEntityPreviewHolder.transform, false);
        }
        if (animation) {
            boardEntityPreviewHolder.transform.localPosition = new Vector3(0, ui ? 50 : 1);
            boardEntityPreviewHolder.GetComponent<ILerpable>().lerpToTimed(new Vector2(0, 0), 1);

            boardEntityPreviewHolder.GetComponent<Fade>().setShow(false, true);
            boardEntityPreviewHolder.gameObject.GetComponent<Fade>().init();
            boardEntityPreviewHolder.GetComponent<Fade>().setShow(true);
        } else {
            boardEntityPreviewHolder.transform.localPosition = new Vector3(0, 0);
        }

        Fade fade = gameObject.transform.parent.GetComponent<Fade>();
        if(fade != null) {
            fade.recheckDict();
        }

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientLibraryItemDragSpawner : MonoBehaviour, IDragHandler, IEndDragHandler {

    private Action<Ingredient> callback;
    private int siblingIndex;

    private Transform parent;
    private GameObject phantomItem;

    private Vector3 startLocalPosition = new Vector3(33,-40);
    private Vector3 localPosition;
    public GameObject target;
    private bool firstFrame = true;
    private Ingredient ingredient;

    public void setDropCallback(Action<Ingredient> callback) {
        this.callback = callback;
    }

    public void Start() {
        localPosition = startLocalPosition;
        parent = transform.parent;
        siblingIndex = transform.GetSiblingIndex();
        ingredient = GetComponentInChildren<IngredientPreviewer>().renderedIngredient;
    }

    public void Update() {
        if (localPosition == startLocalPosition) {
            localPosition = transform.localPosition;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (firstFrame) {
            firstFrame = false;
            GetComponentInChildren<ButtonCG>().setEnabled(false);
            phantomItem = Instantiate(gameObject);
            phantomItem.transform.SetParent(transform.parent);
            siblingIndex = transform.GetSiblingIndex();
            phantomItem.transform.SetSiblingIndex(siblingIndex);
            phantomItem.GetComponent<Fade>().setShow(false, true);

            transform.SetParent(target.transform, false);
        }
        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData) {
        Destroy(phantomItem);
        transform.SetParent(parent);
        transform.SetSiblingIndex(siblingIndex);
        transform.localPosition = localPosition;
        GetComponentInChildren<ButtonCG>().setEnabled(true);
        firstFrame = true;
        callback?.Invoke(ingredient);
    }
}

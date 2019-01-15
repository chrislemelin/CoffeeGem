using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BELibrary : MonoBehaviour, ILibrary {
    [SerializeField]
    private IngredientLibrary ingredientLibrary;

    //public static BELibrary Instance;

    [SerializeField]
    private List<GameObject> bEGameObjects;

    private Dictionary<GemType, GameObject> dictionary = new Dictionary<GemType, GameObject>();

    public IBoardEntity get(GemType type) {
        return dictionary[type].GetComponent<IBoardEntity>();
    }

    //void Awake() {
    //    if (Instance == null) {
    //        makeDictionaries();
    //        Instance = this;
    //        ingredientLibrary.makeDictionary();
    //    }
    //}

    private void makeDictionary() {
        foreach (GameObject be in bEGameObjects) {
            dictionary.Add(be.GetComponent<IBoardEntity>().getType(), be);
        }
    }

    public void init() {
        makeDictionary();
        ingredientLibrary.makeDictionary();
    }
}


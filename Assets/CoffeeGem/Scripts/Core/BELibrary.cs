using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BELibrary : MonoBehaviour {

    public static BELibrary Instance;

    [SerializeField]
    private List<GameObject> bEGameObjects;
    [SerializeField]
    private List<GemType> bETypes;

    private Dictionary<GemType, GameObject> dictionary = new Dictionary<GemType, GameObject>();

    public IBoardEntity get(GemType type) {
        return dictionary[type].GetComponent<IBoardEntity>();
    }

    void Awake() {
        if (Instance != null)
            Destroy(Instance);
        else {
            makeDictionaries();
            Instance = this;
        }
    }

    private void makeDictionaries() {
        foreach (GameObject be in bEGameObjects) {
            dictionary.Add(be.GetComponent<IBoardEntity>().getType(), be);
        }
    } 
}


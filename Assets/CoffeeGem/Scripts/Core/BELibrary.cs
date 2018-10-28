using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BELibrary : MonoBehaviour {

    public static BELibrary Instance;

    [SerializeField]
    private List<GameObject> bEGameObjects;
    [SerializeField]
    private List<BEType> bETypes;

    private Dictionary<BEType, GameObject> dictionary = new Dictionary<BEType, GameObject>();

    public IBoardEntity get(BEType type) {
        return dictionary[type].GetComponent<IBoardEntity>();
    }

    void Awake() {
        if (Instance != null)
            Destroy(Instance);
        else {
            makeDictionaries();
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    private void makeDictionaries() {
        foreach (GameObject be in bEGameObjects) {
            dictionary.Add(be.GetComponent<IBoardEntity>().getType(), be);
        }
    } 
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class LibraryManager : MonoBehaviour {

    public static bool created = false;

    private List<ILibrary> libraries;
    private List<ILibrary> savableLibraries;

    public static LibraryManager instance;

    public void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            makeLibraries();
        } else {
            Destroy(gameObject);
        }
    }

    public void makeLibraries() {
        libraries = GetComponentsInChildren<ILibrary>().ToList();
        savableLibraries = tryLoadLibraries(getSavableLibraries());
        libraries.AddRange(savableLibraries);
        foreach (ILibrary lib in libraries) {
            lib.init();
        }
    }

    private List<ILibrary> getSavableLibraries() {
        List<ILibrary> returnList = new List<ILibrary>();

        returnList.Add(new MenuLibrary());
        returnList.Add(new MoneyLibrary());
        returnList.Add(new IngredientInventoryLibrary());
        returnList.Add(new DayLibrary());

        return returnList;
    }

    private List<ILibrary> tryLoadLibraries(List<ILibrary> libs) {
        List<ILibrary> returnLibraries = new List<ILibrary>();

        foreach (ILibrary lib in libs) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try {
                FileStream fileStream = File.Open(getDataPath(lib), FileMode.Open);
                ILibrary loadedLibrary = (ILibrary)binaryFormatter.Deserialize(fileStream);
                if (loadedLibrary != null) {
                    returnLibraries.Add(loadedLibrary);
                } else {
                    returnLibraries.Add(lib);
                }
            } catch (FileNotFoundException e) {
                returnLibraries.Add(lib);
            }
        }
        return returnLibraries;
    }


    private void trySaveLibraries(List<ILibrary> libs) {
        foreach (ILibrary lib in libs) {
            string jsonString = JsonUtility.ToJson(lib);

            using (StreamWriter streamWriter = File.CreateText(getDataPath(lib))) {
                streamWriter.Write(jsonString);
            }          
        }
    }

    private string getDataPath(ILibrary library) {
        return Path.Combine(Application.persistentDataPath, library.GetType().FullName);
    }

    public T get<T> () where T : ILibrary {
        foreach (ILibrary library in libraries) {
            if (library is T) {
                return (T)library;
            }
        }
        return default(T);
    } 
}

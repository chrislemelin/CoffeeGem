using System;
using System.Collections;
using UnityEngine;

public class Core : MonoBehaviour {

    static public Core core; 

    void Awake() {
        if (core == null) {
            core = this;

        }
    }

    public void ExecuteAfterTime(float time, Action action) {
        StartCoroutine(ExecuteAfterTimeHelper(time, action));
    }
    public static IEnumerator ExecuteAfterTimeHelper(float time, Action action) {
        yield return new WaitForSeconds(time);
        action();
    }

    public static void CopySpriteRender(SpriteRenderer renderer, GameObject gameObject) {
        gameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer copySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        copySpriteRenderer.sprite = renderer.sprite;
        copySpriteRenderer.color = renderer.color;
    }

    public static Component CopyComponent(Component original, GameObject destination) {
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    [SerializeField]
    private float targetAlpha = 1;
    bool active = false;
    bool started = false;
    private float currentAlpha; 


    [SerializeField]
    private float duration = 1;
    [SerializeField]
    protected GameObject target;
    List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    List<TextMesh> textMeshes = new List<TextMesh>();
    List<TextMeshPro> textMeshPros = new List<TextMeshPro>();
    List<TextMeshProUGUI> textMeshProUIs = new List<TextMeshProUGUI>();
    List<Image> images = new List<Image>();
    [SerializeField]
    Dictionary<GameObject, float> rendererToStartAplha = new Dictionary<GameObject, float>();

    public void Start() {
        init();
    }

    public void init(float? defaultAlpha = null) {
        if (!started) {
            started = true;

            if (target == null) {
                target = gameObject;
            }
            generateDict();

            currentAlpha = defaultAlpha.HasValue ? defaultAlpha.Value : targetAlpha;
            setTransparent(currentAlpha, true);
        }
    }

    public void recheckDict() {
        generateDict();
    }

    private void generateDict() {
        List<Fade> fades = target.GetComponentsInChildren<Fade>().OfType<Fade>().ToList();
        Dictionary<GameObject, float> childDict = new Dictionary<GameObject, float>();
        foreach (Fade fade in fades) {
            foreach (KeyValuePair<GameObject, float> entry in fade.rendererToStartAplha) {
                childDict[entry.Key] = entry.Value;
            }
        }

        spriteRenderers = target.GetComponentsInChildren<SpriteRenderer>().OfType<SpriteRenderer>().ToList();
        foreach (SpriteRenderer renderer in spriteRenderers) {
            rendererToStartAplha[renderer.gameObject] = renderer.color.a;
        }

        textMeshes = target.GetComponentsInChildren<TextMesh>().OfType<TextMesh>().ToList();
        foreach (TextMesh textMesh in textMeshes) {
            rendererToStartAplha[textMesh.gameObject] = textMesh.color.a;
        }

        textMeshPros = target.GetComponentsInChildren<TextMeshPro>().OfType<TextMeshPro>().ToList();
        foreach (TextMeshPro textMeshPro in textMeshPros) {
            rendererToStartAplha[textMeshPro.gameObject] = textMeshPro.color.a;
        }

        images = target.GetComponentsInChildren<Image>().OfType<Image>().ToList();
        foreach (Image image in images) {
            float alpha = image.color.a;
            if (childDict.ContainsKey(image.gameObject)) {
                alpha = childDict[image.gameObject];
            }
            rendererToStartAplha[image.gameObject] = alpha;         
        }

        textMeshProUIs = target.GetComponentsInChildren<TextMeshProUGUI>().OfType<TextMeshProUGUI>().ToList();
        foreach (TextMeshProUGUI textMesh in textMeshProUIs) {
            rendererToStartAplha[textMesh.gameObject] = textMesh.color.a;
        }

    }

    public void setTransparent(float value, bool instant = false) {
        targetAlpha = value;
        if (instant) {
            setTransparcyHelper(value);
        } else {
            active = true;
        }
    }

    private void setTransparcyHelper(float value) {
        foreach (SpriteRenderer render in spriteRenderers) {
            Color newColor = render.color;
            newColor.a = value * rendererToStartAplha[render.gameObject];
            render.color = newColor;
        }
        foreach (TextMesh textMesh in textMeshes) {
            Color newColor = textMesh.color;
            newColor.a = value;
            textMesh.color = newColor;
        }

        foreach (TextMeshPro textMeshPro in textMeshPros) {
            Color newColor = textMeshPro.color;
            newColor.a = value;
            textMeshPro.color = newColor;
        }

        foreach (TextMeshProUGUI textMeshPro in textMeshProUIs) {
            Color newColor = textMeshPro.color;
            newColor.a = value;
            textMeshPro.color = newColor;
        }

        foreach (Image image in images) {
            Color newColor = image.color;
            newColor.a = value * rendererToStartAplha[image.gameObject];
            image.color = newColor;
        }
    }

    public void setShow(bool show, bool instant = false) {
        float value = show ? 1.0f : 0.0f;
        setTransparent(value, instant);
    }

    public void setDuration(float duration) {
        this.duration = duration;
    }

    public void Update() {
        if (active) { 
            float alphaDelta = Time.deltaTime / duration;
            float distanceFromTarget = targetAlpha - currentAlpha;
            float ratio = Mathf.Abs(alphaDelta / distanceFromTarget);
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, ratio);
            currentAlpha = newAlpha;

            setTransparcyHelper(newAlpha);
            if (ratio > 1) {
                active = false;
            }           
        }
    }
}

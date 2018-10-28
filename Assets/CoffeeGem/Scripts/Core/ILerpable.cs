using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILerpable : MonoBehaviour {

    [SerializeField]
    private GameObject ghostSprite;


    private bool isLerping = false;
    private Vector3 lerpingToVector;
    private Vector3 lerpingFromVector;
    private float startLerpTime = 0;
    private float lerpDuration = 0;

    private bool isGhostLerping = false;
    private Vector3 ghostLerpingToVector;
    private Vector3 ghostLerpingFromVector;
    private float ghostStartLerpTime = 0;
    private float ghostLerpDuration = 0;

    public float lerpTo(Vector3 vector3, float speed) {
        lerpingToVector = vector3 - ghostLerpingToVector;
        lerpingFromVector = gameObject.transform.localPosition;
        lerpDuration = Vector3.Distance(vector3, gameObject.transform.localPosition) / speed;
        startLerpTime = Time.time;
        isLerping = true;
        return lerpDuration;
    }

    public float lerpToTimed(Vector3 vector3, float time) {
        lerpingToVector = vector3;
        lerpingFromVector = gameObject.transform.localPosition;
        lerpDuration = time;
        startLerpTime = Time.time;
        isLerping = true;
        return lerpDuration;
    }

    public float displaceLerpTo(Vector3 offset, float speed) {
        ghostLerpingToVector = offset - ghostLerpingToVector;
        ghostLerpingFromVector = ghostSprite.transform.localPosition;
        ghostLerpDuration = Vector3.Distance(offset, ghostSprite.transform.localPosition) / speed;
        ghostStartLerpTime = Time.time;
        isGhostLerping = true;
        return lerpDuration;
    }

    public float displaceLerpToTimed(Vector3 offset, float time) {
        ghostLerpingToVector = offset;
        ghostLerpingFromVector = ghostSprite.transform.localPosition;
        ghostLerpDuration = time;
        ghostStartLerpTime = Time.time;
        isGhostLerping = true;
        return lerpDuration;
    }

    public void Update() {
 

        if (isGhostLerping && isLerping) {
            isGhostLerping = false;
            ghostSprite.transform.localPosition = new Vector3(0,0,0);
        }

        if (isGhostLerping) {
            float ratio = (Time.time - ghostStartLerpTime) / ghostLerpDuration;
            if (ghostLerpDuration <= 0) {
                ratio = 1;
            }
            ghostSprite.transform.localPosition = Vector3.Lerp(ghostLerpingFromVector, ghostLerpingToVector, ratio);
            if (ratio >= 1) {
                isGhostLerping = false;
            }
        }

        if (isLerping) {
            float ratio;
            if (lerpDuration <= 0) {
                ratio = 1;
            } else {
                ratio = (Time.time - startLerpTime) / lerpDuration;
            }
     
            transform.localPosition = Vector3.Lerp(lerpingFromVector, lerpingToVector, ratio);
            if (ratio >= 1) {
                isLerping = false;
            }
        }
    }
}

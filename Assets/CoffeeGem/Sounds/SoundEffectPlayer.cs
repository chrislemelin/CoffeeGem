using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour {

    public void PlaySoundEffect(AudioClip audioClip, float volume = 1) {
        GetComponent<AudioSource>().PlayOneShot(audioClip, volume);
    }
}

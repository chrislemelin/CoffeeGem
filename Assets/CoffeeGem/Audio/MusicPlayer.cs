using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    [SerializeField]
    AudioClip audioClip;

	// Use this for initialization
	void Start () {
        //DontDestroyOnLoad(gameObject);
        //GetComponent<AudioSource>().PlayOneShot(audioClip);
       
    }
	

}

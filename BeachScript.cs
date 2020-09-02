using UnityEngine;
using System.Collections;

public class BeachScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioSource amb = GetComponent<AudioSource>();
        amb.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

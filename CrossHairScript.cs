using UnityEngine;
using System.Collections;

public class CrossHairScript : MonoBehaviour {

    public bool active;

    // Use this for initialization
    void Start () {

        GetComponent<Canvas>().enabled = false;
        active = false;

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("rightmouse"))
        {
            if (active)
            {
                GetComponent<Canvas>().enabled = false;
                active = false;
            }
            else
            {
                GetComponent<Canvas>().enabled = true;
                active = true;
            }
        }
	}
}

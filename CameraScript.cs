using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    internal static Space main;
    public bool aim;
    // Use this for initialization
    void Start () {
        aim = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("rightmouse"))
        {
            if (aim == false)
            {
                transform.Translate(0, 0, 140 * .02f);
                
                aim = true;
            }
            else
            {
                transform.Translate(0, 0, -140 * .02f);
                
                aim = false;
            }
        }

    }
}

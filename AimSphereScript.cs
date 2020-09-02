using UnityEngine;
using System.Collections;

public class AimSphereScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * 150, 0));


    }
}

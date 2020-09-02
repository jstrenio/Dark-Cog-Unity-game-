using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour {
    public bool aim;
	// Use this for initialization
	void Start () {
        aim = false;
       
	}
	
	// Update is called once per frame
	void Update () {
      

        transform.position = GameObject.Find("ninja").transform.position;

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * 100, 0));
       // transform.Rotate(new Vector3(0, Input.GetAxis("leftanalogx") * Time.deltaTime * 100, 0));

        if (Input.GetButtonDown("rightmouse"))
        {
            if (aim == false)
            {
                aim = true;
                transform.Rotate(Vector3.right * -13);
            }
            else
            {
                aim = false;
                transform.Rotate(Vector3.right * 13);
            }
        }

        if (aim == true)
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * Time.deltaTime * -150, 0, 0));
            float z = transform.eulerAngles.z;
            transform.Rotate(0, 0, -z);
        }
       else
        {
            float x = transform.eulerAngles.x;
            transform.Rotate(-x, 0, 0);
        }
        

    }
}

           
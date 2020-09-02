using UnityEngine;
using System.Collections;

public class testscript : MonoBehaviour {

  /*  int factor;
	// Use this for initialization
	void Start () {
        factor = 1;
      //  StartCoroutine("RateOfFire");
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("leftanalogx") == 0)
        {
            Debug.Log("nothing");
        }
        else if (Input.GetAxis("leftanalogx") > 0)
        {
            if(Input.GetAxis("leftanalogx") < 1)
            {
                factor = -1;
            }
            else
            {
                factor = -4;
            }
            transform.Translate(0, 0, factor * Time.deltaTime);
        }
        else
        {
            if (Input.GetAxis("leftanalogx") <= -1)
            {
                factor = 4;
            }
            else
            {
                factor = 1;
            }
            transform.Translate(0, 0, factor * Time.deltaTime);
            Debug.Log(Input.GetAxis("leftanalogx"));
        }


        /*  IEnumerator RateOfFire()
          {
              while (true)
              {
                  if (Input.GetKey(KeyCode.J))
                  {
                      Debug.Log("bxo bla");
                      yield return new WaitForSeconds(.1f);
                  }
                  else
                  {
                      yield return null;
                  }
              }
          }
        //Debug.Log(Input.GetJoystickNames());
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick button " + i))
            {
                Debug.Log("Button " + i + " was pressed!");
            }
        }
    } */
}

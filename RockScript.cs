using UnityEngine;
using System.Collections;


public class RockScript : MonoBehaviour {

  
   // public Collider col;
    private bool inAir = false;
    public float start;

    // Use this for initialization
    void Start () {
        //  col = GetComponent<Collider>();
        
	}

    void OnCollisionEnter(Collision col)
    {
        inAir = false;
      
    }


    // Update is called once per frame
    void Update () {
	
        if (Input.GetButtonDown("rocktoss"))
        {
            inAir = true;
            
            start = Time.time;
        }
        
        if (inAir == true)
        {
            if (.55f < Time.time - start && Time.time - start < .6f)
            {
                transform.position = GameObject.Find("ninja").transform.position;
                transform.Translate(.25f, 2f, 1.5f);
                transform.forward = GameObject.Find("ninja").transform.forward;
            }
            else if (.6f < Time.time - start)
            {
                transform.Translate(0, 0, 30 * Time.deltaTime);
                if (Time.time - start < .9f)
                {
                    transform.Translate(0, 1.5f * Time.deltaTime, 0);
                }
                else if (Time.time - start < 1.3f)
                {
                    transform.Translate(0, -4 * Time.deltaTime, 0);
                }
                else
                {
                    transform.Translate(0, -6 * Time.deltaTime, 0);
                }
            }
        }
       

	}
}

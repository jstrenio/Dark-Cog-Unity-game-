using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {
    public Animator anim;
    Vector3 strt = new Vector3();
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
       
        strt = transform.position;
       // InvokeRepeating("spawn", 1, 5);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, 2 * Time.deltaTime);
        if(transform.position.z > 200)
        {
            transform.position = strt;
        }

        if (Input.GetKey(KeyCode.Y))
        {
            spawn();
        }

        
    }

    void spawn()
    {
        int neg;
        if (Random.value < .5)
        {
            neg = -1;
        }
        else
        {
            neg = 1;
        }
        float randx = Random.value * neg * 50;
        float randz = Random.value * neg * 50;
        Vector3 sp = new Vector3(randx, 5, randz);



        GameObject badguy = (GameObject)Instantiate(Resources.Load("badguy Prefab"), sp, transform.rotation);
        GameObject badguy1 = (GameObject)Instantiate(Resources.Load("badguy Prefab"), sp+(Vector3.forward*5), transform.rotation);
    }
}

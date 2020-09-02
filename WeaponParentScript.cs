using UnityEngine;
using System.Collections;

public class WeaponParentScript : MonoBehaviour {
    public Animator anim;
    public int carry;
    public bool touching;
    public bool aim;
    public bool canshoot;
    public Collider col;
    public Rigidbody rb;
    public AudioClip akfire, pickup, aimfx, shot, pull;
    public AudioSource source;
    public RaycastHit hit;
    public GameObject Camera;
    public GameObject ninja;
    GameObject arrowInstance;

    // Use this for initialization
    void Start() {
        aim = false;
        canshoot = true;
        carry = 0;
        touching = false;
        anim = GameObject.Find("ninja").GetComponent<Animator>();
        col = gameObject.GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        Camera = GameObject.Find("MainCamera");
        ninja = GameObject.Find("ninja");
        StartCoroutine("RateOfFire");
    }

 

// Update is called once per frame
void Update() {

        //--------------------GET AXIS FOR BOW PULL BACK----------------------------------------------------------------------
        float h = Input.GetAxis("bowpull");

        //----------------------------------SHOOT ARROW------------------------------------------------------------------------
        if (Input.GetButtonUp("leftmouse") && ninja.GetComponent<Animator>().GetBool("aim") && carry == 1)
        {
            arrowfire(h);
        }

        //--------------------Pull BACK AUDIO------------------------------------------------------------------
        if (Input.GetButtonDown("leftmouse") && ninja.GetComponent<Animator>().GetBool("aim") && carry == 1)
        {
            source.clip = pull;
            source.Play();
        }

        //Debug.DrawRay(origin, direction, Color.blue, 1f, true);

        //if holding ak, aiming, left mouse clicked and current animation is aim play fire sound

        if (carry == 2 && aim && Input.GetButton("leftmouse") && this.anim.GetCurrentAnimatorStateInfo(0).IsName("aim") && transform.parent != null && !anim.GetBool("rateoffire"))
        {
           //if (!source.isPlaying)
            {
              //  source.Stop();
                source.clip = akfire;
                source.Play();
            }
            //fire gun
            Debug.Log("set true");
            anim.SetBool("rateoffire", true);
           // gunshot();
        }

        // if he's crouching and touching the weapon and not carrying it already
            if (touching && anim.GetBool("crouch") && carry == 0)
        {
            Debug.Log("ickup");
            if (!source.isPlaying)
            {
                source.clip = pickup;
                source.Play();
            }
            col.enabled = false;
            Holster();
            if (transform.FindChild("ak47a"))
            {
                carry = 2;
            }

            else if (transform.FindChild("bow"))
            {
                carry = 1;
            }
            touching = false;
         
            anim.SetBool("armed", true);
        }

        //if he's aiming and right clicks holster the weapon
        else if (carry != 0 && aim && Input.GetButtonDown("rightmouse") && this.anim.GetCurrentAnimatorStateInfo(0).IsName("aim"))
        {
            Holster();
        }

        //if he's holstered grab and aim the weapon
        else if (carry != 0 && !aim && Input.GetButtonDown("rightmouse") && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("aim"))
        {
            GrabWeapon();

        }

        //if the user presses "g" drop the weapon
        if (Input.GetButtonDown("dropweapon"))
        {
            DropWeapon();
        }
    }

    //grab weapon switch parent from back to hand
    void GrabWeapon()
    {
        aim = true;
        var hand = GameObject.Find("handL");
        transform.parent = hand.transform;
        transform.localPosition = new Vector3(.001f, -.002f, -.001f);
        Vector3 tmp = Vector3.zero;
        tmp.x = hand.transform.eulerAngles.x + 0;
        tmp.y = hand.transform.eulerAngles.y;
        tmp.z = hand.transform.eulerAngles.z + 150;
        transform.eulerAngles = tmp;
        //Debug.Log("grab weapon");
        if(!source.isPlaying)
        {
            source.clip = aimfx;
            source.Play();
        }

      
    }

    //---------PARENT TO SPINE-----------------------------------------
    void Holster()
    {
        Destroy(GetComponent<Rigidbody>());
        aim = false;
        var spine2 = GameObject.Find("spine2");
        transform.parent = spine2.transform;
        if (transform.FindChild("bow"))
        {
            transform.localPosition = new Vector3(-.004f, 0, -.007f);
        }
        else if (transform.FindChild("ak47a"))
        {
            transform.localPosition = new Vector3(.01f, 0, -.012f);
        }
        Vector3 tmp = Vector3.zero;
        if (transform.FindChild("bow"))
        {
            tmp.x = spine2.transform.eulerAngles.x + 90;
            tmp.y = spine2.transform.eulerAngles.y + 100;
        }
        else if(transform.FindChild("ak47a"))
        {
            tmp.x = spine2.transform.eulerAngles.x + 10;
            tmp.y = spine2.transform.eulerAngles.y + 70;
        }

        tmp.z = spine2.transform.eulerAngles.z + 5;
        transform.eulerAngles = tmp;
      

        //Debug.Log("holster");
       
    }

    //-----------Collisions----------------------------------

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "ninja")
        {
            touching = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "ninja")
        {
            touching = false;
        }
    }

    //----------------------DROP WEAPON FUNCTION----------------------------------------------

    void DropWeapon()
    {
        if (transform.parent.parent.name == "ninja")
        {
            transform.position = ninja.transform.position - ninja.transform.forward + ninja.transform.up * .5f;
            transform.parent = null;
            Rigidbody rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            carry = 0;
            col.enabled = true;
            anim.SetBool("armed", false);

            transform.Rotate(Vector3.right * 90);
            rb.AddForce(ninja.transform.forward * -5);
        }
    }

    //-------------------------RAY CAST GUNSHOT BULLET TRAJECTORY AND TIMING-------------------------------------
    void gunshot()
    {
        Vector3 origin = Camera.transform.position;
        Vector3 direction = Camera.transform.forward * 10;
        Ray ray = new Ray(origin, direction);
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "NPC" && canshoot && hit.collider is SphereCollider)
        {
          //  Debug.DrawRay(origin, direction, Color.yellow, .01f);
            hit.transform.gameObject.GetComponent<badguyScript>().health -= 100;
            
        }
        else if (Physics.Raycast(ray, out hit) && hit.transform.tag == "NPC" && canshoot && hit.collider is BoxCollider)
        {
         //   Debug.DrawRay(origin, direction, Color.yellow, .01f);
            hit.transform.gameObject.GetComponent<badguyScript>().health -= 25;
        }

        GameObject.Find("Sphere").transform.eulerAngles -= new Vector3(1f, 0, 0);
        int chance = Random.Range(-1, 2);
        GameObject.Find("Sphere").transform.eulerAngles -= new Vector3(0, .5f*chance, 0);
    }

    //---------------------------------ARROW FIRE------------------------------------------------------------------
    void arrowfire(float h)
    {
        var cam = Camera;
        arrowInstance = Instantiate(Resources.Load("Arrow Prefab")) as GameObject;
        arrowInstance.transform.parent = cam.transform;
        arrowInstance.transform.localPosition = new Vector3(0, 0, 3);
        arrowInstance.transform.localEulerAngles = new Vector3(87, 0, 0);
        arrowInstance.GetComponent<Rigidbody>().AddForce(arrowInstance.transform.up*60*h, ForceMode.VelocityChange);
        arrowInstance.transform.parent = null;
        source.clip = shot;
        source.Play();
    }

    void canshootfunc()
    {
        canshoot = true;
    }

    IEnumerator RateOfFire()
    {
        while (true)
        {
            if (anim.GetBool("rateoffire") && transform.parent != null && Input.GetButton("leftmouse"))
            {
                anim.SetBool("rateoffire", false);
                Debug.Log("set false");
                gunshot();
                yield return new WaitForSeconds(.13f);
            }
            else
            {
                yield return null;
            }
        }
    }
}

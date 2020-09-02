
using UnityEngine;
using System.Collections;


public class AnimatorController : MonoBehaviour {

    public Animator anim;
    public CapsuleCollider cap;
    public Rigidbody rb;
    public RaycastHit hit;
    public AudioSource source;
    public AudioClip knife;
    float distToGround;
    public bool ladder;
    public bool spacebar;
    public bool aim;
    public bool collide = false;
    public bool crouch = false;

    void Start () {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        distToGround = cap.bounds.extents.y;
        anim.SetBool("hang", false);
        ladder = false;
        spacebar = false;
        aim = false;
    }
    //-------------WALL PLANT SEQUENCE-----------------------------------------------
    void stallEnd()
    {
        anim.SetBool("wallplant", false);

    }

    //--------------CLIMB LEDGE SEQUENCE-----------------------------------------------

    void upover1()
    {
        rb.AddForce(new Vector3(0f, 17000, 0f), ForceMode.Impulse);
    }

    void upover2()
    {
        rb.AddForce(transform.forward *130, ForceMode.Impulse);
    }

    void upover3()
    {
        anim.SetBool("hang", false);
    }


    //--------------ON THE GROUND----------------------------------------------

    bool IsGrounded(float distToGround)
    {
        distToGround = cap.bounds.extents.y;
        var origin = new Vector3();
        origin = transform.position;
        origin.y += 1.5f;

       // Debug.DrawRay(origin, -Vector3.up* (distToGround +1f), Color.blue, .1f, true);
        return Physics.Raycast(origin, -Vector3.up, distToGround +1f);
    }

    //--------------TOUCHING LADDER---------------COLLISIONS---------------------------------------------

    void OnCollisionEnter(Collision col)
    {
        collide = true;
        if(col.gameObject.name == "ladder" && Climbfront())
        {

            ladder = true;
            anim.SetBool("ladder", true); 
        }
    }

    void OnCollisionExit(Collision col)
    {
        collide = false;
        if (col.gameObject.name == "ladder")
        {
            ladder = false;
            anim.SetBool("ladder", false);
        }
    }
 
    //-----------------FIXED UPDATE---------------------------------------------------------

    void FixedUpdate()
    {

        //-------------JUMP-------------------------------------------------------------------
        if (!anim.GetBool("hang") && Input.GetButton("space")  && (this.anim.GetCurrentAnimatorStateInfo(0).IsName("run") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("idle")))
        {
            rb.AddForce(new Vector3(0.0f, 50, 0f), ForceMode.Impulse);

            if(anim.GetFloat("Speed") > .1 && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("up_ledge"))
            {
                rb.AddRelativeForce(new Vector3(0f, 0f, 35f), ForceMode.Impulse);
            }
            
        }


            //-----------CLIMB LADDER------------------------------------------------------------

            if (ladder == true && Climbfront())
            {
                if (Input.GetAxis("leftanalogy") < 0 && !anim.GetBool("hang"))
                {
                    rb.AddForce(new Vector3(0f, 950f, 0f), ForceMode.Force);
                }
                else if (Input.GetAxis("leftanalogy") > 0 && !anim.GetBool("hang"))
                {
                    rb.AddForce(new Vector3(0f, 600f, 0f), ForceMode.Force);
                }
                else
                {
                    rb.velocity = Vector3.zero;
                    rb.AddForce(new Vector3(0f, 800f, 0f), ForceMode.Force);
                }
                if (Input.GetButton("space") && !anim.GetBool("hang"))
                {
                    rb.AddForce(transform.forward * -150, ForceMode.Impulse);
                }
         }


        //--------------COUNTER GRAVITY WHILE HANGING----------------------------------------------------------------

        if (anim.GetBool("hang"))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0f, 800f, 0f), ForceMode.Force);

            if (Input.GetAxis("leftanalogy") >= 1)
            {
                anim.SetBool("hang", false);
                rb.AddForce(transform.forward * 15, ForceMode.Impulse);

            }
        }

       
    }

    //--------------------UPDATE----------------------------------------------------------

    // Update is called once per frame
    void Update() {
        //----------------KNIFE SEQUENCE----------------------------------------------------
        if (Input.GetButtonDown("knife"))
        {
            if (knifeRay())
            {
                anim.SetBool("knife", true);
                source.clip = knife;
                source.Play();
            }
        }

        //-------------------------------CROUCH------------------------------------------------------
        if (Input.GetButton("leftshift") || anim.GetFloat("Speed") < 1)
        {
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", true);
        }

        if(Input.GetButton("space"))
        {
            anim.SetBool("spacebar", true);
        }
        else
        {
            anim.SetBool("spacebar", false);
        }

        //----------------GET SPEED--------------------------------------
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        v = Mathf.Abs(v);
        h = Mathf.Abs(h);
        if (v > h)
        {
            anim.SetFloat("Speed", v);
        }
        else
        {
            anim.SetFloat("Speed", h);
        }

        //------------GROUND ANIMATION---------------------------------------
        if (IsGrounded(distToGround))
        {
            anim.SetBool("ground", true);
        }
        else
        {
            anim.SetBool("ground", false);
        }

        //-----------CROUCH-------------------------------------------
        if (Input.GetButtonDown("leftctrl")  && crouch)
        {
            crouch = false;
        }
        else if (Input.GetButtonDown("leftctrl")  && !crouch)
        {
            crouch = true;
        }
        if (crouch && anim.GetBool("ground"))
        {
            cap.height = .05f;
            cap.center = new Vector3(0, .025f, 0);
            anim.SetBool("crouch", true);
        }
        else
        {
            anim.SetBool("crouch", false);
            cap.height = .1f;
            cap.center = new Vector3(0, .05f, 0);
        }

        //--------------JUMP--------------------------------------------

        if (Input.GetButton("space") && anim.GetBool("ground") && !anim.GetBool("hang"))
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
        //---------------TOSS---------------------------------------------

        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("toss");
        }

        //---------------LEDGE HANG-----------------------------------------------
        if (!Climbabove() && Climbfront() && !anim.GetBool("ground"))
        {
            anim.SetBool("hang", true);
        }

        //---------------UP LEDGE/ LADDER---------------------------------------------------

        if (anim.GetBool("hang") && (Input.GetButtonDown("space") || Input.GetButtonDown("leftanalogy")))
        {
             Invoke("upover2", .3f);
            anim.SetBool("hang", false);
            rb.AddForce(new Vector3(0.0f, 640, 0f), ForceMode.Impulse);
        }

        // ---------------DRAW GUN----------------------------------------------
        if (Input.GetButtonDown("rightmouse"))
        {
            if (anim.GetBool("aim"))
            {
                anim.SetBool("aim", false);
            }
            else
            {
                anim.SetBool("aim", true); 
            }
        }

        //--------------Wall Plant--------------------------------------------------
        if(Climbabove() && Climbfront() && !anim.GetBool("ground") && anim.GetFloat("Speed") > .9f && collide == false && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("climb") && !anim.GetBool("ladder"))
        {   
            anim.SetBool("wallplant", true);
            Invoke("stallEnd", 1f);
            rb.velocity = Vector3.zero;
        }

        if(anim.GetBool("wallplant"))
        {
            rb.AddForce(new Vector3(0f, 500f, 0f), ForceMode.Force);
            if(Input.GetButtonDown("space"))
            {
                rb.AddForce(transform.forward * 400, ForceMode.Impulse);
                rb.AddForce(0, 600, 0, ForceMode.Impulse);
                anim.SetBool("wallplant", false);
            }
        }
    }

    //------------------RAYCAST INFRONT AND ABOVE--------------------------------------------------
    bool knifeRay()
    {
        var origin = new Vector3();
        origin = transform.position;
        origin.y += 1.0f;
        Vector3 direction = transform.forward * 1;
        Ray ray = new Ray(origin, direction);
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "NPC")
        {
            transform.position = hit.transform.position - (hit.transform.forward * .5f);
            hit.transform.gameObject.GetComponent<Animator>().Stop();
            transform.forward = hit.transform.forward;
            return true;
        }
        else
        {
            return false;
        }
    }

    bool Climbfront()
    {
        var origin2 = new Vector3();
        origin2 = transform.position;
        if(Input.GetButton("leftctrl"))
        {
            origin2.y += 1f;
        }
        else
        {
            origin2.y += 2.0f;
        }
        Vector3 front = transform.TransformDirection(new Vector3(0, 0, .5f));
        /*Debug.DrawRay(origin2, front, Color.blue, .1f, true);
        Ray ray = new Ray(origin2, front);
        if  (source.clip.name == "knife" && source.isPlaying)
        {
            return false;
        }
        Debug.Log(hit.transform.tag);*/
        return Physics.Raycast(origin2, front, 1f);

    }

    bool Climbabove()
    {
        var origin1 = new Vector3();
        origin1 = transform.position;
        origin1.y += 2.2f;

        Vector3 front = transform.TransformDirection(new Vector3(0, 0, .5f));

        Debug.DrawRay(origin1, front, Color.red, .1f, true);

        return Physics.Raycast(origin1, front, 1f);
    }
    
}



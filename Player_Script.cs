using UnityEngine;
using System.Collections;



public class MoveForward : MonoBehaviour {

    public float speed = 5f;
    float factor;
    public Animator anim;
    public AudioClip sprint, jump, land;
    public AudioSource source;



    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //--------------------------LAND AUDIO---------------------------------------------------------------
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("air") && anim.GetBool("ground") && !source.isPlaying)
        {
            source.clip = land;
            source.Play();
        }
        //--------------------------JUMP AUDIO----------------------------------------------------------------
        if (Input.GetButtonDown("space") && anim.GetBool("ground"))
        {
            source.clip = jump;
            source.Play();
        }

        //--------------------------Hide Cursor-----------------------------------------------------------------
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        //-------------------------MOVE SLOWER WHEN...----------------------------------------------------------
        if (Input.GetKey(KeyCode.LeftControl) || GameObject.Find("MainCamera").GetComponent<CameraScript>().aim)
        {
            factor = 1;
        }
        else
        {
            factor = 2;
        }

        Run();

        if (anim.GetBool("wallplant") && Input.GetButtonDown("space"))
        {
            Vector3 tmp1 = transform.eulerAngles;
            tmp1.y += 180;
            transform.eulerAngles = tmp1;
        }


        if (GameObject.Find("MainCamera").GetComponent<CameraScript>().aim)
        {
            //make y axis of character match y axis of sphere script
            var sphere1 = GameObject.Find("AimSphere");
            var sphere = GameObject.Find("Sphere");
            Vector3 tmp1 = transform.eulerAngles;
            tmp1.x = sphere1.transform.eulerAngles.x;
            tmp1.y = sphere.transform.eulerAngles.y;
            transform.eulerAngles = tmp1;
        }

        //-------------TILT CHARACTER WHILE AIMING--------------------------

    }


    //----------------------WHILE RUNNING------------------------------------------
    void Run()
    {
        var sphere = GameObject.Find("Sphere");
        var sphere1 = GameObject.Find("AimSphere");
        Vector3 tmp = sphere.transform.eulerAngles;
        tmp.y = sphere.transform.eulerAngles.y;
        tmp.x = sphere1.transform.eulerAngles.x;

        if (!anim.GetBool("hang") && !anim.GetBool("ladder") && (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("up_ledge")))
        {
            float angle = (Mathf.Atan2(Input.GetAxis("leftanalogy"), Input.GetAxis("leftanalogx"))) * 180 / Mathf.PI;
            
            if (!anim.GetBool("ground"))
            {
                factor /= 8;
            }
            else if(Input.GetButton("leftshift") || GetComponent<AnimatorController>().crouch || anim.GetFloat("Speed") < 1)
            {
                factor /= 3;
            }

            if (Mathf.Abs(Input.GetAxis("leftanalogx")) > .2 || Mathf.Abs(Input.GetAxis("leftanalogy")) > .2)
            {
                tmp.y += angle + 90;
                transform.eulerAngles = tmp;
                transform.Translate(0, 0, factor * speed * Time.deltaTime);
            }
            //--------------MOVE DIAGANOL------------------------------------------------

            /*
            if (Input.GetKey(KeyCode.D) && Input.GetKey("w") || Input.GetAxis("leftanalogy") < -.2f && Input.GetAxis("leftanalogx") > .2f)
            {
                tmp.y += 45;
                transform.eulerAngles = tmp;
                transform.Translate(0, 0, factor * speed * Time.deltaTime);
            }

            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) || Input.GetAxis("leftanalogy") > .2f && Input.GetAxis("leftanalogx") > .2f)
            {
                tmp.y += 135;
                transform.eulerAngles = tmp;
                transform.Translate(0, 0, factor * speed * Time.deltaTime);
            }

            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) || Input.GetAxis("leftanalogy") > .2f && Input.GetAxis("leftanalogx") < -.2f)
            {
                tmp.y += 225;
                transform.eulerAngles = tmp;
                transform.Translate(0, 0, factor * speed * Time.deltaTime);
            }

            else if (Input.GetKey(KeyCode.A) && Input.GetKey("w") || Input.GetAxis("leftanalogy") < -.2f && Input.GetAxis("leftanalogx") < -.2f)
            {
                tmp.y += 315;
                transform.eulerAngles = tmp;
                transform.Translate(0, 0, factor * speed * Time.deltaTime);
            }

            //-------------MOVE STRAIGHT-----------------------------------------------------------

            else if (Input.GetAxis("leftanalogy") > 0 || Input.GetKey(KeyCode.S))
            {
                tmp.y += 180;
                transform.eulerAngles = tmp;
                transform.Translate(0, 0, factor * speed * Time.deltaTime);
            }
            else if (Input.GetAxis("leftanalogy") < 0 || Input.GetKey(KeyCode.W))
            {
                transform.eulerAngles = tmp;
                transform.Translate(0, 0, factor * speed * Time.deltaTime);
            }


            else if (Input.GetAxis("leftanalogx") > 0 || Input.GetKey("d"))
            {
                tmp.y += 90;
                transform.eulerAngles = tmp;
                transform.Translate(0, 0, factor * speed * Time.deltaTime);
            }

            else if (Input.GetAxis("leftanalogx") < 0 || Input.GetKey("a"))
            {
                tmp.y += 270;
                transform.eulerAngles = tmp;
                transform.Translate(0, 0, factor * speed * Time.deltaTime);
            }

           */
            //----------------------------RUN AUDIO-----------------------------------------------------

            if (!anim.GetBool("aim") && (anim.GetFloat("Speed") > .5 || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) && anim.GetBool("ground") && !Input.GetButton("space") && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("air") && !anim.GetBool("crouch") && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            {
                source.clip = sprint;
                if (!source.isPlaying)
                    source.Play();
            }
            else if(source.clip == sprint)
            {
                source.Stop();
            }

            if (!anim.GetBool("ground"))
            {
                factor *= 8;
            }
            else if (Input.GetButton("leftshift"))
            {
                factor *= 3;
            }
        }

        //-------------------------------------------------------------------------------

        if (anim.GetBool("hang"))
        {
            if(Input.GetAxis("leftanalogy") >= 1 || Input.GetKey(KeyCode.S))
            {
                tmp.y += 180;
                transform.eulerAngles = tmp;
            }
        }

        if (anim.GetBool("ladder") && !anim.GetBool("hang"))
        {
            if (Input.GetAxis("leftanalogx") <= -1 || Input.GetKey("a"))
            {
                transform.Translate(-1 * Time.deltaTime, 0, 0);
            }

            else if (Input.GetAxis("leftanalogx") >= 1 || Input.GetKey("d"))
            {
                transform.Translate(1 * Time.deltaTime, 0, 0);
            }
        }
    }

}

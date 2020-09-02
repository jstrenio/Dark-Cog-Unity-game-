using UnityEngine;
using System.Collections;

public class badguyScript : MonoBehaviour {
    public Rigidbody rb;
    public Animator anim;

    public int state, checkpoint, chasechoice;
    public GameObject p1, p2, p3, p4;
    public BoxCollider body;
    public SphereCollider head;
    public bool waitstart = false;
    public bool choose = true;
    public bool choose2 = false;
    public bool voke = true;
    public int health = 100;
    public GameObject ninja;
    public new Vector3 target;
    public RaycastHit hit;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        body = GetComponent<BoxCollider>();
        head = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        anim.SetInteger("anstate", state);
        checkpoint = 1;
        ninja = GameObject.Find("ninja");
        StartCoroutine("decisions");
        target = ninja.transform.position;


    }  
	
	// Update is called once per frame
	void Update () {
        
        //------------------Hit Box Location-----------------------------
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("crouch"))
        {
            body.size = new Vector3(.023f, .037f, .028f);
            head.center = new Vector3(0, .043f, .018f);
            body.center = new Vector3(0, .02f, 0);
        }
        else
        {
            body.size = new Vector3(.023f, .076f, .013f);
            head.center = new Vector3(0, .088f, 0);
            body.center = new Vector3(0, .038f, 0);
        }

        //-----------------STATES----------------------------------------
        if (health < 1)
        {
            //dead
            state = 6;
        }
        if(ninja.GetComponent<Animator>().GetBool("knife") && Vector3.Distance(transform.position, ninja.GetComponent<Transform>().position) <= 1)
        {
            state = 6;
        }

        if (state == 2)
        {
            patrol();  
        }
        else if (state == 4)
        {
            chase();
        }
        else if (state == 6)
        {
            Destroy(this.gameObject);
            GameObject ragdoll = (GameObject)Instantiate(Resources.Load("Ragdoll Prefab"), transform.position, transform.rotation);
        }
        else if (state == 1)
        {
            transform.Translate(Vector3.zero);
            anim.SetInteger("anstate", 1);
        }
    }

    void OnCollisionEnter(Collision rb)
    {
        if (rb.gameObject.tag == "Arrow" && rb.collider.name == "SphereCollider")
        {
            health -= 100;
        }
    } 

    void patrol()
    {
        //checkpoint 1
        if (Vector3.Distance(p1.transform.position, transform.position) > 1 && checkpoint == 1)
        {
            anim.SetInteger("anstate", 2);
            transform.LookAt(p1.transform);
            transform.Translate(0, 0, 1.5f * Time.deltaTime);
        }

        else if (Vector3.Distance(p1.transform.position, transform.position) <= 1 && checkpoint == 1)
        {
            anim.SetInteger("anstate", 1);
            transform.Rotate(Vector3.up * 30 * Time.deltaTime);
            if (waitstart == false)
            {
                Invoke("wait", 3);
                waitstart = true;
            }
        }
        //checkpoint 2
        else if (Vector3.Distance(p2.transform.position, transform.position) > 1 && checkpoint == 2)
        {
            anim.SetInteger("anstate", 2);
            transform.LookAt(p2.transform);
            transform.Translate(0, 0, 1.5f * Time.deltaTime);
        }

        else if (Vector3.Distance(p2.transform.position, transform.position) <= 1 && checkpoint == 2)
        {
            anim.SetInteger("anstate", 1);
            transform.Rotate(Vector3.up * 30 * Time.deltaTime);
            if (waitstart == false)
            {
                Invoke("wait", 3);
                waitstart = true;
            }
        }
        //checkpoint 3
        else if (Vector3.Distance(p3.transform.position, transform.position) > 1 && checkpoint == 3)
        {
            anim.SetInteger("anstate", 2);
            transform.LookAt(p3.transform);
            transform.Translate(0, 0, 1.5f * Time.deltaTime);
        }

        else if (Vector3.Distance(p3.transform.position, transform.position) <= 1 && checkpoint == 3)
        {
            anim.SetInteger("anstate", 1);
            transform.Rotate(Vector3.up * 30 * Time.deltaTime);
            if (waitstart == false)
            {
                Invoke("wait", 3);
                waitstart = true;
            }
        }
        //checkpoint 4
        else if (Vector3.Distance(p4.transform.position, transform.position) > 1 && checkpoint == 4)
        {
            anim.SetInteger("anstate", 2);
            transform.LookAt(p4.transform);
            transform.Translate(0, 0, 1.5f * Time.deltaTime);
        }

        else if (Vector3.Distance(p4.transform.position, transform.position) <= 1 && checkpoint == 4)
        {
            anim.SetInteger("anstate", 1);
            transform.Rotate(Vector3.up * 30 * Time.deltaTime);
            if (waitstart == false)
            {
                Invoke("wait", 3);
                waitstart = true;
            }
        }
    }

     void wait()
    {

        if (checkpoint == 4)
        {
            checkpoint = 1;
        }
        else
        {
            checkpoint++;
        }

        waitstart = false;
    }

    void chase()
    {
        var origin = new Vector3();
        origin = transform.position;
        origin.y += .8f;
      //  origin.x += .5f;
      //  origin.z += .5f;
        Vector3 front = transform.TransformDirection(new Vector3(0, 0, 4f));
        Vector3 right = transform.TransformDirection(new Vector3(4, 0, 0f));
        Debug.DrawRay(origin, front, Color.red, .1f, true);
        Debug.DrawRay(origin, right, Color.blue, .1f, true);

        //if chasechoice 3 has been triggered do it before the other stuff
        if (chasechoice == 3)
        {
            evaderight(target);
        }
        else if (chasechoice == 4)
        {
            evadeleft(target);
        }
        else if (chasechoice == 5)
        {
            backup();
            Debug.Log("backup");
        }

        //if there's something in front of you
        if (Physics.Raycast(origin, front, 2.5f))
        {
            //see if there's an object to the right
            if(!Physics.Raycast(origin, right, 2f) && Physics.Raycast(origin, front, 5f))
            {
                //if not run right
              //  target = origin + right*.5f + front*.5f;
              //  GameObject.Find("testcube").transform.position = target;
              //  chasechoice = 3;
                
            }
            else if(!Physics.Raycast(origin, -right, 2f) && Physics.Raycast(origin, front, 5f))
            {
             //   target = origin - right * .5f + front * .5f;
             //   GameObject.Find("testcube").transform.position = target;
              //  chasechoice = 4;
            }
            else if (!Physics.Raycast(origin, -front, 2f) && Physics.Raycast(origin, front, 5f))
            {
                
             //   chasechoice = 5;
            }
        }
        else if (chasechoice == 1)
        {
            charge();
        }
        else if (chasechoice == 2)
        {
            shoot();
        }
    }


    IEnumerator decisions()
    {
        while (true)
        {
                //if state = -------------------CHASE--------------------------------
                if (state == 4)
                {
                    // -----------------DISTANCE FAR--------------------------------
                    if (60 > Vector3.Distance(transform.position, ninja.transform.position) && Vector3.Distance(transform.position, ninja.transform.position) > 5)
                    {
                       if (chasechoice == 3 || chasechoice == 4 || chasechoice == 5)
                        {
                            yield return new WaitForSeconds(.5f);
                            chasechoice = 1;
                        }
                      
                    //choose a chase action
                        if (Random.value < 2f)
                        {
                            chasechoice = 1;
                            yield return new WaitForSeconds(3f);
                        }
                        else
                        {
                            chasechoice = 2;
                            yield return new WaitForSeconds(1f);
                        }
                    }
                    else if (Vector3.Distance(transform.position, ninja.transform.position) < 5)
                    {
                        state = 1;
                    }
                }
            yield return null;
        }
    }

    //----------CHASE PLAYER ACTIONS FUNCTIONS----------------------------------------------
    //run at player function
    void charge()
    {
        
        //raycast line of sight, if you can't see him, run to where you last saw him
        Vector3 bgorigin = new Vector3();
        bgorigin = transform.position + Vector3.up;
        GameObject.Find("testcube").transform.position = target + Vector3.up;
        Vector3 direction = new Vector3();
        direction = GameObject.Find("testcube").transform.position - bgorigin;
        Debug.DrawRay(bgorigin, direction);
        Ray ray = new Ray(bgorigin, direction);

        Vector3 directiontest = new Vector3();
        directiontest = (ninja.transform.position + Vector3.up * .5f) - bgorigin;
        directiontest = directiontest.normalized;
        float fov = Vector3.Angle(transform.forward, directiontest);


        if (!Physics.Raycast(ray, out hit, Vector3.Distance(target, bgorigin)))
        {
            target = ninja.transform.position + Vector3.up*.5f;
           // Debug.Log(Vector3.Angle(bgorigin, ninja.transform.position + Vector3.up));
        }

        //if while you can't see him you suddenly can see him reset target to player
        else
        {
            Vector3 direction2 = new Vector3();
            direction2 = (ninja.transform.position + Vector3.up*.5f) - bgorigin;
            Ray ray2 = new Ray(bgorigin, direction2);
            direction2 = direction2.normalized;
            
            if (!Physics.Raycast(ray2, out hit, Vector3.Distance(ninja.transform.position, bgorigin)) && fov < 100) 
            {
                target = ninja.transform.position + Vector3.up;
            }

        }

        //spin to face player don't rotate vertically
        var q = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 200 * Time.deltaTime);
        Vector3 tmp1 = transform.eulerAngles;
        tmp1.x = 0;
        transform.eulerAngles = tmp1;
        //play run animation
        anim.SetInteger("anstate", 3);

        //move player forward
        transform.Translate(Vector3.forward * 6 * Time.deltaTime);
    }
    //crouch function

    //evade object function
    void evaderight(Vector3 target)
    {
        //spin to right
        var q = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 250 * Time.deltaTime);
        Vector3 tmp1 = transform.eulerAngles;
        tmp1.x = 0;
        transform.eulerAngles = tmp1;
        anim.SetInteger("anstate", 3);
        transform.Translate(Vector3.forward * 6 * Time.deltaTime);
    }
    void evadeleft(Vector3 target)
    {
        //spin to left
        var q = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 250 * Time.deltaTime);
        Vector3 tmp1 = transform.eulerAngles;
        tmp1.x = 0;
        transform.eulerAngles = tmp1;
        anim.SetInteger("anstate", 3);
        transform.Translate(Vector3.forward * 6 * Time.deltaTime);
    }
    //idle while facing player

    //run parallel to character

    //back up from player
    void backup()
    {
        var q = Quaternion.LookRotation(ninja.transform.position + transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 500 * Time.deltaTime);
        Vector3 tmp1 = transform.eulerAngles;
        tmp1.x = 0;
        transform.eulerAngles = tmp1;
        anim.SetInteger("anstate", 3);
        transform.Translate(Vector3.forward * 6 * Time.deltaTime);
    }
    //melee player

    //jump wall

    //shoot at player
    void shoot()
    {
        //spin to face player don't rotate vertically
        var q = Quaternion.LookRotation(ninja.transform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 100 * Time.deltaTime);
        Vector3 tmp1 = transform.eulerAngles;
        tmp1.x = 0;
        transform.eulerAngles = tmp1;
        anim.SetInteger("anstate", 5);
    }

    //walk and shoot at player

}


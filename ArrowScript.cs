using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

   public Rigidbody rb;

	// Use this for initialization
	void Start () {

        Physics.IgnoreCollision(GameObject.Find("ninja").GetComponent<Collider>(), GetComponent<Collider>());
    }
	
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "NPC")
        {
            if(col.collider is SphereCollider)
            {
                Debug.Log("headshot");
            }
            Destroy(col.transform.gameObject);
            GameObject ragdoll = (GameObject)Instantiate(Resources.Load("Ragdoll Prefab"), col.transform.position, col.transform.rotation);
        }
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true;
        transform.parent = null;
        Destroy(this.gameObject, 3);
    }
}

using UnityEngine;
using System.Collections;

public class WeaponBGscript : MonoBehaviour {
    public Transform transform;
    public Transform bghand;
    // Use this for initialization
    void Start () {
        transform = GetComponent<Transform>();
        bghand = transform.parent;
      //  Debug.Log(transform.parent);
     //   transform.position = bghand.position;
    }
	
	// Update is called once per frame
	void Update () {
        
        var hand = GameObject.Find("bghand");
        transform.parent = hand.transform;

    }
}

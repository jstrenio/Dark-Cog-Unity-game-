using UnityEngine;
using System.Collections;

public class RagdollScript : MonoBehaviour {
    public Rigidbody rb;
    public Collider col;
    public AudioSource audio;
    public AudioClip impact, death1, death2, death3, death4;
    int impacts = 0;
    public Animator ninjaAni;
	// Use this for initialization
	void Start () {
        ninjaAni = GameObject.Find("ninja").GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

       
         if (GameObject.Find("WeaponParentbow").GetComponent<WeaponParentScript>().carry == 1)
        {
            rb.AddForce(GameObject.Find("ninja").GetComponent<Transform>().forward * 25, ForceMode.VelocityChange);
        }
        else if (GameObject.Find("WeaponParent").GetComponent<WeaponParentScript>().carry == 2)
        {
            rb.AddForce(GameObject.Find("ninja").GetComponent<Transform>().forward * 40, ForceMode.VelocityChange);
        }
        Invoke("gone", 10);

        int deathaudio = (int)(Random.Range(0f, 4f)) % 4;
        switch (deathaudio)
        {
            case 0:
                audio.clip = death1;
                audio.Play();
                break;
            case 1:
                audio.clip = death2;
                audio.Play();
                break;
            case 2:
                audio.clip = death3;
                audio.Play();
                break;
            case 3:
                audio.clip = death1;
                audio.Play();
                break;
        }
    }
	
    void OnCollisionEnter(Collision col)
    {
        if (!audio.isPlaying && impacts < 1)
        {
            audio.clip = impact;
            audio.Play();
            impacts++;
        }
    }
	// Update is called once per frame
	void Update () {
        if (ninjaAni.GetBool("knife"))
        {
            ninjaAni.SetBool("knife", false);
        }
    }

    void gone()
    {
        Destroy(this.gameObject);
    }
}

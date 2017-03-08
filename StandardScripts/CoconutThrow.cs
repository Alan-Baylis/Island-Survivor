using UnityEngine;
using System.Collections;

public class CoconutThrow : MonoBehaviour {

	public AudioClip throwSound;
	public 	Rigidbody coconutObject;
	public float throwForce;

    public bool canThrow = false;

	// Update is called once per frame
	void Update () {

		if(Input.GetButtonUp ("Fire1") && canThrow)
		{
            GetComponentInParent<AudioSource>().PlayOneShot(throwSound);
			Rigidbody newCoconut = (Rigidbody)Instantiate (coconutObject, transform.position, transform.rotation);
			newCoconut.name = "coconut";
			newCoconut.GetComponent<Rigidbody>().velocity = transform.transform.TransformDirection(0,0,throwForce);
			Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), newCoconut.GetComponent<Collider>(),true);
		}	
	}
}

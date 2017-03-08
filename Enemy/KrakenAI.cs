using UnityEngine;
using System.Collections;

public class KrakenAI : MonoBehaviour {

    public GameObject launcher;
    public Rigidbody square;
    public GameObject character;

    bool justThrow = false;

    // Use this for initialization
    void Update ()
    {
        if(transform.position.z < -200)
            transform.position += new Vector3(0, 0, 2);
        else if(justThrow == false)
        {
            Rigidbody newDeathBox = (Rigidbody)Instantiate(square, launcher.transform.position, launcher.transform.rotation);
            newDeathBox.name = "death box";
            
            newDeathBox.GetComponent<Rigidbody>().velocity = transform.TransformDirection(-500, 0, 0);

            justThrow = true;
        }
        
    }
	
}

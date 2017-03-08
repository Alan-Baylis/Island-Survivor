using UnityEngine;
using System.Collections;

public class AttackBoxTidy : MonoBehaviour {

    void Start()
    {
        Destroy(gameObject, 5);
    }

	void OnCollisionEnter(Collision hit)
    {
		if (hit.gameObject.name == "Character") 
		{
			hit.gameObject.GetComponent<PlayerAnimations> ().healthPoints -= 10;
			Destroy(gameObject);
		}
    }
}

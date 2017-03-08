using UnityEngine;
using System.Collections;

public class DeathBox : MonoBehaviour {

    GameObject player;

    float timer;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Character");

	}

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.5f)
        {
            player.GetComponent<PlayerAnimations>().healthPoints = 0;
        }
    }
}

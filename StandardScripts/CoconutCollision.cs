using UnityEngine;
using System.Collections;

public class CoconutCollision : MonoBehaviour {

    //verifica se foi atingido
    private bool beenHit = false;
    //tempo que o alvo fica para baixo
    private float timer = 0.0f;

    TargetManager tg;

    // Use this for initialization
    void Awake () {
        tg = GameObject.Find("Targets").GetComponent<TargetManager>();
	}
	
	// Update is called once per frame
	void Update () {

	    if(tg.isPlayable)
        {
            if (beenHit)
            {
                timer += Time.deltaTime;
            }

            if (timer >= 3)
            {
                GetComponent<AudioSource>().PlayOneShot(tg.resetSound);
                GetComponentInParent<Animation>().Play("up");

                tg.hits--;

                timer = 0;

                beenHit = !beenHit;
            }
        }
	}

    void OnCollisionEnter(Collision theObject)
    {
        if(beenHit == false && theObject.gameObject.name == "coconut")
        {
            GetComponentInParent<AudioSource>().PlayOneShot(tg.hitSound);
            GetComponentInParent<Animation>().Play("down");

            tg.hits++;

            beenHit = true;  
        }
    }
}

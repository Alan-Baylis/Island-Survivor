using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegenerateMap : MonoBehaviour {
        /*
     * Every map will have a game genre
     * 
     * 8 - the first and standard, only appears once
     * 9 - Shooter
     * 
     */

    // the player
    [SerializeField]
    private GameObject player;

    // where the script MapGenerator is
    [SerializeField]
    private GameObject map;

    // the map generator script
    MapGenerator mapGen;

    // text that shows the change timer
    public Text timerText;

    // to change game genre
    public float timeToChange = 3;
    bool changeGenre = false;
    float changeTimer = 0.0f;

    void Awake()
    {
        mapGen = map.GetComponent<MapGenerator>();
    }

	// Update is called once per frame
	void Update ()
    {
        //change map
        if (Input.GetKey("m"))
        {
            changeTimer = 0;
            changeGenre = true;
        }

        // change game genre
        if (changeGenre)
            ChangeGenre();

        // generate another map
        if (Input.GetKey("p"))
            mapGen.RegenerateMap(MapGenerator.mapGameGenre, null);

        // before the map gets regenerate, the player must fly - so he don't get trapped in the new collider
        if (Input.GetKey("o"))
            player.transform.position += new Vector3(0, 3, 0);
    }

    public void StartChange()
    {
        changeTimer = 0;
        changeGenre = true;
    }

    void ChangeGenre()
    {

		if (changeTimer > 0 && changeTimer < timeToChange) {
			timerText.text = "Changing Idea in: " + (timeToChange - changeTimer).ToString ("0.0");
			player.GetComponentInChildren<ParticleSystem> ().Play ();
		}

        // decrease timer
        changeTimer += Time.deltaTime;

        // if the timer is 0, the player must fly and the map change
        if (changeTimer > timeToChange && changeTimer < timeToChange + 2.5f)
            player.transform.position += new Vector3(0, 2, 0);

        // after this, the island will change and the player will fall
        if (changeTimer > timeToChange + 2.5f)
        {
            timerText.text = "";
            mapGen.RegenerateMap(MapGenerator.mapGameGenre, null);
            changeTimer = timeToChange;
            changeGenre = false;
			player.GetComponentInChildren<ParticleSystem> ().Stop ();
        }
    }
}

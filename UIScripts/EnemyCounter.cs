using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour {

    public GameObject loadGame;

    public int deadEnemies = 0;

	// Update is called once per frame
	void Update ()
    {
	    if(deadEnemies > 0)
        {
            GetComponentInChildren<Image>().enabled = true;
            GetComponentInChildren<Text>().text = "kill count: " + deadEnemies.ToString();

            if(deadEnemies >= 5 && !loadGame.GetComponent<LoadGameGenres>().krakenReleased)
            {
                loadGame.GetComponent<LoadGameGenres>().ReleaseTheKraken();
            }
        }
	}
}

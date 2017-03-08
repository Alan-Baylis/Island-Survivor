using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public GameObject character;

    public GameObject gameManager;

    private PlayerAnimations script;
    private LoadGameGenres scriptLoad;

    private Image thisImage;

	// Use this for initialization
	void Start () {
        script = character.GetComponent<PlayerAnimations>();
        thisImage = GetComponent<Image>();
        scriptLoad = gameManager.GetComponent<LoadGameGenres>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (0.008f * (script.maxHealthPoints - script.healthPoints) < 0.8f)
        {
            thisImage.color = new Color(200, 0, 0, 0.008f * (script.maxHealthPoints - script.healthPoints));
        }

        // player is dead
        if(script.healthPoints <= 0 && PlayerAnimations.playerAlive)
        {
            scriptLoad.GameOverText();
            script.reloadTime = 1.5f;
        }
	}
}

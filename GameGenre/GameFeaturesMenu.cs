using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameFeaturesMenu : MonoBehaviour {

    [SerializeField]
    private GameObject gameManager;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject featureButton;

    // script that stores the genres array
    LoadGameGenres loadGenres;

    // components used in the menu
    Image image;
    Text txt;

    Image buttonImage;
    Text buttonText;

    int currentGenre = 0;



	// Use this for initialization
	void Awake ()
    {

        image = GetComponentInChildren<Image>();
        image.enabled = false;

        txt = GetComponentInChildren<Text>();
        txt.enabled = false;

        buttonImage = GetComponentInChildren<Button>().GetComponentInChildren<Image>();
        buttonImage.enabled = false;

        buttonText = GetComponentInChildren<Button>().GetComponentInChildren<Text>();
        buttonText.enabled = false;

        loadGenres = gameManager.GetComponent<LoadGameGenres>();

    }
	
	// Update is called once per frame
	void Update () {

        // can't open menu in the first map  
        if (Input.GetKeyDown("i") && MapGenerator.mapGameGenre != 8)
        {
            image.enabled = !image.enabled;
            txt.enabled = !txt.enabled;
            buttonImage.enabled = !buttonImage.enabled;
            buttonText.enabled = !buttonText.enabled;

            player.GetComponent<FirstPersonController>().enabled = !player.GetComponent<FirstPersonController>().enabled;

            UpdateMenu();
        }
	}

    // change color, features and description of the menu
    void UpdateMenu()
    {
        currentGenre = MapGenerator.mapGameGenre;

        txt.text = loadGenres.genres[currentGenre-9].getName();

        image.color = loadGenres.genres[currentGenre-9].getColor() - new Color(0, 0, 0, 0.5f);

        buttonImage.GetComponent<Image>().color = image.color + new Color(0, 0, 0, 0.25f);
        
    }

}

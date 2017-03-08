using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour {


    [SerializeField]
    private GameObject regenerate;
    [SerializeField]
    private GameObject messages;
    [SerializeField]
    private GameObject narrator;
    [SerializeField]
    private GameObject gameManager;


    // script that changes the narrator line
    SubtitleManager narratorLines;
    // script that display a string in the screen
    TextManager textManager;
    // script that change the map
    RegenerateMap regenerateMap;
    // script that contains the array with genres
    LoadGameGenres loadGenres;

    // if the game genre has been changed
    public static bool gameChange = false;
    
    // vars that controll player interactions
    bool firstStep = false;
    public bool firstCoconutTrow = false;
    public bool firstFPSMode = false;
    public static bool firstDoor = false;

	// Use this for initialization
	void Awake ()
    {
        narratorLines = narrator.GetComponent<SubtitleManager>();
        regenerateMap = regenerate.GetComponent<RegenerateMap>();
        textManager = messages.GetComponent<TextManager>();
        loadGenres = gameManager.GetComponent<LoadGameGenres>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        // if the game is playable
        if (MenuManager.inGame)
        {
            if (firstDoor)
            {
                regenerateMap.StartChange();
                narratorLines.UpdateSubtitle("Pensando bem, acho que vou fazer um game com gênero diferente...", null, null);
                firstDoor = false;
            }
        }

        if (gameChange && MapGenerator.mapGameGenre != 8)
        {
            textManager.DisplayMessage(loadGenres.genres[MapGenerator.mapGameGenre - 9].getName(), new Color(255, 255, 255), loadGenres.genres[MapGenerator.mapGameGenre - 9].getColor(), 1.5f);
            gameChange = false;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit coll)
    {
        // first time touching the floor
        if(!firstStep && coll.gameObject.name == "Map")
        {
            narratorLines.UpdateSubtitle("Eae! | Estou desenvolvendo esse game, pode dar uma olhada? | Até agora eu tenho essa ilha que é gerada randomicamente", 0, 1);
            narratorLines.UpdateSubtitle("Por enquanto o único objetivo é abrir a porta daquela cabana ali | Você precisa de 4 baterias para isso", 0, 1);
            
            firstStep = true;
        }
    }
}

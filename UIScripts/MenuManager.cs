using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    // if the player is in the game
    public static bool inGame = false;

    public GameObject player;

    // start menu texts to update the menu screen
    public Text[] menuTexts;
    // the option current selected
    static int menuIndex = 0;

    void Awake()
    {
        inGame = false;
        player.GetComponent<CharacterController>().enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Game Menu
        if (!inGame)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                menuIndex = AttMenu(menuIndex, -1);
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                menuIndex = AttMenu(menuIndex, 1);

            if (Input.GetKeyDown(KeyCode.Return))
                SelectOption(menuIndex);

            ChangeTextColor();
        }
	}

    // change the menuIndex
    int AttMenu(int menuIndex, int i)
    {
        int m = menuIndex + i;

        if (m < 0)
            m = 3;
        else if (m > 3)
            m = 0;

        return m;
    }

    void ChangeTextColor()
    {
        for(int i = 0; i < menuTexts.Length; i++)
        {
            if (i == menuIndex)
                menuTexts[i].color = Color.black;
            else
                menuTexts[i].color = Color.white;
        }
    }

    void SelectOption(int option)
    {
        if (option == 0)
        {
            // start the game
            inGame = true;

            // give the player gravity so he can touch the floor
            player.GetComponent<CharacterController>().enabled = true;

            DestroyComponents();
        }
    }

    // disable texts
    void DestroyComponents()
    {
        for(int i = 0; i < menuTexts.Length; i++)
        {
            Destroy(menuTexts[i]);
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadGameGenres : MonoBehaviour {

    public GameGenre[] genres;

    [SerializeField]
    private GameObject charParent;
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject FPSgun;

    public GameObject kraken;

    public GameObject counter;
    
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject gameCanvas;
    [SerializeField]
    private GameObject narrator;
    [SerializeField]
    private GameObject enemy;

    private PlayerInteractions playerInteractions;
    private SubtitleManager narratorLines;
    private PlayerAnimations playerAnim;
    private PlayerCollision playerColl;

    Vector3 gunPos;
    Vector3 gunRotation;
	
	System.Random rand;

    public bool krakenReleased;

    // Use this for initialization
    void Start () {

        genres = new GameGenre[2];

        // creating features in a fast way
        Feature[] fs = new Feature[4];

        for(int i = 0; i < 4; i++)
        {
            fs[i] = new Feature("Descript " + i.ToString(), i, false);
        }
        // creating games genre in a fast way.
        genres[0] = new GameGenre(9, "Shooter", new Color(200, 0, 0), fs);
        genres[1] = new GameGenre(10, "Survival", new Color(255, 255, 0), fs);

        gunPos = new Vector3(-0.1f, 0.195f, -0.447f);
        gunRotation = new Vector3(1.417f, 360, 360);

        playerInteractions = charParent.GetComponent<PlayerInteractions>();
        narratorLines = narrator.GetComponent<SubtitleManager>();
        playerColl = charParent.GetComponent<PlayerCollision>();


    }

    // at this point, the map will be already clean from previous game genre prefabs,
    // so this function will organize the new characteristcs from the new genre
    public void PerpareNewGenre(int genre)
    {
        if (genre == 9)
            PerpareShooter();
        if (genre == 10)
            PrepareSurvivor();
    }

    void PerpareShooter()
    {
        if (!playerInteractions.firstFPSMode)
        {
            playerInteractions.firstFPSMode = true;
            narratorLines.UpdateSubtitle("Agora essa ilha é de outro planeta, e você precisa encontrar recursos para fazer sua nave decolar | Cuidado com os alienigenas que protegem os recursos!", 3, 2);
        }
		
		charParent.GetComponent<BoxCollider>().enabled = true;
		
        GameObject myFPS = Instantiate(FPSgun) as GameObject;
        myFPS.transform.parent = character.transform;
        myFPS.transform.position = gunPos;      
        myFPS.transform.localEulerAngles = gunRotation;

        playerAnim = charParent.GetComponent<PlayerAnimations>();

        playerAnim.gunAnim = GetComponentInChildren<GunAnimation>();

        playerAnim.gunScript = GameObject.Find("Gun Energy").GetComponent<GunEnergy>();

        playerColl.rayCaster = GameObject.Find("Raycaster");

        LoadEnemy();
    }

    void LoadEnemy()
    {
		
		for(int i = 0; i < 10; i++)
		{
			rand = new System.Random(i);
		
			GameObject enemyObj = Instantiate(enemy) as GameObject;
			// instantiate enemies close to character
			enemyObj.tag = "enemy";
			enemyObj.transform.position = charParent.transform.position + new Vector3(rand.Next(200), -charParent.transform.position.y + 10, rand.Next(200));
            enemyObj.GetComponent<EnemyAI>().character = charParent;
            enemyObj.GetComponent<EnemyAI>().killCounter = counter;

        }
    }

    void PrepareSurvivor()
    {        
        Debug.Log("Preparing Survivor");

    }

    public void ReleaseTheKraken()
    {

        krakenReleased = true;

        foreach (GameObject g in FindObjectsOfType<GameObject>())
        {
            // destroy every enemy
            if (g.name == "enemy")
            {
                Destroy(g);
            }

        }

        GameObject enemyObj = Instantiate(kraken) as GameObject;

        enemyObj.tag = "enemy";
    }

    public void GameOverText()
    {
        // player is dead, show text
        foreach (Text txt in gameOver.GetComponentsInChildren<Text>())
        {
            txt.enabled = true;
            PlayerAnimations.playerAlive = false;
        }
    }
}
